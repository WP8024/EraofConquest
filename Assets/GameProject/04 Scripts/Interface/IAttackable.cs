using UnityEngine;

public interface IAttackable {


    void TakeAttack(float damage, RaycastHit hit);

    void SetAttack(float damage);
}

public class AttackEx
{ 
    [Header("Attack Status")]
    public float attackDamage;
    public float attackRange;
    public AudioClip attackAudio;
    public float moveSpeed;
    public float searchRange;
    [HideInInspector]
    public Transform currentTarget;
    [HideInInspector]
    public Vector3 castleAttackPosition;

    [Header("Wirad")]
    public bool wizard;
    public ParticleSystem spawnEffect;
    public GameObject skeleton;
    public int skeletonAmount;
    public float newSkeletonsWaitTime;


    [Header("target info")]
    public string attackTag;    //적유닛
    public string attackBaseTag;//적건물
    public LayerMask attacklayer;
    public LayerMask groundlayer;
    public bool isAttack = false; //공격중인가 private예정

    public bool isMoving = false;
    public bool isarrive = true;
    public float waittime = 3;

    private bool goingToClickedPos;
    private float defaultStoppingDistance;
    private Vector3 targetPosition;
    private Vector3 randomTarget;
}