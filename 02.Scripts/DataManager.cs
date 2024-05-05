using Unity.VisualScripting;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // 모든 데이터를 저장하고 사용하는 Script로 singleTon패턴을 이용해 처음 load한 뒤로 계속 사용한다.
    private static DataManager instance;
    private static object mylock = new object();

    public TextAsset monsterData; // CSV 파일을 저장할 변수
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


    public TextAsset TowerData; // CSV 파일을 저장할 변수

    private string[] towerType;
    private int[] towerRare;
    private int[] towerMaxLevel;
    private float[] towerDamage;
    private float[] towerRange;
    private float[] towerAttackSpeed;
    private int[] towerIndex; //타워 종류에 대한 Index
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

    public static DataManager Instance //Instance접근
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
                        // 씬에 DataManager가 없을 경우 생성
                        GameObject obj = new GameObject("DataManager");
                        instance = obj.AddComponent<DataManager>();
                    }

                    DontDestroyOnLoad(instance.gameObject);
                    // 객체 리턴               
                }
            }
            return instance;
        }
    }

    void DataManagerInitailize() 
    {
        LoadMonsterData(); //데이터 먼저 가져오고 null 체크하기
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
        for (int i = 1; i < data.Length; i++) // 첫 번째 줄은 열의 이름이므로 1부터 시작
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
        //Debug.Log("지금 레벨은 " + level);
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
        for (int i = 1; i < data2.Length; i++) // 첫 번째 줄은 열의 이름이므로 1부터 시작
        {
            string[] row = data2[i].Split(',');

            towerType[i]=row[0];
            towerRare[i]=int.Parse(row[1]);
            towerMaxLevel[i]= int.Parse(row[2]);
            towerDamage[i]= float.Parse(row[3]);
            towerRange[i]= float.Parse(row[4]);
            towerAttackSpeed[i]= float.Parse(row[5]);
            towerIndex[i]= int.Parse(row[6]); //타워 종류에 대한 Index
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
        towerIndex = new int[dataCnt]; //타워 종류에 대한 Index
        towerCost = new float[dataCnt];
    }

    public Tower GetTowerData(int tIndex)
    {
        Tower returnTower = new Tower(towerType[tIndex], towerRare[tIndex], towerMaxLevel[tIndex], towerDamage[tIndex],
            towerRange[tIndex], towerAttackSpeed[tIndex], towerIndex[tIndex], towerCost[tIndex]);
        return returnTower;
    }

}
