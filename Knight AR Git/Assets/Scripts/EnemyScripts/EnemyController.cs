using UnityEngine;

public abstract class EnemyController : CustomMonoBehaviour, IDamageable
{
    public EnemyAttributesController AttributesController { get; set; }
    public EnemyAnimationController AnimationController { get; protected set; }
    public EnemyMovementController MovementController { get; protected set; }

    public event System.Action<EnemyController> EnemyGetDamaged;
    public event System.Action<EnemyController> EnemyDied;

    [Space]
    [SerializeField]
    Collider contactCollider;
    [Space]
    [SerializeField]
    Transform damagePopupSpawnTransform;
    [Space]
    [SerializeField]
    Transform bloodEffectSpawnTransform;

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
    }

    protected virtual void OnEnable()
    {
        //Check Until GameManager instance is not Null
        StartCoroutine(WaitUntilConditionHappenCoroutine(ConditionFunc: () =>
        {
            bool condition = GameManager.instance?.gamePlayMode == GamePlayMode.AR;
            return condition;
        },
        action: () =>
        {
            transform.localScale = Vector3.one * GameManager.instance.characterLocalScaleForAR;
            AttributesController.ResetAllAttributes();
        }));       
    }

    protected virtual void Update()
    {
    }

    #region IDamageable

    public virtual void ApplyDamage(float damage, DamageElement damageElement = DamageElement.Physical)
    {
        if (damage > 0)
            GameManager.instance.objectPoolManager.Spawn(ObjectPoolType.DamagePopup, damagePopupSpawnTransform, transform.localScale, damage);

        if (AttributesController.health > 0)
        {
            AttributesController.health -= damage;
            EnemyGetDamaged?.Invoke(this);

            ///Blood Effect Spawn.
            GameManager.instance.objectPoolManager.Spawn(ObjectPoolType.BloodSprayEffect, bloodEffectSpawnTransform, transform.localScale);
        }

        if (AttributesController.health <= 0)
        {
            Die();
        }
        else
        {
            AnimationController.SetAnimationParameter(AnimationParameter.Hit);
        }
    }

    #endregion

    protected virtual void Die()
    {
        AnimationController.SetAnimationParameter(AnimationParameter.Die);
        EnemyDied?.Invoke(this);
        contactCollider.enabled = false;

        GameManager.instance.screenUIController.gameResultUIController.ShowGameResultUI(true);
    }

    public virtual void Revive()
    {
        if (AttributesController.health <= 0)
        {
            AnimationController.SetAnimationParameter(AnimationParameter.Revive);
            contactCollider.enabled = true;
        }

        AttributesController.ResetAllAttributes();
    }
}