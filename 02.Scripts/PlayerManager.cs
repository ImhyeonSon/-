using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    // 업그레이드 비용을 수치가 높아질 수록 비싸도록 설계하기.
    int money;
    int luck;
    float damage;
    float health;
    float maxHP;
    float range;
    float healValue;
    float defence;
    float attackSpeed;

    int killCount;
    int allKillcount;

    int luckGage;
    int successCnt;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI luckText;
    public TextMeshProUGUI damageText;
    public TextMeshProUGUI hpText;
    public TextMeshProUGUI defenceText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI attackSpeedText;
    public TextMeshProUGUI healText;


    bool isDie = false;

    Timer attackTimer = new Timer();
    Timer healTimer = new Timer();



    public RoundManager RM;
    GameObject MM; //MonsterManager
    private void Awake()
    {
        MM = GameObject.Find("MonsterManager");
    }

    void Start()
    {
        SetInitailize();
    }
    void SetInitailize()
    {
        money = 20; luck = 0;
        damage = 2; health = 40f;
        range = 4f; healValue = 1f;
        maxHP = 40f; defence = 0f; killCount = 0; allKillcount = 0;
        attackSpeed = 100f; // 기본이 100 빠르면 최대 300
        attackTimer.TimeDelaySet(150f / attackSpeed);
        healTimer.TimeDelaySet(10f);
        SetMoneyText();
        SetLuckText();
        SetDamageText();
        SetHpText();
        SetDefenceText();
        SetRangeText();
        SetAttackSpeedText();
        SetHealText();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDie)
        {
            PlayerAttack();
            PlayerHeal();
        }

        //테스트용 버튼
        if (Input.GetKeyDown(KeyCode.J))
        {
            GetMoney(10);
        }
        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    luck = 80;
        //}
        if (Input.GetKeyDown(KeyCode.D))
        {
            health = 1f;
        }
    }


    public void GetKillCount(int kCount)
    {
        allKillcount += 1; // 실제 죽인 몬스터 수
        // PM은 항상 그대로 이므로 몬스터의 수 처리는 여기서 담당한다.
        if (!isDie)
        {
            RM.KillMonster();

            killCount += kCount; // 돈을 주는 count로 보스는 kCount가 높다.
            // 돈
            if (killCount >= 5)
            {
                for (int i = 0; i < killCount / 5; i++)
                {
                    GetMoney(5);
                }
                killCount %= 5;
            }
        }
    }

    public int GetNowMoney()
    {
        return money;
    }

    public void GetMoney(int gMoney)
    {

        successCnt = GetSuccess();
        if (successCnt == 1)
        {
            money += (int)((float)gMoney * 1.5f);
        }
        else if (successCnt == 2)
        {
            money += gMoney * 2;
        }
        else
        {
            money += gMoney;
        }
        //Debug.Log("변경후 돈 : " + money);
        SetMoneyText();
        // Player의 현재 Money를 갱신하기
    }

    public int GetNowLuck()
    {
        int NowLuck = Random.Range(luck, 100);
        return NowLuck;
    }

    public int GetSuccess()
    {
        int nowLuck = GetNowLuck();
        if (nowLuck > 90)
        {
            return 2;
        }
        else if (nowLuck > 60)
        {
            return 1;
        }
        return 0;
    }

    public void UpgradeLuck() {
        bool luckTF = SpendMoney((luck / 10)*3 + 4);
        if (luckTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                luck += 2;
            }
            else if (successCnt == 2)
            {
                luck += 3;
            }
            else
            {
                luck += 1;
            }
            SetLuckText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }
    public void UpgradeDamage() {
        bool damageTF = SpendMoney((int)(damage / 100) + 1);
        if (damageTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                damage += 4;
            }
            else if (successCnt == 2)
            {
                damage += 6;
            }
            else
            {
                damage += 2;
            }
            SetDamageText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }
    public void UpgradeHealth() {
        bool hpTF = SpendMoney((int)(maxHP / 100) + 1);
        if (hpTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                health += 7;
                maxHP += 7;
            }
            else if (successCnt == 2)
            {
                health += 10;
                maxHP += 10;
            }
            else
            {
                health += 5;
                maxHP += 5;
            }
            SetHpText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }
    public void UpgradeRange() {
        bool rangeTF = SpendMoney((int)range + 2);
        if (rangeTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                range += 0.375f;
            }
            else if (successCnt == 2)
            {
                range += 0.5f;
            }
            else
            {
                range += 0.25f;
            }
            SetRangeText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }
    public void UpgradeHealthValue() {
        bool healTF = SpendMoney(3);
        if (healTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                healValue += 1.5f;
            }
            else if (successCnt == 2)
            {
                healValue += 2f;

            }
            else
            {
                healValue += 1f;
            }
            SetHealText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }

    public void UpgradeDefence()
    {
        bool defenceTF = SpendMoney((int)defence + 1);
        if (defenceTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                defence += 1f;
            }
            else if (successCnt == 2)
            {
                defence += 1.5f;
            }
            else
            {
                defence += 0.5f;
            }
            SetDefenceText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }

    public void UpgradeAttackSpeed()
    {
        bool speedTF = SpendMoney((int)(attackSpeed / 100) + 1);
        if (speedTF)
        {
            successCnt = GetSuccess();
            if (successCnt == 1)
            {
                attackSpeed += 1f;
            }
            else if (successCnt == 2)
            {
                attackSpeed += 1.5f;
            }
            else
            {
                attackSpeed += 0.5f;
            }
            attackTimer.TimeDelaySet(100f / attackSpeed);
            SetAttackSpeedText();
        }
        else
        {
            MoneyPopUp();
        }
        CloseMoneyUI();
    }

    public float SetDamage(float sDamage)
    {
        if (!isDie)
        {
            health -= Mathf.Max(1, sDamage-defence);
            if (health <= 0f)
            {
                //Die실행
                isDie = true;
                Debug.Log(" 게임오버");
                GameOverPopUp();
            }
        }
        SetHpText();
        return health;
    }

    // 어디서 돈이 부족한지 체크를 할 지 여기서 선택해 주어야함.
    public bool SpendMoney(int sMoney)
    {
        if (money - sMoney >= 0)
        {
            money -= sMoney;
            SetMoneyText();
            return true;
        }
        else
        {
            // 돈이 부족하다는 팝업 띄우기
            Debug.Log("돈이 부족합니다!");
            return false;
        }
    }
    public Animator anim;
    void PlayerAttack()
    {
        bool AttackBool = attackTimer.TimeFlow(Time.deltaTime);
        if (AttackBool)
        {
            //MonsterBehavior targetMonster=FindMonsters(nowTower.GetRange());
            MonsterBehavior targetMonster = FindMonsters(2f * range);
            if (targetMonster != null)
            {
                targetMonster.SetDamage(damage);
                //Debug.Log("데미지 1 달게 한다.");
                anim.SetTrigger("IsAttack");
                PlayerRotation(targetMonster.transform.position);
            }
            else
            {
                attackTimer.WaitTime();
            }
        }
    }
    void PlayerRotation(Vector3 targetP)
    {
        Vector3 dir = transform.position - targetP;
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 15f);
        }
    }

    void PlayerHeal()
    {
        bool healBool = healTimer.TimeFlow(Time.deltaTime);
        if (healBool)
        {
            if (health == maxHP)
            {
                attackTimer.WaitTime();
            }
            else
            {
                health += healValue;
                CreateHealText(healValue);
                if (health > maxHP)
                {
                    health = maxHP;
                }
                SetHpText();
            }
        }
    }

    MonsterBehavior FindMonsters(float attackRange)
    {
        MonsterBehavior[] monsters = MM.GetComponentsInChildren<MonsterBehavior>();

        foreach (MonsterBehavior MB in monsters)
        { //안 죽었을 때, 사거리에 들어왔다면
            if (!MB.GetIsDie() && Vector3.Distance(MB.transform.position, transform.position) < attackRange)
            {
                return MB;
            }
        }
        return null;
    }


    void SetMoneyText()
    {
        moneyText.text = ": " + money + " Gold";
    }

    void SetLuckText()
    {
        luckText.text = ": " + luck + "/100 Luck";
    }
    void SetDamageText()
    {
        damageText.text = "" + damage;
    }
    void SetHpText()
    {
        hpText.text = health + " / " + maxHP;
    }
    void SetDefenceText()
    {
        defenceText.text = "" + defence;
    }
    void SetRangeText() {
        rangeText.text = "" + range;
    }
    void SetAttackSpeedText()
    {
        attackSpeedText.text = "" + attackSpeed;
    }
    void SetHealText()
    {
        healText.text = "" + healValue;
    }



    // 단순하게 팝업 7개 만들기
    public TextMeshProUGUI popUpUIText;
    public GameObject PopUpPannel;
    public GameObject PopUpLuck;
    public GameObject PopUpDamage;
    public GameObject PopUpHp;
    public GameObject PopUpDefence;
    public GameObject PopUpRange;
    public GameObject PopUpAttackSpeed;
    public GameObject PopUpHeal;


    public void PopUpTextUI(int pMoney)
    {
        popUpUIText.text = pMoney + "원을 사용해 Upgrade 하시겠습니까?\n(현재 money : " + money + ")";
    }

    public void PopUpUIAll(int index)
    {
        switch (index)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;
            case 7:
                break;
        }
    }
    public void PopUpLuckUI()
    {
        PopUpTextUI((luck / 10)*3 + 4);
        PopUpPannel.SetActive(true);
        PopUpLuck.SetActive(true);
    }
    public void PopUpDamageUI()
    {
        PopUpTextUI((int)(damage / 100) + 1);
        PopUpPannel.SetActive(true);
        PopUpDamage.SetActive(true);
    }
    public void PopUpHpUI()
    {
        PopUpTextUI((int)(maxHP / 100) + 1);
        PopUpPannel.SetActive(true);
        PopUpHp.SetActive(true);
    }
    public void PopUpDefenceUI()
    {
        PopUpTextUI((int)defence + 1);
        PopUpPannel.SetActive(true);
        PopUpDefence.SetActive(true);
    }
    public void PopUpRangeUI()
    {
        PopUpTextUI((int)range + 2);
        PopUpPannel.SetActive(true);
        PopUpRange.SetActive(true);
    }
    public void PopUpAttackSpeedUI()
    {
        PopUpTextUI((int)(attackSpeed / 100) + 1);
        PopUpPannel.SetActive(true);
        PopUpAttackSpeed.SetActive(true);
    }
    public void PopUpHealUI()
    {
        PopUpTextUI(3);
        PopUpPannel.SetActive(true);
        PopUpHeal.SetActive(true);
    }

    public void CloseMoneyUI()
    {
        PopUpLuck.SetActive(false);
        PopUpDamage.SetActive(false);
        PopUpHp.SetActive(false);
        PopUpDefence.SetActive(false);
        PopUpRange.SetActive(false);
        PopUpAttackSpeed.SetActive(false);
        PopUpHeal.SetActive(false);
        PopUpPannel.SetActive(false);
    }

    public GameObject MoneyPannel;
    public void MoneyPopUp()
    {
        MoneyPannel.SetActive(true);

    }
    public void CloseMoneyPopUp()
    {
        MoneyPannel.SetActive(false);
    }

    public GameObject GameOverPannel;

    public void GameOverPopUp()
    {
        // 현재 라운드 기록하기
        GameRankSave();

        GameOverPannel.SetActive(true);
        StartCoroutine(GameOverCoroutine(GameOverPannel));
    }

    public void GameRankSave() 
    {
        PlayerInfoManager.Instance.RankSave(MM.GetComponent<MonsterManager>().GetNowRound()-1);
    }


    static IEnumerator GameOverCoroutine(GameObject GameOverPannel) 
    {
        Image gImage = GameOverPannel.GetComponent<Image>();
        Color color = gImage.color;
        while (color.a < 1f) 
        {
            color.a = color.a+0.5f *Time.deltaTime;
            gImage.color = color;
            yield return color.a;
        }
    }
    public void GameOverPopUpClose() 
    {
        GameOverPannel.SetActive(false);
    }

    GameObject tempHealObject;
    public GameObject HealText;
    public GameObject DamageCanvas;
    public void CreateHealText(float tDamage)
    {
        tempHealObject = Instantiate(HealText, new Vector3(0, 2, 0), Quaternion.identity);
        tempHealObject.transform.position = this.transform.position+2*Vector3.right;
        tempHealObject.transform.SetParent(DamageCanvas.transform, false);
        tempHealObject.GetComponent<TextMeshProUGUI>().text = "+" + tDamage.ToString("0.00");
    }

    public void PopUpUI()
    {
        //PopUp
    }

    public void SetButtonAccept() 
    {
        
    }



    // 확인을 눌러야 함수 실행, 확인에 함수를 달아두어야 함.

}
