using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyMovementController : EnemyMovementController
{
    protected override void Update()
    {
        if (EnemyController.Target != null && EnemyController.Target.AttributesController.health > 0)
            base.Update();
        else
            EnemyController.AnimationController.SetSpeed(0);
    }
}
