using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyAnimationController : EnemyAnimationController
{
    [Space]
    [SerializeField]
    Collider mutantShapeHandContactCollider;

    public override void SetAttack()
    {
        AttackStateStarted();
        Anim.SetTrigger("Attack");
    }

    public override void SetSpeed(float speed)
    {
        Anim.SetFloat("Speed", speed);
    }

    public override void SetHit()
    {
        AttackStateFinished();
        Anim.SetTrigger("Hit");
    }

    public override void SetRevive()
    {
        AttackStateFinished();
        Anim.SetTrigger("Revive");
    }

    public override void SetDie()
    {
        Anim.SetTrigger("Die");
    }

    public override void AttackStateStarted() //Animation function
    {
        IsAttack = true;
    }

    public override void AttackStateFinished() //Animation function
    {
        SetDisableMutantShapeHandCollider();
        IsAttack = false;
    }

    public void SetEnableMutantShapeHandCollider() //Animation function
    {
        mutantShapeHandContactCollider.enabled = true;
    }

    public void SetDisableMutantShapeHandCollider() //Animation function
    {
        mutantShapeHandContactCollider.enabled = false;
    }
}
