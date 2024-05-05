using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerSeatManager : MonoBehaviour // 사실상 TowerManager다.
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
        SetTowerSeat(); //초기화
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
        TowerSeatList[idx] = TowerObject; //오브젝트를 할당
    }


    GameObject nowSelectTower; // 판매시 null로 바꿔주기
    GameObject hitObject;
    Transform towerTrans;
    bool dragging = false;//drag중인지 판단하는 변수
    float dist;
    Vector3 offset;
    int nowTowerIndex; // 선택한 타워의 index
    Tower nowTowerInfo;

    void TowerMove()
    {
        //if (Input.GetMouseButtonDown(0))
        //{

        //}
        // 모바일 드래그앤 드롭
        Vector3 vec;
        if (Input.touchCount != 1)
        {
            // drag 중이던 오브젝트가 원래 자리로 돌아가도록 구현
            if (dragging&&nowSelectTower!=null) 
            {
                nowSelectTower.transform.position = TowerSeatTransform[nowTowerIndex].position;
                nowSelectTower.GetComponent<TowerBehavior>().SetDragging(false); // 드래깅 해제
            }
            dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 tPos = touch.position;

        if (touch.phase == TouchPhase.Began) // 첫 터치라면
        {
            Ray ray = Camera.main.ScreenPointToRay(tPos);// 터치된 곳에 광선을 쏜다.
            RaycastHit hit;
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.tag == "Tower")
                {
                    hitObject = hit.collider.gameObject;
                    if (nowSelectTower != null) // 이전에 선택된 적이 있다면,
                    {
                        nowSelectTower.GetComponent<TowerBehavior>().OffSelectEffect();
                    }
                    nowSelectTower = hitObject;
                    towerTrans = hitObject.transform;
                    vec = new Vector3(tPos.x, tPos.y, 0); //터치한 포지션의 x, y값
                    vec = Camera.main.ScreenToWorldPoint(vec);
                    offset = towerTrans.position - vec; // 첫 위치와 스크린으로 바꾼 원드좌표 값

                    dragging = true;
                    //드래깅 on
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
            if (tempTowerIndex == -1)//방문하지 않도록
            {
                hitObject.transform.position = TowerSeatTransform[nowTowerIndex].position; //원래 그대로 위치
            }
            else 
            {
                // 이동할 곳에 있는 Tower와 tempBool
                changeTower = TowerSeatList[tempTowerIndex];

                // 지금 선택한 타워
                TowerSeatList[tempTowerIndex] = hitObject;
                TowerSeatList[nowTowerIndex] = changeTower;

                // 위치 옮기기
                hitObject.transform.position = TowerSeatTransform[tempTowerIndex].position;
                if (changeTower!=null) //비어있지 않다면 실행
                {
                    changeTower.transform.position = TowerSeatTransform[nowTowerIndex].position;
                }
                // 자리 차 있는지 체크하는 것 바꾸기
                IsUseTowerSeat[nowTowerIndex] = IsUseTowerSeat[tempTowerIndex];
                IsUseTowerSeat[tempTowerIndex] = true; //이동 했으므로 true이다.
            }

            // 드래깅 해제
            hitObject.GetComponent<TowerBehavior>().SetDragging(false);

            dragging = false;
            hitObject = null;
        }
    }

    public int GetTowerIndex(GameObject nowTower) 
    {
        int towerIndex = -1;
        float minDist = 1.5f; //너무 거리가 멀면 이동을 하지 않도록 한다.
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
        //Debug.Log("선택한 타워 Index : " + towerIndex);
        return towerIndex;
    }

    // TowerSeatManager에서 Upgrade도 관리한다.


    public void SelectedTower() 
    {
        // 타워 선택시 UI 업데이트
        UM.OnTowerUI(nowSelectTower);
        nowSelectTower.GetComponent<TowerBehavior>().OnSelectEffect();        
    }

    public GameObject UpgradePopUp;
    public TextMeshProUGUI UpgradeText;
    public void TowerUpgradePopUp() {
        nowTowerInfo = nowSelectTower.GetComponent<TowerBehavior>().GetNowTowerInfo();
        int nowCost = (int)nowTowerInfo.GetCost() * (nowTowerInfo.GetLevel() + 6) / 6;
        UpgradePopUp.SetActive(true);
        UpgradeText.text = nowCost + "원을 사용해 Upgrade 하시겠습니까?\n(현재 money : " + PM.GetNowMoney() + ")";
    }

    public void CloseTowerUpgradePopUp() 
    {
        UpgradePopUp.SetActive(false);
    }
    public void TowerUpgrade() 
    {
        CloseTowerUpgradePopUp();
        // nowTower는 업그레이드 버튼을 눌렀을 때 이미 정해진다.
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
                nowTowerInfo.SetSellCost((nowCost-2)/ 2); // 타워 판매 비용 계산
                UM.SetTowerInfomation(nowTowerInfo);
            }
            else
            {
                // 돈 없다는 팝업
                MoneyLossPanel.SetActive(true);
            }
        }
        else 
        {
            MaxLevelPanel.SetActive(true);
            // max Level에 도달했다는 팝업
        }
    }

    public void MaxLevePanelClosePopUp() 
    {
        MaxLevelPanel.SetActive(false);
    }

    public GameObject SellTowerPanel;
    public TextMeshProUGUI SellTowerText;

    Tower SellTowerInfo; //순간 select 돼 버그가 생기는 문제를 방지하기 위함
    GameObject SellTowerObject;
    int sellTowerIndex;
    public void SellTowerPopUp()
    {
        SellTowerInfo = nowSelectTower.GetComponent<TowerBehavior>().GetNowTowerInfo();
        SellTowerObject = nowSelectTower;
        SellTowerText.text = "타워를 판매해 " + (SellTowerInfo.GetSellCost()-1) + " Gold를 얻겠습니까?\nLuck 이 높을수록 돈을 더 많이 돌려받을 수 있습니다.";
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
        // 선택 정보도 없애주기
        SellTowerObject = null;
        SellTowerInfo = null;
        //추가 작성
        IsUseTowerSeat[sellTowerIndex] =false;
        TowerSeatList[sellTowerIndex] =null;
        SellTowerPopUpClose();
        TowerPannel.SetActive(false);
    }


}
