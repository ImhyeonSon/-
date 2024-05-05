using UnityEngine;
using System.Collections;
using System.Data.SqlTypes;


public class TowerGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    // ���߿� ui�� �ٲܿ���
    public GameObject MaxTowerPanel;

    public AnimationCurve BonousCurve;
    DataManager DM;
    public TowerSeatManager TSM;
    public PlayerManager PM;

    public GameObject TowerPrefab;
    private void Awake()
    {
        PM = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>(); 
        DM = DataManager.Instance;
    }

    void Start()
    {

    }

    public int GetRare() 
    {
        return PM.GetSuccess();
    }

    public void MakeTower() //Random����
    {
        if (PM.GetNowMoney()>=10) //���Ű� �����ϴٸ�
        {
            int RandomTower = Random.Range(0, 5);
            int TowerRare = GetRare();
            float RandomBonus = GetCurveBonusRandom();
            Tower sTower = DM.GetTowerData(RandomTower * 3 + TowerRare+1);

            sTower.SetDamage((1f + RandomBonus) * sTower.GetDamage());
            Debug.Log(sTower.GetTowerType() + " " + sTower.GetDamage());
            Vector3 GeneratorSeat = new Vector3();
            bool flag = false;
            int seatIndex = 0;
            Transform[] TowerSeatTransform = TSM.GetTowerSeatTransform();
            bool[] IsUseTowerSeat = TSM.GetIsUseTowerSeat();
            for (int i = 0; i < TowerSeatTransform.Length; i++)
            {
                if (!IsUseTowerSeat[i])
                {
                    GeneratorSeat = TowerSeatTransform[i].position;
                    flag = true;
                    seatIndex = i;
                    break;
                }
            }
            if (flag)
            {
                PM.SpendMoney(10);
                IsUseTowerSeat[seatIndex] = true;
                GameObject newTower = Instantiate(TowerPrefab, GeneratorSeat, Quaternion.identity);
                TSM.SetIsUseTowerSeat(seatIndex, true, newTower);
                newTower.GetComponent<TowerBehavior>().SetNowTower(sTower);
            }
            else
            {
                //������ �ȵȴٴ� �˾� �޼��� ǥ��
                MaxTowerPanel.SetActive(true);
            }
        }
        else 
        {
            // ���Ű� �Ұ���
            PM.MoneyPopUp();
        }
    }

    public float GetCurveBonusRandom()
    {
        return BonousCurve.Evaluate(Random.value);
    }


    public void MaxTowerGeneratorClosePopUp()
    {
        MaxTowerPanel.SetActive(false);
    }
}
