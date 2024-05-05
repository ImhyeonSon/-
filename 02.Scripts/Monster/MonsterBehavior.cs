using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehavior : MonoBehaviour
{

    // monster의 공격과 이동 등 모든 행동을 담당할 스크립트
    CharacterController CC;

    Monster nowMonster;

    public Animator anim;
    PlayerManager PM;
    
    
    private int waypointIndex = 0;
    private Transform waypointList;
    private Transform target;
    private Transform[] waypoints;
    MonsterManager monsterManager;

    //private float speed = 10f;

    Timer attackTimer = new Timer();


    bool reach = false;
    bool isDie = false;
    bool AttackBool;

    // Damage 표시용 Canvas
    public GameObject DamageText;
    GameObject tempDamageObject;
    public GameObject DamageCanavas;

    private void Awake()
    {
        DamageCanavas= GameObject.Find("DamageCanvas");
        CC =GetComponent<CharacterController>();
        monsterManager = GameObject.Find("MonsterManager").GetComponent<MonsterManager>();
        waypointList=GameObject.Find("WayPointsList").transform;
        //PM= GameObject.Find("PlayerManager").GetComponent<PlayerManager>();//이름으로 찾는 방법
        PM= GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();//태그로 찾는 방법
        GetWaypoints();
        target = waypoints[waypointIndex];

        // 나중에 속도 변수를 가져올 scripts
        // Awake와 OnEnable이 아니라 다른 script에서 접근해서 StatusInitialize해준다.
    }
    void Start()
    {
        //attackTimer.TimeDelaySet(1f);
        SelectSlime((nowMonster.GetLevel() - 1) / 5);
        AttackInitializer();
        AnimationInitializer();
    }


    private void OnEnable()
    {
        SelectSlime((nowMonster.GetLevel()-1)/5);
        StatusInitialize();
        AttackInitializer();
        AnimationInitializer();
        //SetNowMonster(int level); //generator에서 미리 호출시키기
    }


    void Update()
    {
        if (!isDie)
        {
            Move();
            MonsterAttack();
        }
    }

    void GetWaypoints() 
    {
        waypoints = new Transform[waypointList.childCount];
        for (int i=0; i< waypoints.Length; i++) 
        {
            waypoints[i]= waypointList.GetChild(i);
        }
    }

    void Move() 
    {
        if (!reach) 
        {
            //Debug.Log(waypointIndex +" "+ waypoints.Length);
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized* nowMonster.GetSpeed()*Time.deltaTime, Space.World);
            if (dir != Vector3.zero) { 
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * 15f);
            }

            if (Vector3.Distance(transform.position, target.position) <= 0.1f) 
            {
                GetNextWaypoints();
            }
        }
    }

    public void SetNowMonster(int level) 
    {
        nowMonster = DataManager.Instance.GetMonsterData(level); //새로 생성해 삽입
    }


    void GetNextWaypoints() 
    {
        if (waypointIndex < waypoints.Length - 1)
        {
            waypointIndex++;
        }
        else 
        {
            reach=true;
        }
        target = waypoints[waypointIndex];
    }
    // Monster를 여기에 전달하고 관리하기.
    // stun, slow, dot, 등의 상태이상은 temp변수를 하나 더 만들어 관리하기
    // Update에서 상태 계속 관리하기

    void IsMonsterDie() 
    {
        if (nowMonster.GetHealth()<=0f) 
        {
            //Debug.Log("죽음");
            isDie= true;
            monsterManager.MonsterDead();
            // 나중에 animation 설정 후 해제해주어야함
            StartCoroutine(AnimationDie());
        }
    }
    // 추후 완료
    IEnumerator AnimationDie() 
    {
        //anim.SetTrigger("Die");
        anim.SetTrigger("isDie");

        // 플레이어 kill count up
        PM.GetKillCount(nowMonster.GetKillCount());
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    void StatusInitialize() 
    {
        gameObject.transform.position = waypoints[0].position;
        waypointIndex =0;
        target = waypoints[waypointIndex];
        isDie = false;
        reach = false;
        SetHpBar(); // 체력바 초기화
    }


    //몬스터가 공격받는 함수
    public void SetDamage(float towerDamage) 
    {
        float CalcDamage = Math.Max(0.5f, towerDamage - nowMonster.GetDefense());
        float tempH=nowMonster.GetHealth() - CalcDamage;
        //Debug.Log("실제로 다냐?"+tempH);

        nowMonster.SetHealth(tempH);
        CreateDamageText(CalcDamage);
        SetHpBar();
        IsMonsterDie();
    }

    // 도트 데미지를 받는 함수
    public void SetDotDamage(float time, float timeSequence, float towerDamage) 
    {
        // 도트딜 딜레이적용, 그 전에 일반 타워 하나 만들어서 테스트를 하면서 만드는게 좋을 듯

    }

    public bool GetIsDie() 
    {
        return isDie;
    }

    void AnimationInitializer() // 처음 생성 시 걷는 애니메이션으로 초기화 해주어야 함
    {
        anim.SetTrigger("isWalk");
    }

    void AttackInitializer() 
    {
        attackTimer.TimeDelaySet(5f/nowMonster.GetAttackSpeed());
    }

    void MonsterAttack() 
    {
        AttackBool = attackTimer.TimeFlow(Time.deltaTime);
        if (AttackBool)
        {
            if (Vector3.Distance(PM.transform.position, transform.position) <= 3f)
            {
                PM.SetDamage(nowMonster.GetDamage());
                anim.SetTrigger("isAttack");
                //Debug.Log("공격");
            }
            else
            {
                attackTimer.WaitTime();
            }
        }
    }


    public GameObject BabySlime;
    public GameObject DefaultSlime;
    public GameObject BabyLeafSlime;
    public GameObject LeafSlime;
    public GameObject KingSlime;

    public GameObject RabbitSlime;
    public GameObject HalmetSlime;
    public GameObject KingSlime_2;
    public GameObject VikingSlime;


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
        switch (caseNum) {
            case 0:
                tempObject=BabySlime;
                break;
            case 1:
                tempObject=DefaultSlime;
                break;
            case 2:
                tempObject = BabyLeafSlime;
                break;
            case 3:
                tempObject = LeafSlime;
                break;
            case 4:
                tempObject = KingSlime;
                break;
            case 5:
                tempObject = RabbitSlime;
                break;
            case 6:
                tempObject = HalmetSlime;
                break;
            case 7:
                tempObject = KingSlime_2;
                break;
            case 8:
                tempObject = VikingSlime;
                break;
            default:
                tempObject = DefaultSlime;
                break;
        }
        tempObject.SetActive(true);
        anim = tempObject.GetComponent<Animator>();
    }

    public Image HpBar;
    public HpBar HpBarObject;
    public void SetHpBar()
    {
        float hpRatio = (float) nowMonster.GetHealth() / (float)nowMonster.GetMaxHealth();
        //Debug.Log("아오"+nowMonster.GetHealth() + " : "+nowMonster.GetMaxHealth());
        if (hpRatio <= 0)
        {
            hpRatio = 0f;
        }
        HpBarObject.SetHpBar(hpRatio);
    }

    
    public void CreateDamageText(float tDamage) 
    {
        tempDamageObject = Instantiate(DamageText, new Vector3(0, 2, 0), Quaternion.identity);
        tempDamageObject.transform.position = this.transform.position;
        tempDamageObject.transform.SetParent(DamageCanavas.transform,false);
        tempDamageObject.GetComponent<TextMeshProUGUI>().text = "-"+tDamage.ToString("0.00"); 
    }



}
