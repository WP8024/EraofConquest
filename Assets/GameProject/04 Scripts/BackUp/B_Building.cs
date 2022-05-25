//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//[RequireComponent(typeof(NavMeshObstacle))]
//public class B_Building : ObjectBody
//{

//    #region 생산
//    public GameObject[] units;      //유닛프리팹모음
//    public Transform spawnPoint;    //유닛생성지점
//    public int turngold = 100;      //주기적으로 획득하는 골드량 
//    #endregion

//    #region 공격
//    public Transform muzzle;        //발사체 생성위치
//    public Projectile projectilePrefab; //발사체 프리팹
//    public float msBetweenShots = 100;//발사체의 연사속도
//    public float muzzleVelocity = 35; //발사체의 발사될때 속도 


//    LayerMask layermask;
//    float range;              //탐지범위 & 공격범위
//    ObjectBody targetObj;           //타겟오브젝트
//    bool hasTarget;                 //타겟유무확인
//    float nextShotTime;             //공격딜레이


//    public Transform target;        //타겟 위치

//    #endregion

//    public bool isSpawner = false;
//    public bool isAttackable = false;

//    private DataManager datamanager;

//    public override void Awake()
//    {
//        base.Awake();
//    }

//    public override void Start()
//    {
//        base.Start();
//        if (units.Length != 0)
//        {
//            isSpawner = true;
//        }
//        if (projectilePrefab != null)
//        {
//            isAttackable = true;
//        }
//        datamanager = DataManager.Instance;
//        // StartCoroutine(Search());
//    }

//    public override void Updated()
//    {

//    }

//    public void Update()
//    {

//        if (target == null || !inRange(target))
//        {
//            target = null;
//            Search();
//        }
//        if (inRange(target))
//        {
//            StartCoroutine(Shoot());

//        }
//    }
//    public override bool HandleMessage(Telegram telegram)
//    {
//        return false;
//    }

//    public void Seek(Transform _target)
//    {
//        target = _target;
//    }


//    IEnumerator Shoot()
//    {
//        if (isAttackable)
//        {

//            if (Time.time > nextShotTime)
//            {
//                nextShotTime = Time.time + msBetweenShots / 1000;
//                Projectile newProjectile = Instantiate(projectilePrefab, muzzle.position, muzzle.rotation) as Projectile; //타입이 안맞는다는 메세지가 뜨는경우 as Projectile 추가
//                newProjectile.SetSpeed(muzzleVelocity);
//            }

//        }

//        yield return nextShotTime;
//    }


//    public void Search()
//    {
//        RaycastHit hit;

//        ////OverlapSphere사용예 범위에 접촉된 모든 콜라이더 객체들 반환
//        //Collider[] colls = Physics.OverlapSphere(transform.position, range, layermask);
//        //for (int i = 0; i < colls.Length; ++i)
//        //{
//        //    colls[i].SendMessage("attackPlayer", SendMessageOptions.DontRequireReceiver);
//        //}


//        if (Physics.SphereCast(transform.position, range, Vector3.zero, out hit, layermask))
//        {
//            if (!CompareTag(hit.transform.tag))
//            {
//                target = hit.transform;
//            }
//        }
//    }
//    public virtual bool inRange(Transform _target)
//    {
//        if (_target == null)
//        {
//            return false;
//        }
//        else
//        {
//            if (range > Vector3.Distance(transform.position, _target.transform.position))
//            {
//                return true;
//            }
//            else
//            {

//                return false;
//            }
//        }
//    }

//    //public Vector3 nextAttackPosition()
//    //{
//    //    // use the parametric equation of a circle to determine the next attack position.
//    //    var position = transform.position;
//    //    position.x += radius * Mathf.Sin((attackSpread * unitcount + spreadOffset) * Mathf.Deg2Rad);
//    //    position.z += radius * Mathf.Cos((attackSpread * unitcount + spreadOffset) * Mathf.Deg2Rad);
//    //    unitcount++;
//    //    return position;
//    //}

//    public bool createUnit()
//    {
//        if (!isSpawner) return false;
//        else
//        {


//            int n = Random.RandomRange(0, units.Length);

//            if (transform.tag == "Blue" && datamanager.player.money >= 200)
//            {
//                datamanager.player.money -= units[n].GetComponent<Unit>().price;
//                var spawnPosition = spawnPoint.position;
//                //var spawnedUnit = GameObject.Instantiate(units[n], spawnPosition, spawnPoint.rotation) as GameObject;

//                var spawnedUnit = GameObject.Instantiate(units[n], spawnPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
//            }
//            if (transform.tag == "Red" && datamanager.enemy.money >= 200)
//            {
//                datamanager.enemy.money -= units[n].GetComponent<Unit>().price;
//                var spawnPosition = spawnPoint.position;
//                //var spawnedUnit = GameObject.Instantiate(units[n], spawnPosition, spawnPoint.rotation) as GameObject;

//                var spawnedUnit = GameObject.Instantiate(units[n], spawnPosition, Quaternion.Euler(0, 0, 0)) as GameObject;
//            }


//            //spawnedUnit.transform.parent = unitParent;

//            // store the spawned unit in a list so we can reference it later
//            //units.Add(spawnedUnit);
//            return true;

//        }


//    }

//}