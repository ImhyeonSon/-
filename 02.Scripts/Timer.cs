public class Timer 
{
    float nowTime = 0f;
    float timeCycle = 0f;
    public bool TimeDelaySet(float Settime) 
    {
        timeCycle = Settime;
        return true;
    }
    public bool TimeFlow(float dtime)
    {
        nowTime -= dtime;
        if (nowTime <= 0)
        {
            nowTime = timeCycle;
            return true; //���� ����
        }
        return false;
    }

    public void WaitTime() 
    {
        nowTime = 0; //���� ����� ǥ�ÿ�
    }

    public float GetTime() 
    {
        return nowTime;
    }

    public float SetNowTime(float sNowTime)
    {
        nowTime = sNowTime;
        return nowTime;
    }
}
