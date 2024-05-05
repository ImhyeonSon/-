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

    public TextMeshProUGUI RoundText; // 라운드 표시 패널
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



    void GameInitialize() // 처음 게임 시작
    {
        RoundLevel = 0;

    }

    public GameObject GameClearPannel;
    public void NextRound() // 실행하면 사실상 스킵이다.
    {
        if (RoundLevel < 45) //마지막 라운드
        {
            RoundLevel++;
            RoundText.text = "Round : " + RoundLevel;
            //Debug.Log(RoundLevel);
            nowMonster = MD.GetMonsterData(RoundLevel); //레벨 올리고 다음 단계 정보가져오기
            nowMonsterCount = nowMonster.GetCount(); //레벨 올리고 다음 단계 정보가져오기
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
        //Monster 수 가져오고 pooling하기, pooling후 RoundLevel에 따라 몬스터들 초기화 및 생성 등 작업 하기
        MonsterBehavior[] MonsterT = GetComponentsInChildren<MonsterBehavior>(true); // true로 꺼진 오브젝트도 가져오기

        Monsters = new GameObject[MonsterT.Length];
        for (int i=0; i<MonsterT.Length; i++) 
        {
            Monsters[i] = MonsterT[i].gameObject;
        }



        ///변경해줘야함
        int monsterIndex = 0;
        GameObject[] GenMonsters = new GameObject[nowMonsterCount];
        //Debug.Log("nowMonsterCount" + nowMonsterCount);
        //GenMonsters = new GameObject[nowMonsterCount];
        for (int i = 0; i < Monsters.Length; i++)
        {
            //Debug.Log("아오 Monsters[i].activeSelf : " + Monsters[i].activeSelf);

            if (!Monsters[i].activeSelf)
            {
                GenMonsters[monsterIndex] = Monsters[i];
                GenMonsters[monsterIndex].GetComponent<MonsterBehavior>().SetNowMonster(RoundLevel); // 미리 몬스터 레벨 설정, Monster의 Status를 Initialize해준다.
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
            GenMonsters[monsterIndex].transform.parent = gameObject.transform; //하위에 생성하는 코드
            GenMonsters[monsterIndex].GetComponent<MonsterBehavior>().SetNowMonster(RoundLevel); // 미리 몬스터 레벨 설정 ,Monster의 Status를 Initialize해준다.
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
            //Debug.Log("생성중");
            selectMonster = GenMonsters[i];
            selectMonster.SetActive(true);
            // Monster 생성, OnEnable등으로 생성
            if (cnt == 1) {
                selectMonster.transform.localScale = new Vector3(2f, 2f, 2f);
            }//보스selectMonster
            else 
            {
                selectMonster.transform.localScale = new Vector3(1f, 1f, 1f);
            }
            yield return new WaitForSeconds(genTime);
        }
    }

    // 다음 라운드로 스킵이 가능하도록 버튼 만들기
    public void MonsterDead() 
    {
        deadMonsterCount += 1;
        if (generatedMonsterCount == deadMonsterCount) 
        {
            //Next Stage 버튼 활성화 구현하기
        }
    }

    public int GetNowRound()
    {
        return RoundLevel;
    }


}
