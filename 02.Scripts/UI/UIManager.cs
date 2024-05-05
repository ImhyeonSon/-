using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{

    public GameObject PlayerUI;
    public GameObject TowerUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame


    public void OnPlayerUI()
    { 
        PlayerUI.SetActive(true);
        TowerUI.SetActive(false);
    }

    public void OnTowerUI(GameObject towerObject) 
    {
        PlayerUI.SetActive(false);
        TowerUI.SetActive(true);
        Tower nowTowerInfo = towerObject.GetComponent<TowerBehavior>().GetNowTowerInfo();
        SetTowerInfomation(nowTowerInfo);

    }


    public TextMeshProUGUI rareText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI costText;
    public GameObject ImageB;
    public GameObject ImageA;
    public GameObject ImageS;

    public void SetTowerInfomation(Tower sTower) 
    {
        // tower 정보를 여기에 작성
        string rs;
        if (sTower.GetRare() == 1){ 
            ImageB.SetActive(true);
            ImageA.SetActive(false);
            ImageS.SetActive(false);
            rs = "B"; 
        } else if (sTower.GetRare() == 2) {
            ImageB.SetActive(false);
            ImageA.SetActive(true);
            ImageS.SetActive(false);
            rs = "A";
        } else {
            ImageB.SetActive(false);
            ImageA.SetActive(false);
            ImageS.SetActive(true);
            rs = "S";
        }
        rareText.text=rs;
        levelText.text= "Level : " + sTower.GetLevel()+"/"+sTower.GetMaxLevel();
        nameText.text=""+sTower.GetTowerType();
        damageText.text="Damage : "+(int)sTower.GetDamage();
        rangeText.text="Range : "+sTower.GetRange();
        attackSpeedText.text="speed : "+sTower.GetAttackSpeed();
        costText.text= ((int)sTower.GetCost() * (sTower.GetLevel() + 6) / 6) + " Gold";
    }

    public GameObject TimerPannel;
    public TextMeshProUGUI TimerText;
    public void TimerUI(float nowTime)
    {
        TimerText.text ="time : "+nowTime.ToString("N2");
    }

    public void TimerOnOff(bool TF) 
    {
        TimerPannel.SetActive(TF);
    }
}
