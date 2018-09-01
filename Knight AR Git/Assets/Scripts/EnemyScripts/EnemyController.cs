using UnityEngine;

public abstract class EnemyController : CustomMonoBehaviour, IDamageable
{
    public EnemyAttributesController AttributesController { get; set; }
    public EnemyAnimationController AnimationController { get; protected set; }
    public EnemyMovementController MovementController { get; protected set; }
    public PlayerController Target { get; private set; }

    public event System.Action<EnemyController> EnemyGetDamaged;
    public event System.Action<EnemyController> EnemyDied;

    [Space]
    [SerializeField]
    Collider contactCollider;
    [Space]
    [SerializeField]
    Transform damagePopupSpawnPoint;

    protected bool isPlayerDead;

    protected virtual void Awake()
    {
        AnimationController = GetComponent<EnemyAnimationController>();
        AttributesController = GetComponent<EnemyAttributesController>();
        MovementController = GetComponent<EnemyMovementController>();
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
            Target = GameManager.instance.playerController;
        }));
    }

    protected virtual void Update()
    {
    }

    protected virtual void Die()
    {
        AnimationController.SetDie();
        EnemyDied?.Invoke(this);
        contactCollider.enabled = false;

        GameManager.instance.screenUIController.gameResultUIController.ShowGameResultUI(true);
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
            GameManager.instance.objectPoolManager.Spawn(ObjectPoolType.DamagePopup, damagePopupSpawnPoint, damage); 

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