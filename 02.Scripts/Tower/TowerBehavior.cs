using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TowerBehavior : MonoBehaviour
{
    // Start is called before the first frame update

    Tower nowTower;
    Timer attackTimer = new Timer();
    GameObject MM; //MonsterManager
    public TowerGenerator TG;

    float nowRange = 2f;

    bool isDragging = false;

    // 이펙트
    int effectIdx = 0; // 이펙트 관리용
    public GameObject SelectEffect;

    public GameObject WizardEffect;
    public GameObject RapidEffect;
    public GameObject SplashEffect;

    // 사운드
    public AudioSource gunAudio;
    public AudioSource splashAudio;
    public AudioSource magicAudio;
    public AudioSource attackAudio;


    private void Awake()
    {
        MM = GameObject.Find("MonsterManager");
    }
    void Start()
    {
        AttackTimeInitialize();
    }

    // Update is called once per frame

    void Update()
    {
        if (!isDragging)
        {
            TowerAttack();
        }
    }

    public void SetDragging (bool TF)
    {
        isDragging = TF;    
    }

    // 이 부분을 수정해 주어야함
    void TowerAttack() 
    {
        bool AttackBool = attackTimer.TimeFlow(Time.deltaTime);
        if (AttackBool) 
        {
            MonsterBehavior targetMonster = FindMonsters(nowRange);
            if (targetMonster != null)
            {
                anim.SetTrigger("IsAttack");
                EffectGen(targetMonster);
                //Debug.Log("데미지 1 달게 한다.");
            }
            else 
            {
                attackTimer.WaitTime();
            }
        }
    }

    Vector3 monsterP;
    void EffectGen(MonsterBehavior targetMonster) 
    {

        monsterP=targetMonster.transform.position;
        TowerRotation(monsterP);

        if (effectIdx == 1) // 이펙트 추가
        {
            WizardEffect.SetActive(true);
            WizardEffect.GetComponent<WizardEffect>().EffectSetting(monsterP, transform.position);
            targetMonster.SetDamage(nowTower.GetDamage());
            magicAudio.Play();
        }
        else if (effectIdx == 2)
        {
            RapidEffect.SetActive(true);
            RapidEffect.GetComponent<RapidEffect>().EffectSetting(monsterP);
            targetMonster.SetDamage(nowTower.GetDamage());
            gunAudio.Play();
        }
        else if (effectIdx == 3)
        {
            // effect에서 Damage주도록 변경
            SplashEffect.SetActive(true);
            MonsterBehavior[] monsters = MM.GetComponentsInChildren<MonsterBehavior>();
            SplashEffect.GetComponent<SplashEffect>().EffectSetting(monsterP, nowTower.GetDamage(), monsters);
            splashAudio.Play();
        }
        else 
        {
            targetMonster.SetDamage(nowTower.GetDamage());
            attackAudio.Play();

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

    public void SetNowTower(Tower stower) 
    {
        nowTower = stower;
        SetTowerGraphic(stower);
        SetTowerRange(stower);
    }

    /// <summary>///
    /// tower종류에 따라서 어떤 Design과 animation을 설정할지 결정
    public GameObject RapidTower_B;
    public GameObject RapidTower_A;
    public GameObject RapidTower_S;
    public GameObject SwordTower_B;
    public GameObject SwordTower_A;
    public GameObject SwordTower_S;
    public GameObject FootManTower_B;
    public GameObject FootManTower_A;
    public GameObject FootManTower_S;
    public GameObject WizardTower_B;
    public GameObject WizardTower_A;
    public GameObject WizardTower_S;
    public GameObject SplashTower_B;
    public GameObject SplashTower_A;
    public GameObject SplashTower_S;
    bool generatorFlag = false; // animation이 null인 상태 방지용
    Animator anim;
    /// </summary>///

    public void SetTowerGraphic(Tower stower) {
        int tIndex = (int)stower.GetIndex();
        GameObject nowDesign;
        switch (tIndex) 
        {
            case 1:
                nowDesign = FootManTower_B;
                break;
            case 2:
                nowDesign = FootManTower_A;
                break;
            case 3:
                nowDesign = FootManTower_S;
                break;
            case 4:
                nowDesign = SplashTower_B;
                effectIdx = 3;
                break;
            case 5:
                nowDesign = SplashTower_A;
                effectIdx = 3;
                break;
            case 6:
                nowDesign = SplashTower_S;
                effectIdx = 3;
                break;
            case 7:
                nowDesign = WizardTower_B;
                effectIdx = 1;
                break;
            case 8:
                nowDesign = WizardTower_A;
                effectIdx = 1;
                break;
            case 9:
                nowDesign = WizardTower_S;
                effectIdx = 1;
                break;
            case 10:
                nowDesign = SwordTower_B;
                break;
            case 11:
                nowDesign = SwordTower_A;
                break;
            case 12:
                nowDesign = SwordTower_S;
                break;
            case 13 :
                nowDesign = RapidTower_B;
                effectIdx = 2;
                break;
            case 14 :
                nowDesign = RapidTower_A;
                effectIdx = 2;
                break;
            case 15 :
                nowDesign= RapidTower_S;
                effectIdx = 2;
                break;
            default:
                nowDesign = RapidTower_S;
                break;
        }
        nowDesign.SetActive(true);
        anim = nowDesign.GetComponent<Animator>();
    }


    


    public Tower GetNowTowerInfo() 
    {
        return nowTower;
    }

    void TowerMove() 
    {
    
    }

    void AttackTimeInitialize() 
    {
        attackTimer.TimeDelaySet(5f/nowTower.GetAttackSpeed());
    }


    float RangeFactor = 4f;
    void SetTowerRange(Tower sTower)
    {

        float tRange=sTower.GetRange()*RangeFactor; 
        nowRange = sTower.GetRange()* RangeFactor/2f;
        //gizmos = nowRange;
    }
    void TowerRotation(Vector3 targetP) 
    {
        Vector3 dir = targetP - transform.position;
        //transform.Translate(dir.normalized * Time.deltaTime, Space.World);
        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), 15f);
        }
    }

    public void OnSelectEffect() {
        SelectEffect.SetActive(true);
    }
    public void OffSelectEffect() { 
        SelectEffect.SetActive(false);
    }

    //float gizmos=1f;

    //void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, (float)gizmos);
    //    Debug.Log("기즈모 그리는 중"+gizmos);
    //}
}
