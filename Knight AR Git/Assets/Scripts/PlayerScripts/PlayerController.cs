using CnControls;
using UnityEngine;

public class PlayerController : CustomMonoBehaviour, IDamageable
{
    public PlayerAttributesController AttributesController { get; set; }
    public PlayerAnimationController AnimationController { get; set; }

    public bool IsCanControl { get; set; } = true;

    public event System.Action<PlayerController> PlayerGetDamaged;
    public event System.Action<PlayerController> PlayerDied;

    [Space]
    [SerializeField]
    public Collider contactCollider;
    [Space]
    [SerializeField]
    Transform damagePopupSpawnPoint;

    float inputMoveSpeed;
    bool isWillMove = false;

    CharacterController characterController;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        AttributesController = GetComponent<PlayerAttributesController>();
        AnimationController = GetComponent<PlayerAnimationController>();

        characterController.detectCollisions = false;
    }

    void Start()
    {
        GameManager.instance.playerController = this;
    }

    void Update()
    {
        Move();
        AnimationController.SetSpeed(inputMoveSpeed);
    }

    void Move()
    {
        if (IsCanControl)
        {
            var inputVector = new Vector2(CnInputManager.GetAxis("Horizontal"), CnInputManager.GetAxis("Vertical"));
            Vector3 movementVector = inputVector;

            isWillMove = inputVector != Vector2.zero ? true : false;

            if (isWillMove)
            {
                var angle = inputVector.GetAngle();
                transform.rotation = Quaternion.Euler(0, angle, 0);

                movementVector += Physics.gravity;
                characterController.Move(movementVector * Time.deltaTime * AttributesController.speed);
                inputMoveSpeed = inputVector.magnitude;
            }
            else
            {
                inputMoveSpeed = Vector3.zero.magnitude;
            }
        }
        else
        {
            inputMoveSpeed = Vector3.zero.magnitude;
        }
    }

    public virtual void Die()
    {
        GameManager.instance.screenUIController.gameResultUIController.ShowGameResultUI(false);
        GameManager.instance.screenUIController.playerUIController.OnDisable();

        PlayerDied?.Invoke(this);
        contactCollider.enabled = false;

        AnimationController.SetDie();
    }

    public virtual void Revive()
    {
        if (AttributesController.health <= 0)
        {
            AnimationController.SetRevive();
            contactCollider.enabled = true;
        }

        AttributesController.ResetAllAttributes();
        GameManager.instance.screenUIController.playerUIController.OnEnable();
    }

    #region IDamageable

    public void ApplyDamage(float damage, DamageElement damageElement = DamageElement.Physical)
    {
        if (damage > 0)
            GameManager.instance.objectPoolManager.Spawn(ObjectPool.DamagePopup, damagePopupSpawnPoint, damage);

        if (AttributesController.health > 0)
        {
            var TotalDamage = damage - AttributesController.armor;
            AttributesController.health -= TotalDamage;
            PlayerGetDamaged?.Invoke(this);
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