using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSeatManager : MonoBehaviour // ��ǻ� TowerManager��.
{

    public UIManager UM;
    public PlayerManager PM;
    Transform[] TowerSeatTransform;
    bool[] IsUseTowerSeat;
    GameObject[] TowerSeatList;

    public GameObject MoneyLossPanel;
    public GameObject MaxLevelPanel;
    public GameObject TowerPannel;
    // Start is called before the first frame update
    private void Awake()
    {
        
    }
    void Start()
    {
        SetTowerSeat(); //�ʱ�ȭ
    }

    // Update is called once per frame
    void Update()
    {
        TowerMove();
    }



    void SetTowerSeat()
    {
        Transform[] newTransform = GetComponentsInChildren<Transform>();
        IsUseTowerSeat = new bool[24];
        TowerSeatTransform = new Transform[24];
        TowerSeatList = new GameObject[24];
        for (int i = 1; i < 25; i++)
        {
            TowerSeatTransform[i - 1] = newTransform[i];
            IsUseTowerSeat[i - 1] = false;
        }
    }

    public Transform[] GetTowerSeatTransform()
    {
        return TowerSeatTransform;
    }
    public bool[] GetIsUseTowerSeat()
    {
        return IsUseTowerSeat;
    }

    public void SetIsUseTowerSeat(int idx, bool TF, GameObject TowerObject)
    {
        IsUseTowerSeat[idx]=TF;
        TowerSeatList[idx] = TowerObject; //������Ʈ�� �Ҵ�
    }


    GameObject nowSelectTower; // �ǸŽ� null�� �ٲ��ֱ�
    GameObject hitObject;
    Transform towerTrans;
    bool dragging = false;//drag������ �Ǵ��ϴ� ����
    float dist;
    Vector3 offset;
    int nowTowerIndex; // ������ Ÿ���� index
    Tower nowTowerInfo;

    void TowerMove()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //}
        // ����� �巡�׾� ���
        Vector3 vec;
        if (Input.touchCount != 1)
        {
            // drag ���̴� ������Ʈ�� ���� �ڸ��� ���ư����� ����
            if (dragging&&nowSelectTower!=null) 
            {
                nowSelectTower.transform.position = TowerSeatTransform[nowTowerIndex].position;
                nowSelectTower.GetComponent<TowerBehavior>().SetDragging(false); // �巡�� ����
            }
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 tPos = touch.position;

        if (touch.phase == TouchPhase.Began) // ù ��ġ���
        {
            Ray ray = Camera.main.ScreenPointToRay(tPos);// ��ġ�� ���� ������ ���.
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Tower")
                {
                    hitObject = hit.collider.gameObject;
                    if (nowSelectTower != null) // ������ ���õ� ���� �ִٸ�,
                    {
                        nowSelectTower.GetComponent<TowerBehavior>().OffSelectEffect();
                    }
                    nowSelectTower = hitObject;
                    towerTrans = hitObject.transform;
                    vec = new Vector3(tPos.x, tPos.y, 0); //��ġ�� �������� x, y��
                    vec = Camera.main.ScreenToWorldPoint(vec);
                    offset = towerTrans.position - vec; // ù ��ġ�� ��ũ������ �ٲ� ������ǥ ��

                    dragging = true;
                    //�巡�� on
                    hitObject.GetComponent<TowerBehavior>().SetDragging(true);

                    nowTowerIndex = GetTowerIndex(hitObject);
                    SelectedTower();
                }
                else if (hit.collider.gameObject.tag == "Player") 
                {
                    UM.OnPlayerUI();
                }
            }
        }

        if (dragging && touch.phase == TouchPhase.Moved)
        {
            vec = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            vec = Camera.main.ScreenToWorldPoint(vec);
            Vector3 nV = new Vector3(vec.x + offset.x, towerTrans.position.y, vec.z + offset.z);
            towerTrans.position = nV;
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            int tempTowerIndex = GetTowerIndex(hitObject);
            GameObject changeTower;
            if (tempTowerIndex == -1)//�湮���� �ʵ���
            {
                hitObject.transform.position = TowerSeatTransform[nowTowerIndex].position; //���� �״�� ��ġ
            }
            else 
            {
                // �̵��� ���� �ִ� Tower�� tempBool
                changeTower = TowerSeatList[tempTowerIndex];

                // ���� ������ Ÿ��
                TowerSeatList[tempTowerIndex] = hitObject;
                TowerSeatList[nowTowerIndex] = changeTower;

                // ��ġ �ű��
                hitObject.transform.position = TowerSeatTransform[tempTowerIndex].position;
                if (changeTower!=null) //������� �ʴٸ� ����
                {
                    changeTower.transform.position = TowerSeatTransform[nowTowerIndex].position;
                }
                // �ڸ� �� �ִ��� üũ�ϴ� �� �ٲٱ�
                IsUseTowerSeat[nowTowerIndex] = IsUseTowerSeat[tempTowerIndex];
                IsUseTowerSeat[tempTowerIndex] = true; //�̵� �����Ƿ� true�̴�.
            }

            // �巡�� ����
            hitObject.GetComponent<TowerBehavior>().SetDragging(false);

            dragging = false;
            hitObject = null;
        }
    }

    public int GetTowerIndex(GameObject nowTower) 
    {
        int towerIndex = -1;
        float minDist = 1.5f; //�ʹ� �Ÿ��� �ָ� �̵��� ���� �ʵ��� �Ѵ�.
        float tempD;
        for (int i = 0; i < 24; i++)
        {
            tempD = Vector3.Distance(nowTower.transform.position, TowerSeatTransform[i].position);
            if (minDist > tempD) 
            {
                towerIndex = i;
                minDist = tempD;
            }
        }
        //Debug.Log("������ Ÿ�� Index : " + towerIndex);
        return towerIndex;
    }

    // TowerSeatManager���� Upgrade�� �����Ѵ�.


    public void SelectedTower() 
    {
        // Ÿ�� ���ý� UI ������Ʈ
        UM.OnTowerUI(nowSelectTower);
        nowSelectTower.GetComponent<TowerBehavior>().OnSelectEffect();        
    }

    public GameObject UpgradePopUp;
    public TextMeshProUGUI UpgradeText;
    public void TowerUpgradePopUp() {
        nowTowerInfo = nowSelectTower.GetComponent<TowerBehavior>().GetNowTowerInfo();
        int nowCost = (int)nowTowerInfo.GetCost() * (nowTowerInfo.GetLevel() + 6) / 6;
        UpgradePopUp.SetActive(true);
        UpgradeText.text = nowCost + "���� ����� Upgrade �Ͻðڽ��ϱ�?\n(���� money : " + PM.GetNowMoney() + ")";
    }

    public void CloseTowerUpgradePopUp() 
    {
        UpgradePopUp.SetActive(false);
    }
    public void TowerUpgrade() 
    {
        CloseTowerUpgradePopUp();
        // nowTower�� ���׷��̵� ��ư�� ������ �� �̹� ��������.
        int nowCost = (int)nowTowerInfo.GetCost() * (nowTowerInfo.GetLevel() + 6) / 6;
        bool towerTF = (nowCost<=PM.GetNowMoney());
        if (nowTowerInfo.GetMaxLevel() > nowTowerInfo.GetLevel())
        {
            if (towerTF)
            {
                PM.SpendMoney(nowCost);
                int SuccessCnt= PM.GetSuccess();
                float plusDamage = nowTowerInfo.GetDamage() / nowTowerInfo.GetLevel();
                if (SuccessCnt == 1)
                {
                    nowTowerInfo.SetDamage((int)nowTowerInfo.GetDamage()+plusDamage);
                }
                else if (SuccessCnt == 2)
                {
                    nowTowerInfo.SetDamage((int)nowTowerInfo.GetDamage()+(plusDamage*1.5f));
                }
                else
                {
                    nowTowerInfo.SetDamage((int)nowTowerInfo.GetDamage() + (plusDamage*0.75f));
                }
                nowTowerInfo.SetLevel(nowTowerInfo.GetLevel()+1);
                nowTowerInfo.SetSellCost((nowCost-2)/ 2); // Ÿ�� �Ǹ� ��� ���
                UM.SetTowerInfomation(nowTowerInfo);
            }
            else
            {
                // �� ���ٴ� �˾�
                MoneyLossPanel.SetActive(true);
            }
        }
        else 
        {
            MaxLevelPanel.SetActive(true);
            // max Level�� �����ߴٴ� �˾�
        }
    }

    public void MaxLevePanelClosePopUp() 
    {
        MaxLevelPanel.SetActive(false);
    }

    public GameObject SellTowerPanel;
    public TextMeshProUGUI SellTowerText;

    Tower SellTowerInfo; //���� select �� ���װ� ����� ������ �����ϱ� ����
    GameObject SellTowerObject;
    int sellTowerIndex;
    public void SellTowerPopUp()
    {
        SellTowerInfo = nowSelectTower.GetComponent<TowerBehavior>().GetNowTowerInfo();
        SellTowerObject = nowSelectTower;
        SellTowerText.text = "Ÿ���� �Ǹ��� " + (SellTowerInfo.GetSellCost()-1) + " Gold�� ��ڽ��ϱ�?\nLuck �� �������� ���� �� ���� �������� �� �ֽ��ϴ�.";
        SellTowerPanel.SetActive(true);
        sellTowerIndex = GetTowerIndex(SellTowerObject);

    }
    public void SellTowerPopUpClose()
    {
        SellTowerPanel.SetActive(false);
    }

    public void SellTower() 
    {
        PM.GetMoney(SellTowerInfo.GetSellCost()-1);
        Destroy(SellTowerObject, 0f);
        nowSelectTower = null;
        nowTowerInfo = null;
        // ���� ������ �����ֱ�
        SellTowerObject = null;
        SellTowerInfo = null;
        //�߰� �ۼ�
        IsUseTowerSeat[sellTowerIndex] =false;
        TowerSeatList[sellTowerIndex] =null;
        SellTowerPopUpClose();
        TowerPannel.SetActive(false);
    }


}
