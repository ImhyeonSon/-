public class Monster //생성자도 만들어야함
{
    // MonsterBehavior에서 사용할 정보들 

    private bool isDie=false;
    private bool isStun=false;
    private bool isSlow = false;


    private int level;
    private string monsterName;
    private float health;
    private float maxHealth;
    private float damage;
    private float defense;
    private float speed;
    private float attackSpeed;
    private int count; 
    private float genTime;
    private int killCount;

    public Monster(int sLevel = 1, string sName = "default", float sHealth = 10, float sDamage = 1, float sDefense = 0, float sSpeed = 2, float sAttackSpeed = 1, int sCount = 20, float sGentime = 1, int sKillCount = 1) 
    {
        level = sLevel;
        monsterName = sName;
        health = sHealth;
        maxHealth = sHealth;
        damage = sDamage;
        defense = sDefense; speed = sSpeed; attackSpeed = sAttackSpeed; count = sCount; genTime = sGentime; killCount=sKillCount;
    }


    public void SetStatus(int sLevel = 1, string sName = "default", float sHealth = 10, float sDamage = 1, float sDefense = 0, float sSpeed = 2, float sAttackSpeed = 1, int sCount = 20, float sGentime = 1, int sKillCount = 1) 
    {
        level = sLevel;
        monsterName = sName;
        health = sHealth;
        maxHealth = sHealth;
        damage = sDamage;
        defense = sDefense; speed = sSpeed; attackSpeed = sAttackSpeed; count = sCount; genTime = sGentime; killCount = sKillCount;
    }

    public int GetLevel() 
    {
        return level;
    }
    public string GetMonsterName()
    {
        return monsterName;
    }
    public float GetHealth() 
    {
        return health;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetDefense()
    {
        return defense;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public float GetAttackSpeed() 
    { 
        return attackSpeed;
    }
    public int GetCount() 
    {
        return count;
    }

    public float GetMaxHealth() 
    {
        return maxHealth;
    }

    public float GetGenTime()
    {
        return genTime;
    }
    public int GetKillCount()
    {
        return killCount;
    }

    public int SetLevel(int sLevel)
    {
        level = sLevel;
        return level;
    }
    public string SetMonsterName(string sName)
    {
        monsterName=sName;
        return monsterName;
    }
    public float SetHealth(float sHealth)
    {
        health = sHealth;
        return health;
    }
    public float SetDamage(float sDamage)
    {
        damage = sDamage;
        return damage;
    }
    public float SetDefense(float sDefence)
    {
        defense = sDefence;
        return defense;
    }
    public float SetSpeed(float sSpeed)
    {
        speed=sSpeed;
        return speed;
    }
    public float SetAttackSpeed(float sAttackSpeed)
    {
        attackSpeed = sAttackSpeed;
        return attackSpeed;
    }
    public int SetCount(int sCount)
    {
        count = sCount;
        return count;
    }
    public float SetMaxHealth(float sHealth) 
    {
        maxHealth=sHealth; return maxHealth;
    }
    public float SetGenTime(float sGentTime)
    {
        genTime = sGentTime; return genTime;
    }
    public int GetKillCount(int sKillCount)
    {
        killCount = sKillCount; return killCount;
    }

}