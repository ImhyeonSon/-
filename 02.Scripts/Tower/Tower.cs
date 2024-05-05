public class Tower
{
    // Start is called before the first frame update
    
        // MonsterBehavior에서 사용할 정보들 
    
    private string myType;
    private int rare;
    private int maxLevel;
    private float damage;
    private float range;
    private float attackSpeed; //높으면 공속 빠름
    private int index; //타워 종류에 대한 Index
    private float cost;
    private int cellCost;

    private int level;

    public Tower(string sTowerType = "중거리타워", int sRare=1, int sMaxLevel=5, float sDamage=20, float sRange=4, float sAttackSpeed=3, int sIndex=1, float sCost=10)
    {
        myType = sTowerType;
        rare = sRare; maxLevel = sMaxLevel; damage = sDamage; range = sRange; attackSpeed = sAttackSpeed; index = sIndex; cost = sCost;
        level = 1;
        cellCost = 5;
    }


    public void SetStatus(string sTowerType = "중거리타워", int sRare = 1, int sMaxLevel = 5, float sDamage = 20, float sRange = 4, float sAttackSpeed = 3, int sIndex = 1, float sCost = 10)
    {
        myType = sTowerType;
        rare = sRare; maxLevel = sMaxLevel; damage = sDamage; range = sRange; attackSpeed = sAttackSpeed; index = sIndex; cost = sCost;
    }

    public string GetTowerType()
    {
        return myType;
    }
    public int GetRare()
    {
        return rare;
    }
    public int GetMaxLevel()
    {
        return maxLevel;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetRange()
    {
        return range;
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }
    public float GetIndex()
    {
        return index;
    }
    public float GetCost()
    {
        return cost;
    }
    public int GetLevel() 
    {
        return level;
    } 

    public string SetTowerType(string sType)
    {
        myType = sType;
        return myType;
    }
    public int SetRare(int sRare)
    {
        rare = sRare;
        return rare;
    }
    public int SetMaxLevel(int sMaxLevel)
    {
        maxLevel = sMaxLevel;
        return maxLevel;
    }
    public float SetDamage(float sDamage)
    {
        damage = sDamage;
        return damage;
    }
    public float SetRange(float sRange)
    {
        range = sRange;
        return range;
    }
    public float SetAttackSpeed(float sAttackSpeed)
    {
        attackSpeed = sAttackSpeed;
        return attackSpeed;
    }
    public float SetIndex(int sIndex)
    {
        index = sIndex;
        return index;
    }
    public float SetCost(float sCost)
    {
        cost = sCost;
        return cost;
    }
    public int SetLevel(int sLevel) 
    {
        level = sLevel;
        return level;
    }
    public int GetSellCost() 
    {
        return cellCost;
    }
    public int SetSellCost(int pCost) 
    {
        cellCost += pCost;
        return cellCost;
    }
}
