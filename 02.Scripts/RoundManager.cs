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
            isStart = RoundTimer.TimeFlow(Time.deltaTime); //true�� �ٲ�� ������
            UM.TimerUI(RoundTimer.GetTime()); //Ÿ�̸ӿ� �ð� ����
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

    public void StartNextRound() //���� ���� �ٷ� ����
    {
        UM.TimerOnOff(false); //Ÿ�̸� �Ⱥ��̰� �ϱ�
        startTrigger = false;
        isStart = true; //�����ߴٰ� �ٲ��ش�.
        MM.NextRound();
        UM.TimerOnOff(false); // Ÿ�̸� ����
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
