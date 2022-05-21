using UnityEngine;
using UnityEngine.AI;

//public enum UnitState { Idle, Walk, Run, Attack, TakeDamage, Death, Charge, Cast }

[RequireComponent (typeof (NavMeshAgent))]
public class Unit : ObjectBody
{
  

   
    public int UnitID;
    public RTSDB rtsDB;

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
    public float waittime = 3;

    private bool goingToClickedPos;
    private float defaultStoppingDistance;
 
    [Header("target info")]
    public Transform currentTarget; //Ÿ�� ĳ����
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
    //private NavMeshAgent agent;
    private GameObject[] enemies;
    private GameObject healthbar;

    private UnitAnimation unitAnimation;

    public override void Awake()
    {
        base.Awake();
        unitAnimation = GetComponent<UnitAnimation>();


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
    }


    public override void Updated()
    {
    }

    public void Update()
    {
        if (isarrive)
        {
            if (currentTarget==null)
            {
                if (isAttack)
                {
                    isAttack = false;
                }
                if (!findCurrentTarget())
                {
                    getRandomPosition();
                }
            }
            else if (currentTarget != null)
            {
                Attack(transform);
            }
        }
        else
        {
            if (nextPos == transform.position)
            {
                getRandomPosition();
            }
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

    public virtual bool inRange(Transform _target)
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
    public void Move()
    {
        if (OnArrived() || isAttack) return;

        moveRotation = CalculateRotation();

        if (currentTarget != null)
        {
            ///move character towards target tile position
            transform.position = Vector3.MoveTowards(transform.position, currentTarget.transform.position, moveSpeed * Time.deltaTime);
            ///rotate character
            transform.rotation = Quaternion.Lerp(this.transform.rotation, moveRotation, 0.1f);

        }
        else if (currentTarget == null)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, moveRotation, 0.1f);
        }

        if (!isMoving)
        {
            isMoving = true;
            unitAnimation.SetRun(true);
        }
    }
    public void SelectUnitMove(Vector3 pos)
    {
        if (selected)
        {
            currentTarget = null;
            nextPos = pos;
        }
    }
  
    public void Attack(Transform _targetCharacter)
    {
        //if (isAttack)
        //    return;
        if (Vector3.Distance(transform.position, _targetCharacter.transform.position) > attackRange)
        {
            isarrive = false;
            return;
        }
        //isAttack = true;

        //Ÿ�� ����
        currentTarget = _targetCharacter;
        //���� �ִϸ��̼� 
        unitAnimation.SetAttack();
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
        currentTarget = GameObject.FindGameObjectWithTag(attackTag).transform;
        
        //Collider[] hitCol = Physics.OverlapSphere(transform.position, searchRange, attacklayer);
        //foreach (Collider enemy in hitCol)
        //{
        //    //if (enemy.tag == attackBaseTag || enemy.tag == attackTag)
        //    if (enemy.tag == attackTag)
        //    {
        //        if (!currentTarget || (currentTarget && (Vector3.Distance(transform.position, currentTarget.transform.position) > Vector3.Distance(transform.position, enemy.transform.position))))
        //        {
        //            currentTarget = enemy.GetComponent<GameObject>().transform;
        //            nextPos = currentTarget.transform.position;
        //            isarrive = false;
        //            return true;
        //        }
        //    }
        //}
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
        Vector3 raypoint = new Vector3(Random.Range(-searchRange / 2, searchRange / 2) + transform.position.x, transform.position.y, (Random.Range(-searchRange / 2, searchRange / 2) + transform.position.z));
        //Debug.Log(raypoint);
        RaycastHit hit;


        if (Physics.Raycast(raypoint, Vector3.up, out hit, 2f, groundlayer))
        {

            isarrive = false;
            return hit.point;
        }

        return transform.position;
    }

    /// ��ǥ������ �����ϸ�
    private bool OnArrived()
    {
        if (currentTarget != null)
        {
            if (Vector3.Distance(transform.position, currentTarget.transform.position) > attackRange) return false;
        }
        else
        {
            if (Vector3.Distance(transform.position, nextPos) > attackRange) return false;
        }


        isarrive = true;
        isMoving = false;
        nextPos = transform.position;
        unitAnimation.SetRun(false);
        // Debug.Log("OnArrived");


        return true;
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


    public void TakeDamage(Unit _attackerCharacter)
    {
        //Debug.Log("TakeDamage" + _attackerCharacter.attackDamage);
        health -= _attackerCharacter.attackDamage;

        //this.GetComponent<CharacterAnimationEx>().SetDamage(true);
        if (health <= 0)
        {
            Death();
        }

    }
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
    public void OnDeathFinish()
    {

    }
    public void OnChargeFinish()
    {

    }
    public void OnCastFinish()
    {

    }

}
