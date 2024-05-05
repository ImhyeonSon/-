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
            return true; //공격 가능
        }
        return false;
    }

    public void WaitTime() 
    {
        nowTime = 0; //공격 대기중 표시용
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
