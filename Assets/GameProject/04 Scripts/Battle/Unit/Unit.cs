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
    public string attackTag;    //������
    public string attackBaseTag;//���ǹ�
    public LayerMask attacklayer;
    public LayerMask groundlayer;
    public bool isAttack = false; //�������ΰ� private����
    public bool isMoving = false;
    public bool isarrive = true;
    public float attackDelay = 1;
    float timer = 0f;

    private bool goingToClickedPos;
    private float defaultStoppingDistance;
 
    [Header("target info")]
    public ObjectBody currentTarget; //Ÿ�� ĳ����
    public Vector3 nextPos;   //�̵� ����
    public Vector3 randomPos; //
    public Vector3 castleAttackPosition; //��ǥ ���̽�����

    //�߻�ü������
    public GameObject projectilePrefab;
    //�߻�ü��������
    public GameObject projectileStart;
    //ĳ���� �̵�����
    private Quaternion moveRotation;
    

    /// <summary>
    /// ������� ������������
    /// </summary>
    private NavMeshAgent agent;
    private GameObject[] enemies;
    private GameObject healthbar;

    private UnitAnimation unitAnimation;
    private AudioSource audio;


    private GameObject unitMarker;

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
        
        //���� ������ �ƴ϶� ĳ���� �Ӹ����� ��ġ�ص� �þ߰��� ���� �޶���
        //�ϴ��� ����
        //health = transform.Find("Health").gameObject;
        //healthbar = health.transform.Find("Healthbar").gameObject;
        //health.SetActive(false);
        //healthbar.GetComponent<Slider>().maxValue = currentHP;

        moveRotation = transform.rotation;
        //StartCoroutine(Wait());
        nextPos = getRandomPosition();

        agent.stoppingDistance = attackRange - 2;
        agent.speed = moveSpeed;


       
    }
    public void SetUP()
    {

        attackDamage = attackDamage + (DataManager.instance.player.up_CavalryUnit);
    }

    public override void Updated()
    {
    }

    IEnumerator Attack()
    {
        yield return new WaitForSeconds(attackDelay);
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
                        Debug.Log("Attack call");
                        Attack(currentTarget);
                    }
                }
            }
        }
        else
        {
           
            Debug.Log("Onarrive() fail");
            Move();


        }
        



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
            Debug.Log("CheckInAttackrange curtargel null");
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
        //if (OnArrive() || isAttack) return;

        moveRotation = CalculateRotation();

        if (currentTarget != null)
        {
            agent.SetDestination(currentTarget.transform.position);
            ///move character towards target tile position
            //transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
            /////rotate character
            //transform.rotation = Quaternion.Lerp(this.transform.rotation, moveRotation, 0.1f);

        }
        else if (currentTarget == null)
        {
            agent.SetDestination(nextPos);
            
            //transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
            //transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
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
                //Debug.Log("Vector3.Distance(transform.position, nextPos)<attackRange :" + Vector3.Distance(transform.position, nextPos));
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
        //if (!isAttack)
        //    return;
        //if (Vector3.Distance(transform.position, _targetCharacter.transform.position) > attackRange)
        //{
        //    isarrive = false;
        //    return;
        //}
        if (!isAttack) return;
        //Ÿ�� ����
        currentTarget = _targetCharacter;
        //���� �ִϸ��̼� 
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
        else//�߻�ü �����
        {
            Debug.Log("ȭ�����");
            //��ϵ� �߻�ü ����
            GameObject projectile = Instantiate(projectilePrefab);
            projectile.transform.parent = gameObject.transform;
            
            //������������ �߻�ü ��ġ
            projectile.transform.position = projectileStart.transform.position;
            //�߻�ü�� Ÿ�ٵ��
            projectile.GetComponent<Projectile>().Init(currentTarget);

            currentTarget.TakeDamage(attackDamage);
        }
    }
    public void Death()
    {
        unitAnimation.SetDeath();

        Destroy(gameObject, 2);
    }

    //������ ��Ÿ�� ����
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

        //Vector3 raypoint = new Vector3(Random.Range(-searchRange, searchRange) + transform.position.x, transform.position.y, (Random.Range(-searchRange / 2, searchRange / 2) + transform.position.z));
        ////Debug.Log(raypoint);
        //RaycastHit hit;


        //if (Physics.Raycast(raypoint, Vector3.up, out hit, 2f, groundlayer))
        //{

        //    isarrive = false;
        //    return hit.point;
        //}

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
            //������
            Vector3 direction = (nextPos - transform.position).normalized;
            //�������
            var angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            return Quaternion.AngleAxis(angle, Vector3.up);
        }

        return Quaternion.identity;

    }


    //public void TakeDamage(Unit _attackerCharacter)
    //{
    //    //Debug.Log("TakeDamage" + _attackerCharacter.attackDamage);
    //    health -= _attackerCharacter.attackDamage;

    //    //this.GetComponent<CharacterAnimationEx>().SetDamage(true);
    //    if (health <= 0)
    //    {
    //        Death();
    //    }

    //}
    //public void OnAttack()
    //{
    //    if (currentTarget == null)
    //    {
    //        unitAnimation.SetAttack(false);
    //        return;
    //    }
    
    //    if (projectilePrefab == null)
    //    {
    //        currentTarget.TakeDamage(attackDamage);
    //    }
    //    else//�߻�ü �����
    //    {
    //        Debug.Log("ȭ�����");
    //        //��ϵ� �߻�ü ����
    //        GameObject projectile = Instantiate(projectilePrefab);
    //        //������������ �߻�ü ��ġ
    //        projectile.transform.position = projectileStart.transform.position;
    //        //�߻�ü�� Ÿ�ٵ��
    //        projectile.GetComponent<Projectile>().Init(currentTarget);

    //        currentTarget.TakeDamage(attackDamage);
    //    }
    //}

    //public void OnAttackFinish()
    //{
        
    //    if (currentTarget == null)
    //    {
    //        unitAnimation.SetAttack(false);
    //        return;
    //    }
    
        
    //}

    //���ּ���
    
    public void SelectUnit()
    {
        selectedMarker.SetActive(true);
        Debug.Log("SelectUnit");
    }
    public void DeselectUnit()
    {
        selectedMarker.SetActive(false);
        Debug.Log("deselectUnit");
    }
    public void SelectUnitMove(Vector3 pos)
    {
        if (selected)
        {
            currentTarget = null;
            nextPos = pos;
            agent.SetDestination(pos);
        }

    }

}
