using System.Collections;
using UnityEngine;

public class UnitAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Unit unit;

    void Awake()
    {
        animator = GetComponent<Animator>();
        unit = GetComponent<Unit>();
    }
    // Update is called once per frame
    void Start()
    {
        animator.Play(0, -1, Random.value);
    }

    public void SetCast(bool iscast)
    {
        animator.SetBool("IsCast", iscast);
    }
    public void SetCharge(bool ischarge)
    {
        animator.SetBool("IsCharge", ischarge);
    }

    public void SetRun(bool isrun)
    {
        //Debug.Log("isrun : " + isrun);
        animator.SetBool("IsMoving", isrun);
    }
    public void SetAttack()
    {
        // Debug.Log("isattack : " + isattack);
        animator.SetTrigger("IsAttack");
    }
    public void SetDamage(bool isdamage)
    {
        //Debug.Log("isdamage : " + isdamage);
        //animator.SetBool("isDamage", isdamage);
        animator.SetTrigger("IsDamage");
    }

    public void SetDeath()
    {
        //animator.SetBool("isDeath", isdeath);

        animator.SetTrigger("IsDeath");
    }

    //public void OnAttack()
    //{ 
    //    unit.OnAttack();
    //}
    #region 애니메이션 이벤트가 안맞아서 일단은 폐기
    //public void OnAttackAnimationFinished()
    //{
    //    SetAttack(false);
    //    Debug.Log("attack");
    //    unit.OnAttackFinish();
    //}
    //public void OnChargeAnimationFinished()
    //{
    //    SetCharge(false);

    //    unit.OnChargeFinish();
    //}
    //public void OnCastAnimationFinished()
    //{
    //    SetCast(false);

    //    unit.OnCastFinish();
    //}
    #endregion
}

