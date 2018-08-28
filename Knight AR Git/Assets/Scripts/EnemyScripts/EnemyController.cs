using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyController : CustomMonoBehaviour, IDamageable
{
    public NavMeshAgent agent { get; private set; }
    public EnemyAttributesController AttributesController { get; set; }
    public MutantEnemyAnimationController AnimationController { get; private set; }

    public event System.Action<EnemyController> EnemyGetDamaged;
    public event System.Action<EnemyController> EnemyDied;

    [Space]
    [SerializeField]
    Collider contactCollider;
    [Space]
    [SerializeField]
    Transform damagePopupSpawnPoint;

    protected PlayerController target;
    protected bool isPlayerDead;

    protected virtual void Awake()
    {
        AnimationController = GetComponent<MutantEnemyAnimationController>();
        AttributesController = GetComponent<EnemyAttributesController>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
    }

    protected virtual void Start()
    {
        GameManager.instance.enemyController = this;

        //Check Until PlayerController is not Null
        StartCoroutine(WaitUntilConditionHappenCoroutine(ConditionFunc: () =>
        {
            bool condition = GameManager.instance.playerController != null;
            return condition;
        },
        action: () =>
        {
            target = GameManager.instance.playerController;
        }));
    }

    protected virtual void Update()
    {
        MoveToTarget();
    }

    protected virtual void MoveToTarget()
    {
        if (target && target.AttributesController.health > 0 && AttributesController.health > 0)
        {
            if (!AnimationController.IsAttack && AttributesController.health > 0)
                transform.LookAt(target.transform.position);

            if (agent.isOnNavMesh)
            {
                float distance = Vector3.Distance(transform.position, target.transform.position);
                agent.SetDestination(target.transform.position);

                if (distance <= agent.stoppingDistance && !AnimationController.IsAttack)
                {
                    StopAndAttack();
                }
                else if (distance > agent.stoppingDistance && !AnimationController.IsAttack)
                {
                    Move();
                }
            }
        }
    }

    protected virtual void StopAndAttack()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        AnimationController.SetAttack();
    }

    protected virtual void Move()
    {
        agent.isStopped = false;
        AnimationController.SetSpeed(agent.velocity.magnitude);

        var speedWithAnim = (AnimationController.Anim.deltaPosition / Time.deltaTime).magnitude - 1;
        if (speedWithAnim > 0)
            agent.speed = speedWithAnim;
    }

    protected virtual void Die()
    {
        AnimationController.SetDie();
        EnemyDied?.Invoke(this);
        contactCollider.enabled = false;

        GameManager.instance.screenUIController.gameResultUIController.ShowGameResultUI(true);

        //StartCoroutine(WaitForSeconds(3, () =>
        //{
        //    gameObject.SetActive(false);
        //}));
    }

    public virtual void Revive()
    {
        if (AttributesController.health <= 0)
        {
            AnimationController.SetRevive();
            contactCollider.enabled = true;
        }

        AttributesController.ResetAllAttributes();
    }

    #region IDamageable

    public virtual void ApplyDamage(float damage, DamageElement damageElement = DamageElement.Physical)
    {
        if (damage > 0)
            GameManager.instance.objectPoolManager.Spawn(ObjectPool.DamagePopup, damagePopupSpawnPoint, damage); 

        if (AttributesController.health > 0)
        {
            AttributesController.health -= damage;
            EnemyGetDamaged?.Invoke(this);
        }

        if (AttributesController.health <= 0)
        {
            Die();
        }
        else
        {
            AnimationController.SetHit();
        }
    }

    #endregion
}