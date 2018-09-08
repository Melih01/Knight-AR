using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovementController : CustomMonoBehaviour
{
    public NavMeshAgent Agent { get; set; }
    public EnemyController EnemyController { get; private set; }
    public PlayerController Target { get; private set; }

    protected virtual void Awake()
    {
        EnemyController = GetComponent<EnemyController>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;

        //Check Until PlayerController is not Null
        StartCoroutine(WaitUntilConditionHappenCoroutine(ConditionFunc: () =>
        {
            bool condition = GameManager.instance?.playerController;
            return condition;
        },
        action: () =>
        {
            Target = GameManager.instance.playerController;
        }));
    }

    protected virtual void Update()
    {
        if (Target != null && Target.AttributesController.health > 0)
            MoveToTarget();
        else
            EnemyController.AnimationController.SetAnimationParameter(AnimationParameter.Speed, 0);
    }

    protected virtual void MoveToTarget()
    {
        if (Target && Target.AttributesController.health > 0 && EnemyController.AttributesController.health > 0)
        {
            if (!EnemyController.AnimationController.IsAttack && EnemyController.AttributesController.health > 0)
                transform.LookAt(Target.transform.position);

            if (Agent.isOnNavMesh && !EnemyController.AnimationController.IsAttack)
            {
                float distance = Vector3.Distance(transform.position, Target.transform.position);
                Agent.SetDestination(Target.transform.position);

                if (distance <= Agent.stoppingDistance)
                {
                    StopAndAttack();
                }
                else if (distance > Agent.stoppingDistance)
                {
                    Move();
                }
            }
        }
    }

    protected virtual void StopAndAttack()
    {
        Agent.velocity = Vector3.zero;
        Agent.isStopped = true;
        EnemyController.AnimationController.SetAnimationParameter(AnimationParameter.Speed, 0);
        EnemyController.AnimationController.SetAnimationParameter(AnimationParameter.Attack);
    }

    protected virtual void Move()
    {
        Agent.isStopped = false;
        EnemyController.AnimationController.SetAnimationParameter(AnimationParameter.Speed, EnemyController.AttributesController.attributesInfoAsset.moveSpeed);

        var speedWithAnim = (EnemyController.AnimationController.Anim.deltaPosition / Time.deltaTime).magnitude - 1;
        if (speedWithAnim > 0)
            Agent.speed = speedWithAnim * EnemyController.AttributesController.extraARFactor;
    }
}
