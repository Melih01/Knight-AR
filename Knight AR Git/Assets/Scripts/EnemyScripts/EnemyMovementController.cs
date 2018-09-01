using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyMovementController : CustomMonoBehaviour
{
    public NavMeshAgent Agent { get; set; }
    public EnemyController EnemyController { get; private set; }

    protected virtual void Awake()
    {
        EnemyController = GetComponent<EnemyController>();
        Agent = GetComponent<NavMeshAgent>();
        Agent.updateRotation = false;
    }

    protected virtual void Update()
    {
        MoveToTarget();
    }

    protected virtual void MoveToTarget()
    {
        if (EnemyController.Target && EnemyController.Target.AttributesController.health > 0 && EnemyController.AttributesController.health > 0)
        {
            if (!EnemyController.AnimationController.IsAttack && EnemyController.AttributesController.health > 0)
                transform.LookAt(EnemyController.Target.transform.position);

            if (Agent.isOnNavMesh)
            {
                float distance = Vector3.Distance(transform.position, EnemyController.Target.transform.position);
                Agent.SetDestination(EnemyController.Target.transform.position);

                if (distance <= Agent.stoppingDistance && !EnemyController.AnimationController.IsAttack)
                {
                    StopAndAttack();
                }
                else if (distance > Agent.stoppingDistance && !EnemyController.AnimationController.IsAttack)
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
        EnemyController.AnimationController.SetAttack();
    }

    protected virtual void Move()
    {
        Agent.isStopped = false;
        EnemyController.AnimationController.SetSpeed(Agent.velocity.magnitude);

        var speedWithAnim = (EnemyController.AnimationController.Anim.deltaPosition / Time.deltaTime).magnitude - 1;
        if (speedWithAnim > 0)
            Agent.speed = speedWithAnim;
    }
}
