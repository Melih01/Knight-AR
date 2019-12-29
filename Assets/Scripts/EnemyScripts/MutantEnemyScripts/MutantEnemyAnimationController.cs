using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyAnimationController : EnemyAnimationController
{
    [Space]
    [SerializeField]
    Collider mutantShapeHandContactCollider;

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
