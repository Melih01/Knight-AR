using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : CustomMonoBehaviour
{
    public Animator Anim { get; protected set; }
    public bool IsAttack { get; protected set; }

    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public virtual void SetAttack()
    {
        Anim.SetTrigger("Attack");
        AttackStateStarted();
    }

    public virtual void SetSpeed(float speed)
    {
        Anim.SetFloat("Speed", speed);
    }

    public virtual void SetHit()
    {
        AttackStateFinished();
        Anim.SetTrigger("Hit");
    }

    public virtual void SetRevive()
    {
        AttackStateFinished();
        Anim.SetTrigger("Revive");
    }

    public virtual void SetDie()
    {
        Anim.SetTrigger("Die");
    }

    public virtual void AttackStateStarted() //Animation function
    {
        IsAttack = true;
    }

    public virtual void AttackStateFinished() //Animation function
    {
        IsAttack = false;
    }
}
