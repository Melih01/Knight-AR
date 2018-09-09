using CnControls;
using UnityEngine;

public class PlayerController : CustomMonoBehaviour, IDamageable
{
    public PlayerAttributesController AttributesController { get; set; }
    public PlayerAnimationController AnimationController { get; set; }
    public PlayerMovementController PlayerMovementController { get; set; }

    public event System.Action<PlayerController> PlayerGetDamaged;
    public event System.Action<PlayerController> PlayerDied;

    [Space]
    [SerializeField]
    public Collider contactCollider;
    [Space]
    [SerializeField]
    Transform damagePopupSpawnTransform;
    [Space]
    [SerializeField]
    Transform bloodEffectSpawnTransform;

    void Awake()
    {
        AttributesController = GetComponent<PlayerAttributesController>();
        AnimationController = GetComponent<PlayerAnimationController>();
        PlayerMovementController = GetComponent<PlayerMovementController>();
    }

    void Start()
    {
        GameManager.instance.playerController = this;
    }

    void OnEnable()
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

    public virtual void Die()
    {
        GameManager.instance.screenUIController.gameResultUIController.ShowGameResultUI(false);
        GameManager.instance.screenUIController.playerUIController.OnDisable();

        PlayerDied?.Invoke(this);
        contactCollider.enabled = false;

        AnimationController.SetAnimationParameter(PlayerAnimatorParameter.Die); //AnimationController.SetDie();
    }

    public virtual void Revive()
    {
        if (AttributesController.health <= 0)
        {
            AnimationController.SetAnimationParameter(PlayerAnimatorParameter.Revive); //AnimationController.SetRevive();
            contactCollider.enabled = true;
        }

        AttributesController.ResetAllAttributes();
        GameManager.instance.screenUIController.playerUIController.OnEnable();
    }

    #region IDamageable

    public void ApplyDamage(float damage, DamageElement damageElement = DamageElement.Physical)
    {
        var TotalDamage = damage - AttributesController.armor;

        GameManager.instance.objectPoolManager.Spawn(ObjectPoolType.DamagePopup, damagePopupSpawnTransform, transform.localScale, TotalDamage);

        if (AttributesController.health > 0)
        {
            AttributesController.health -= TotalDamage;
            PlayerGetDamaged?.Invoke(this); /// Used For PlayerUI.

            ///Blood Effect Spawn.
            GameManager.instance.objectPoolManager.Spawn(ObjectPoolType.BloodSprayEffect, bloodEffectSpawnTransform, transform.localScale);
        }

        if (AttributesController.health <= 0)
        {
            Die();
        }
        else
        {
            AnimationController.SetAnimationParameter(PlayerAnimatorParameter.Hit);  //AnimationController.SetHit();
        }
    }

    #endregion
}