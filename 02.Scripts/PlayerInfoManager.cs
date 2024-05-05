using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    // Start is called before the first frame update



    private static PlayerInfoManager instance;


    private static object mylock = new object();
    public static PlayerInfoManager Instance //Instance����
    {
        get
        {
            // thread-safe
            lock (mylock)
            {
                if (instance == null)
                {
                    instance = GameObject.Find("PlayerInfoManager").GetComponent<PlayerInfoManager>();
                    //Debug.Log("instance :" + instance);
                    if (instance == null)
                    {
                        // ���� PlayerInfoManager�� ���� ��� ����
                        GameObject obj = new GameObject("PlayerInfoManager");
                        instance = obj.AddComponent<PlayerInfoManager>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                    // ��ü ����               
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        PlayerInfoManagerInitailize();
    }
    void Start()
    {
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AllDataReset();
        }
    }

    void PlayerInfoManagerInitailize()
    {
        LoadSettingData(); // �Ҹ� ������ Load
        LoadScoreData(); // ���� ������ Load
    }


    public int LoadSettingData()// 0�� on 1�� off
    {
        return PlayerPrefs.GetInt("sound");
    }
    public int SettingSave(int TF)
    {
        PlayerPrefs.SetInt("sound",TF);
        return PlayerPrefs.GetInt("sound");
    }

    public List<int> LoadScoreData()
    {
        List<int> scores = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            int score = PlayerPrefs.GetInt("score" + i); // key���� ������ 0�� return
            scores.Add(score);
        }
        return scores;
    }

    public void RankSave(int nowScore) 
    {
        List<int> scores = new List<int>();
        bool isRecord = false;
        for (int i = 0; i < 3; i++)
        {
            int score = PlayerPrefs.GetInt("score" + i); // key���� ������ 0�� return
            if (!isRecord&&score < nowScore) 
            {
                scores.Add(nowScore);
                isRecord=true;
            }
            scores.Add(score);
        }
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("score" + i, scores[i]); //�� key���� ����
        }
    }

    public void AllDataReset() 
    {
        PlayerPrefs.DeleteAll();
    }

}
