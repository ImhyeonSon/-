using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoManager : MonoBehaviour
{
    // Start is called before the first frame update



    private static PlayerInfoManager instance;


    private static object mylock = new object();
    public static PlayerInfoManager Instance //Instance접근
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
                        // 씬에 PlayerInfoManager가 없을 경우 생성
                        GameObject obj = new GameObject("PlayerInfoManager");
                        instance = obj.AddComponent<PlayerInfoManager>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                    // 객체 리턴               
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
        LoadSettingData(); // 소리 데이터 Load
        LoadScoreData(); // 점수 데이터 Load
    }


    public int LoadSettingData()// 0은 on 1은 off
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
            int score = PlayerPrefs.GetInt("score" + i); // key값이 없으면 0을 return
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
            int score = PlayerPrefs.GetInt("score" + i); // key값이 없으면 0을 return
            if (!isRecord&&score < nowScore) 
            {
                scores.Add(nowScore);
                isRecord=true;
            }
            scores.Add(score);
        }
        for (int i = 0; i < 3; i++)
        {
            PlayerPrefs.SetInt("score" + i, scores[i]); //새 key값을 저장
        }
    }

    public void AllDataReset() 
    {
        PlayerPrefs.DeleteAll();
    }

}
