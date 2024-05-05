using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MonsterUIPannel : MonoBehaviour
{
    // Start is called before the first frame update
    Monster nowMonster=new Monster();

    public TextMeshProUGUI monsterHealth;
    public TextMeshProUGUI monsterDamage;
    public TextMeshProUGUI monsterDefence;
    public TextMeshProUGUI monsterSpeed;
    public TextMeshProUGUI monsterAttackSpeed;
    public TextMeshProUGUI monsterName;
  

    public GameObject MonsterInfoPanel;

    public GameObject BabySlime;
    public GameObject DefaultSlime;
    public GameObject BabyLeafSlime;
    public GameObject LeafSlime;
    public GameObject KingSlime;

    public GameObject RabbitSlime;
    public GameObject HalmetSlime;
    public GameObject KingSlime_2;
    public GameObject VikingSlime;

    private void Start()
    {
        DataInitialize();
    }
    public void PopUpClick() 
    {
        MonsterInfoPanel.SetActive(!MonsterInfoPanel.activeSelf);
    }

    public void SetNowMonster(Monster nowMonsterData)
    {
        nowMonster = nowMonsterData;
        DataInitialize();
    }

    void DataInitialize() 
    {
        monsterHealth.text="Health : "+nowMonster.GetMaxHealth();
        monsterDamage.text="Damage : "+nowMonster.GetDamage();
        monsterDefence.text="Defence : "+nowMonster.GetDefense();
        monsterSpeed.text="Speed : "+nowMonster.GetSpeed();
        monsterAttackSpeed.text="AttackSpeed : "+nowMonster.GetAttackSpeed();
        SelectSlime((nowMonster.GetLevel() - 1) / 5);
    }

    public void SelectSlime(int caseNum)
    {
        BabySlime.SetActive(false);
        DefaultSlime.SetActive(false);
        BabyLeafSlime.SetActive(false);
        LeafSlime.SetActive(false);
        KingSlime.SetActive(false);
        RabbitSlime.SetActive(false);
        HalmetSlime.SetActive(false);
        KingSlime_2.SetActive(false);
        VikingSlime.SetActive(false);
        GameObject tempObject;
        switch (caseNum)
        {
            case 0:
                monsterName.text = "BabySlime";
                tempObject = BabySlime;
                break;
            case 1:
                monsterName.text = "NormalSlime";
                tempObject = DefaultSlime;
                break;
            case 2:
                monsterName.text = "BabyLeaflSlime";
                tempObject = BabyLeafSlime;
                break;
            case 3:
                monsterName.text = "LeaflSlime";
                tempObject = LeafSlime;
                break;
            case 4:
                monsterName.text = "KingSlime";
                tempObject = KingSlime;
                break;
            case 5:
                monsterName.text = "RabbitSlime";
                tempObject = RabbitSlime;
                break;
            case 6:
                monsterName.text = "MetalSlime";
                tempObject = HalmetSlime;
                break;
            case 7:
                monsterName.text = "KingSlime V2";
                tempObject = KingSlime_2;
                break;
            case 8:
                monsterName.text = "VikingSlime";
                tempObject = VikingSlime;
                break;
            default:
                monsterName.text = "NormalSlime";
                tempObject = DefaultSlime;
                break;
        }
        tempObject.SetActive(true);
    }

}
