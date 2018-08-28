using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyController : EnemyController
{
    protected override void Update()
    {
        if (target != null && target.AttributesController.health > 0)
            base.Update();
        else
            AnimationController.SetSpeed(0);
    }

    //private IEnumerator WaitAndLook(float time)
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(time);
    //        var targetRotation = Quaternion.LookRotation(target.transform.position - transform.position);
    //        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, time * Time.deltaTime);
    //    }
    //}
}
