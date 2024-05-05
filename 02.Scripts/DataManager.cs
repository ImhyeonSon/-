using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // ��� �����͸� �����ϰ� ����ϴ� Script�� singleTon������ �̿��� ó�� load�� �ڷ� ��� ����Ѵ�.
    private static DataManager instance;
    private static object mylock = new object();

    public TextAsset monsterData; // CSV ������ ������ ����
    bool isInitailize = false; // 

    private int[] monsterLevel;
    private string[] monsterName;
    private float[] monsterHealth;
    private float[] monsterDamage;
    private float[] monsterDefense;
    private float[] monsterSpeed;
    private float[] monsterAttackSpeed;
    private int[] monsterCount;
    private float[] monsterGenTime;
    private int[] monsterKillCount;


    public TextAsset TowerData; // CSV ������ ������ ����

    private string[] towerType;
    private int[] towerRare;
    private int[] towerMaxLevel;
    private float[] towerDamage;
    private float[] towerRange;
    private float[] towerAttackSpeed;
    private int[] towerIndex; //Ÿ�� ������ ���� Index
    private float[] towerCost;

    private void Awake()
    {
        
        DataManagerInitailize();
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    private DataManager() 
    {
    }

    public static DataManager Instance //Instance����
    {
        get
        {
            // thread-safe
            lock (mylock) { 
                if (instance == null)
                {
                    instance =GameObject.Find("DataManager").GetComponent<DataManager>();
                    //Debug.Log("instance :" + instance);
                    if (instance == null)
                    {
                        // ���� DataManager�� ���� ��� ����
                        GameObject obj = new GameObject("DataManager");
                        instance = obj.AddComponent<DataManager>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                    // ��ü ����               
                }
            }
            return instance;
        }
    }

    void DataManagerInitailize() 
    {
        LoadMonsterData(); //������ ���� �������� null üũ�ϱ�
        LoadTowerData();
        //if (instance == null)
        //{
        //    instance = this;
        //    DontDestroyOnLoad(this.gameObject);
        //}
        //else if 
        //{
        //    Destroy(gameObject);
        //}
    }


    void Start()
    {
/*        LoadMonsterData();*/
    }

    void LoadMonsterData()
    {
        string[] data = monsterData.text.Split('\n');
        SetMonsterData(data.Length+1);
        for (int i = 1; i < data.Length; i++) // ù ��° ���� ���� �̸��̹Ƿ� 1���� ����
        {
            string[] row = data[i].Split(',');

            monsterLevel[i] = int.Parse(row[0]);
            monsterName[i] = row[1];
            monsterHealth[i] = float.Parse(row[2]);
            monsterDamage[i] = float.Parse(row[3]);
            monsterDefense[i] = float.Parse(row[4]);
            monsterSpeed[i] = float.Parse(row[5]);
            monsterAttackSpeed[i] = float.Parse(row[6]);
            monsterCount[i] = int.Parse(row[7]);
            monsterGenTime[i]= float.Parse(row[8]);
            monsterKillCount[i]= int.Parse(row[9]);
}
    }

    void SetMonsterData(int dataCnt) 
    {
        monsterLevel = new int[dataCnt];
        monsterName = new string[dataCnt];
        monsterHealth = new float[dataCnt];
        monsterDamage = new float[dataCnt];
        monsterDefense = new float[dataCnt];
        monsterSpeed = new float[dataCnt];
        monsterAttackSpeed = new float[dataCnt];
        monsterCount = new int[dataCnt];
        monsterGenTime = new float[dataCnt];
        monsterKillCount = new int[dataCnt];
    }

    public Monster GetMonsterData(int level)
    {
        //Debug.Log("���� ������ " + level);
        //Debug.Log("dOdi?" + monsterLevel.Length);
        Monster returnMonster = new Monster(monsterLevel[level], monsterName[level], monsterHealth[level], monsterDamage[level], monsterDefense[level], monsterSpeed[level], monsterAttackSpeed[level], monsterCount[level], monsterGenTime[level], monsterKillCount[level]);
        return returnMonster;
    }


    /// <summary>
    /// tower Initialize
    /// </summary>
    void LoadTowerData()
    {
        string[] data2 = TowerData.text.Split('\n');
        SetTowerData(data2.Length + 1);
        for (int i = 1; i < data2.Length; i++) // ù ��° ���� ���� �̸��̹Ƿ� 1���� ����
        {
            string[] row = data2[i].Split(',');

            towerType[i]=row[0];
            towerRare[i]=int.Parse(row[1]);
            towerMaxLevel[i]= int.Parse(row[2]);
            towerDamage[i]= float.Parse(row[3]);
            towerRange[i]= float.Parse(row[4]);
            towerAttackSpeed[i]= float.Parse(row[5]);
            towerIndex[i]= int.Parse(row[6]); //Ÿ�� ������ ���� Index
            towerCost[i]= float.Parse(row[7]);
        }
    }

    void SetTowerData(int dataCnt)
    {
        towerType = new string[dataCnt];
        towerRare = new int[dataCnt];
        towerMaxLevel = new int[dataCnt];
        towerDamage = new float[dataCnt];
        towerRange = new float[dataCnt];
        towerAttackSpeed = new float[dataCnt];
        towerIndex = new int[dataCnt]; //Ÿ�� ������ ���� Index
        towerCost = new float[dataCnt];
    }

    public Tower GetTowerData(int tIndex)
    {
        Tower returnTower = new Tower(towerType[tIndex], towerRare[tIndex], towerMaxLevel[tIndex], towerDamage[tIndex],
            towerRange[tIndex], towerAttackSpeed[tIndex], towerIndex[tIndex], towerCost[tIndex]);
        return returnTower;
    }

}
