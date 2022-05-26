using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BehaviorDesigner.Runtime;

//public enum UnitState { Idle, Walk, Run, Attack, TakeDamage, Death, Charge, Cast }

[RequireComponent (typeof (NavMeshAgent))]
public class Unit : ObjectBody
{
    private Behavior behavior;

    [Header("Attack Status")]
    public float attackDamage;
    public float attackRange;
    public float moveSpeed;
    public float searchRange;
    public AudioClip attackAudio;

    [Header("target info")]
    public string attackTag;    //적유닛
    public string attackBaseTag;//적건물
    public LayerMask attacklayer;
    public LayerMask groundlayer;
    public bool isAttack = false; //공격중인가 private예정
    public bool isMoving = false;
    public bool isarrive = true;
    public float attackDelay = 1;
    float timer = 0f;

    public bool goingToClickedPos;
    public float defaultStoppingDistance;
 
    [Header("target info")]
    public ObjectBody currentTarget; //타겟 캐릭터
    public Vector3 nextPos;   //이동 지점
    public Vector3 randomPos; //
    public Vector3 castleAttackPosition; //목표 베이스지점

    //발사체프리팹
    public GameObject projectilePrefab;
    //발사체시작지점
    public GameObject projectileStart;
    //캐릭터 이동방향
    private Quaternion moveRotation;
    

    /// <summary>
    /// 사용할지 안할지아직모름
    /// </summary>
    private NavMeshAgent agent;
    private GameObject[] enemies;
    private GameObject healthbar;

    private UnitAnimation unitAnimation;
    private AudioSource audio;


    public GameObject unitMarker;

    public override void Setup(ObjectBody obj, int id)
    {
        base.Setup(obj, id);
    }

    public override void Awake()
    {
        base.Awake();
        unitAnimation = GetComponent<UnitAnimation>();
        agent = GetComponent<NavMeshAgent>();
        audio = GetComponent<AudioSource>();
    }

    public override void Start()
    {
        base.Start();
        
        //시점 고정이 아니라 캐릭터 머리위에 배치해도 시야각에 따라 달라짐
        //일단은 보류
        //health = transform.Find("Health").gameObject;
        //healthbar = health.transform.Find("Healthbar").gameObject;
        //health.SetActive(false);
        //healthbar.GetComponent<Slider>().maxValue = currentHP;



        moveRotation = transform.rotation;
        //StartCoroutine(Wait());
        nextPos = getRandomPosition();

        agent.stoppingDistance = attackRange - 2;
        agent.speed = moveSpeed;

        if (CompareTag("Red"))
        {
            DataManager.Instance.enemy.curUnit++;
        }
        else
        {
            DataManager.Instance.player.curUnit++;
            UnitController.Instance.UnitList.Add(this);
        }


    }
    public void SetUP(Faction _faction,UnitData data)
    {
        faction = _faction;                 //팩션
        health = data.maxhp;                //체력
        attackDamage = data.damage;         //데미지
        attackRange = data.attackdistance;  //공격범위
        //attackDelay =                     //공격속도
        moveSpeed = data.movespeed;         //이동속도
        //attackAudio                       //공격소리
                                            //죽을때소리
                                            

        attackDamage = attackDamage + (DataManager.Instance.player.up_CavalryUnit);
    }



    public void Update()
    {
        if (isAttack)
        {
            timer += Time.deltaTime;
            if (timer >= attackDelay)
            {
                timer = 0f;
                isAttack = false;
            }
        }

        if (OnArrive())
        {
            if (currentTarget==null)
            {
                if (!findCurrentTarget())
                {
                    nextPos = getRandomPosition();
                    if (!agent.hasPath)
                    {
                        isMoving = true;
                        unitAnimation.SetRun(true);
                        agent.SetDestination(nextPos);
                    }
                }
            }
            else if (currentTarget != null)
            {
                if (CheckInAttackrange(currentTarget))
                {
                    if (!isAttack)
                    {
                        isAttack = true;
                        transform.LookAt(currentTarget.transform);
                        Attack(currentTarget);
                    }
                }
            }
        }
        else
        {
            Move();
        }
        #region 주석
        //if (nextPos == transform.position)
        //{
        //    nextPos = getRandomPosition();
        //}
        //if (!isarrive)
        //{
        //    Move();
        //}
        //else if (isarrive)
        //{
        //    if (currentTarget != null)
        //    {
        //        Attack(currentTarget);
        //    }
        //    else
        //    {
        //        if (findCurrentTarget())
        //        {
        //            Attack(currentTarget);
        //        }
        //        if (currentTarget == null)
        //        {
        //            nextPos = getRandomPosition();
        //        }
        //    }
        //}
        //if (isMoving) { }
        //if (currentTarget == null)
        //{
        //    isAttack = false;
        //    unitAnimation.SetAttack(isAttack);
        //}
        //if (isAttack)
        //{
        //    ///calculate direction vector
        //    Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

        //    ///calculate angle
        //    var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        //    ///rotate character
        //    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), 0.1f);
        //}
        //if (isDead) { }
        #endregion
    }

    public virtual bool inRange(ObjectBody _target)
    {
        if (_target == null)
        {
            return false;
        }
        else
        {
            if (searchRange > Vector3.Distance(transform.position, _target.transform.position))
            {
                return true;
            }
            else
            {

                return false;
            }
        }
    }

    public bool CheckInAttackrange(ObjectBody _target)
    {
        if(_target== null)
        {
            return false;
        }
        else
        {
            if (attackRange >= Vector3.Distance(transform.position, _target.transform.position))
                return true;
            
        }
        return false;

    }
    public void Move()
    {
        moveRotation = CalculateRotation();

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.transform.position);

            #region 주석
            //도착여부 판단 if문 작성중
            //if (agent.velocity.sqrMagnitude >= 0.2f * 0.2f && agent.remainingDistance <= 0.5f)
            //{
            //}

            ///move character towards target tile position
            //transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
            /////rotate character
            //transform.rotation = Quaternion.Lerp(this.transform.rotation, moveRotation, 0.1f);
            #endregion
        }
        else
        {
            agent.SetDestination(nextPos);
            #region moveToward사용시
            //transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
            #endregion
        }
        if (!isMoving)
        {
            isMoving = true;
            unitAnimation.SetRun(true);
        }
    }



    public bool OnArrive()
    {
        if (currentTarget == null)
        {
            if(Vector3.Distance(transform.position, nextPos)<attackRange)
            {
                isMoving = false;
                isarrive = true;
                unitAnimation.SetRun(false);
                return true;
            }

        }
        else if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) < attackRange)
            {
                isMoving = false;
                isarrive = true;
                unitAnimation.SetRun(false);
                return true;
            }
        }
        isarrive = false;
        return false;

    }

    public void Attack(ObjectBody _targetCharacter)
    {
        #region 주석
        //if (!isAttack)
        //    return;
        //if (Vector3.Distance(transform.position, _targetCharacter.transform.position) > attackRange)
        //{
        //    isarrive = false;
        //    return;
        //}
        #endregion 
        if (!isAttack) return;
        //타겟 저장
        currentTarget = _targetCharacter;
        //공격 애니메이션 
        unitAnimation.SetAttack();
        if (attackAudio != null)
        {
            audio.Stop();
            audio.PlayOneShot(attackAudio);
        }
        if (projectilePrefab == null)
        {
            currentTarget.TakeDamage(attackDamage);
        }
        else//발사체 존재시
        {
            Debug.Log("화살생성");
            //등록된 발사체 생성
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = gameObject.transform;
            
            //시작지점에서 발사체 위치
            projectile.transform.position = projectileStart.transform.position;
            //발사체에 타겟등록
            projectile.GetComponent<Projectile>().Init(currentTarget);

            currentTarget.TakeDamage(attackDamage);
        }
    }
    public void Death()
    {
        unitAnimation.SetDeath();

        Destroy(gameObject, 2);
    }

    //범위내 적타겟 설정
    public bool findCurrentTarget()
    {
        if (currentTarget != null) { return true; }
          
        
       // currentTarget = GameObject.FindGameObjectWithTag(attackTag).GetComponent<ObjectBody>();

        if (inRange(currentTarget))
        {
            return true;
        }


        Collider[] hitCol = Physics.OverlapSphere(transform.position, searchRange, attacklayer);
        foreach (Collider enemy in hitCol)
        {
            //if (enemy.tag == attackBaseTag || enemy.tag == attackTag)
            if (enemy.tag == attackTag)
            {
                if (!currentTarget || (currentTarget && (Vector3.Distance(transform.position, currentTarget.transform.position) > Vector3.Distance(transform.position, enemy.transform.position))))
                {
                    currentTarget = enemy.GetComponent<ObjectBody>();
                    nextPos = currentTarget.transform.position;
                    isarrive = false;
                    return true;
                }
            }
        }

        return false;
    }

    public Vector3 getRandomPosition()
    {

        if (isMoving)
        {
            isarrive = false;
            // Debug.Log(nextpos);
            return nextPos;
        }
        randomPos = transform.position + Random.insideUnitSphere * searchRange;
        NavMeshHit hit;
        if(NavMesh.SamplePosition(randomPos,out hit, 1.0f, NavMesh.AllAreas))
        {
            return hit.position;
        }
        #region
        //Vector3 raypoint = new Vector3(Random.Range(-searchRange, searchRange) + transform.position.x, transform.position.y, (Random.Range(-searchRange / 2, searchRange / 2) + transform.position.z));
        ////Debug.Log(raypoint);
        //RaycastHit hit;


        //if (Physics.Raycast(raypoint, Vector3.up, out hit, 2f, groundlayer))
        //{

        //    isarrive = false;
        //    return hit.point;
        //}
        #endregion
        return transform.position;
    }



    private Quaternion CalculateRotation()
    {
        if (currentTarget != null)
        {

            ///calculate direction vector
            Vector3 direction = (currentTarget.transform.position - transform.position).normalized;

            ///calculate angle
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            ///rotate character
            return Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.up), 0.1f);
        }

        if (nextPos != transform.position)
        {
            //방향계산
            Vector3 direction = (nextPos - transform.position).normalized;
            //각도계산
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.up);
        }

        return Quaternion.identity;

    }

    void OnDestroy()
    {
        if (CompareTag("Red"))
        {
            DataManager.Instance.enemy.curUnit--;
        }
        else
        {
            DataManager.Instance.player.curUnit--;
            UnitController.Instance.UnitList.Remove(this);
        }
    }
    public void SelectUnit()
    {
        selected = true;
        selectedMarker.SetActive(true);
    }
    public void DeselectUnit()
    {
        selectedMarker.SetActive(false);
    }
    public void SelectUnitMove(Vector3 pos)
    {
        Debug.Log("selectUnitMove");
        if (selected)
        {
            currentTarget = null;
            nextPos = pos;
            agent.SetDestination(pos);
        }

    }

}
