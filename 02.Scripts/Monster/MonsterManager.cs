using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    int RoundLevel = 0;
    int nowMonsterCount = 1;

    int generatedMonsterCount = 0, deadMonsterCount = 0;
    Monster nowMonster; //MonsterData
    GameObject[] Monsters;

    public GameObject MonsterPrefab;

    DataManager MD;//DataManager;
    public RoundManager RM;

    public TextMeshProUGUI RoundText; // ���� ǥ�� �г�
    public MonsterUIPannel MUP;

    private void Awake()
    {
        GameInitialize();
        MD = DataManager.Instance;
    }
    private void Start()
    {

    }

    private void Update()
    {

    }



    void GameInitialize() // ó�� ���� ����
    {
        RoundLevel = 0;

    }

    public GameObject GameClearPannel;
    public void NextRound() // �����ϸ� ��ǻ� ��ŵ�̴�.
    {
        if (RoundLevel < 45) //������ ����
        {
            RoundLevel++;
            RoundText.text = "Round : " + RoundLevel;
            //Debug.Log(RoundLevel);
            nowMonster = MD.GetMonsterData(RoundLevel); //���� �ø��� ���� �ܰ� ������������
            nowMonsterCount = nowMonster.GetCount(); //���� �ø��� ���� �ܰ� ������������
            generatedMonsterCount += nowMonsterCount;
            RM.SetNowMonsterNumber(nowMonsterCount);
            MUP.SetNowMonster(nowMonster);
            RoundStart();
        }
        else 
        {
            GameClearPannel.SetActive(true);
            PlayerInfoManager.Instance.RankSave(45);
        }
    }

    //GameObject[] GenMonsters;

    public void RoundStart()
    {
        //Monster �� �������� pooling�ϱ�, pooling�� RoundLevel�� ���� ���͵� �ʱ�ȭ �� ���� �� �۾� �ϱ�
        MonsterBehavior[] MonsterT = GetComponentsInChildren<MonsterBehavior>(true); // true�� ���� ������Ʈ�� ��������

        Monsters = new GameObject[MonsterT.Length];
        for (int i=0; i<MonsterT.Length; i++) 
        {
            Monsters[i] = MonsterT[i].gameObject;
        }



        ///�����������
        int monsterIndex = 0;
        GameObject[] GenMonsters = new GameObject[nowMonsterCount];
        //Debug.Log("nowMonsterCount" + nowMonsterCount);
        //GenMonsters = new GameObject[nowMonsterCount];
        for (int i = 0; i < Monsters.Length; i++)
        {
            //Debug.Log("�ƿ� Monsters[i].activeSelf : " + Monsters[i].activeSelf);

            if (!Monsters[i].activeSelf)
            {
                GenMonsters[monsterIndex] = Monsters[i];
                GenMonsters[monsterIndex].GetComponent<MonsterBehavior>().SetNowMonster(RoundLevel); // �̸� ���� ���� ����, Monster�� Status�� Initialize���ش�.
                monsterIndex++;
                if (monsterIndex >= nowMonsterCount)
                {
                    break;
                }
            }
        }
        //Debug.Log("monsterIndex : " + monsterIndex);
        while (monsterIndex < nowMonsterCount)
        {
            GenMonsters[monsterIndex] = Instantiate(MonsterPrefab, new Vector3(-12, 2, -8.5f), Quaternion.identity);
            GenMonsters[monsterIndex].transform.parent = gameObject.transform; //������ �����ϴ� �ڵ�
            GenMonsters[monsterIndex].GetComponent<MonsterBehavior>().SetNowMonster(RoundLevel); // �̸� ���� ���� ���� ,Monster�� Status�� Initialize���ش�.
            monsterIndex++;
        }
        StartCoroutine(MonsterGenerator(GenMonsters, nowMonster.GetGenTime()));
    }


    public void TestNextRound()
    {
        NextRound();
    }


    IEnumerator MonsterGenerator(GameObject[] GenMonsters, float genTime)
    {
        GameObject selectMonster;
        int cnt = GenMonsters.Length;
        for (int i=0; i< cnt; i++)
        {
            //Debug.Log("������");
            selectMonster = GenMonsters[i];
            selectMonster.SetActive(true);
            // Monster ����, OnEnable������ ����
            if (cnt == 1) {
                selectMonster.transform.localScale = new Vector3(2f, 2f, 2f);
            }//����selectMonster
            else 
            {
                selectMonster.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            yield return new WaitForSeconds(genTime);
        }
    }

    // ���� ����� ��ŵ�� �����ϵ��� ��ư �����
    public void MonsterDead() 
    {
        deadMonsterCount += 1;
        if (generatedMonsterCount == deadMonsterCount) 
        {
            //Next Stage ��ư Ȱ��ȭ �����ϱ�
        }
    }

    public int GetNowRound()
    {
        return RoundLevel;
    }


}
