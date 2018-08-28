using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyAnimationController : CustomMonoBehaviour
{
    public Animator Anim { get; private set; }
    public bool IsAttack { get; private set; }

    [Space]
    [SerializeField]
    Collider mutantShapeHandContactCollider;

    void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    public void SetAttack()
    {
        Anim.SetTrigger("Attack");
        AttackStateStarted();
    }

    public void SetSpeed(float speed)
    {
        Anim.SetFloat("Speed", speed);
    }

    public void SetHit()
    {
        AttackStateFinished();
        Anim.SetTrigger("Hit");
    }

    public void SetRevive()
    {
        AttackStateFinished();
        Anim.SetTrigger("Revive");
    }

    public void SetDie()
    {
        Anim.SetTrigger("Die");
    }

    public void AttackStateStarted() //Animation function
    {
        IsAttack = true;
    }

    public void AttackStateFinished() //Animation function
    {
        IsAttack = false;
        SetDisableMutantShapeHandCollider();
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
