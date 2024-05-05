using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    float WaitingTime=15f;
    float nowTime = 0;
    bool isStart = true;
    Timer RoundTimer;
    int nowMonsterNumber = 0;
    bool startTrigger = false;

    public MonsterManager MM;
    public UIManager UM;
    // Start is called before the first frame update

    private void Awake()
    {
        RoundTimer = new Timer();
        InitializeTimer();        
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(isStart);
        if (!isStart)
        {
            isStart = RoundTimer.TimeFlow(Time.deltaTime); //true로 바뀌면 시작함
            UM.TimerUI(RoundTimer.GetTime()); //타이머에 시간 세팅
            //Debug.Log(RoundTimer.GetTime());
            startTrigger = true;
        }
        else 
        {
            if (startTrigger) 
            {
               
                StartNextRound();
            }
        }
    }
    void InitializeTimer() 
    {
        RoundTimer.TimeDelaySet(WaitingTime);
        RoundTimer.SetNowTime(WaitingTime);
        UM.TimerUI(RoundTimer.GetTime());
        UM.TimerOnOff(true);

        isStart = false;
    }

    public void StartNextRound() //다음 라운드 바로 시작
    {
        UM.TimerOnOff(false); //타이머 안보이게 하기
        startTrigger = false;
        isStart = true; //시작했다고 바꿔준다.
        MM.NextRound();
        UM.TimerOnOff(false); // 타이머 끄기
    }
    public void SetNowMonsterNumber(int sMonsterNum) 
    {
        nowMonsterNumber=sMonsterNum;
    }

    public void KillMonster() 
    {
        nowMonsterNumber--;
        if (nowMonsterNumber <= 0) 
        {
            InitializeTimer();
        }
    }

}
