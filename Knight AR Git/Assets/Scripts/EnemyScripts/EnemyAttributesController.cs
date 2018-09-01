using UnityEngine;

public class EnemyAttributesController : CustomMonoBehaviour
{
    [Space]
    public EnemyAttributesInfoAsset attributesInfoAsset;
    public EnemyController EnemyController { get; set; }

    public float speed { get; set; }
    public float stoppingDistance { get; set; }
    public float health { get; set; }
    public float regularDamage { get; set; }
    public float criticalChance { get; set; }
    public float armorPiece { get; set; }
    public float armor { get; set; }
    public DamageElement damageElement { get; set; }

    public virtual float TotalDamage
    {
        get
        {
            return attributesInfoAsset.regularDamage +
              attributesInfoAsset.criticalChance > Random.Range(0, 1) ? (attributesInfoAsset.regularDamage * 2) : 0 +
              attributesInfoAsset.armorPiece;
        }
    }

    protected float extraARFactor = .3f;

    protected virtual void Awake()
    {
        health = attributesInfoAsset.health;
        regularDamage = attributesInfoAsset.regularDamage;
        criticalChance = attributesInfoAsset.criticalChance;
        armorPiece = attributesInfoAsset.armorPiece;
        armor = attributesInfoAsset.armor;
        damageElement = attributesInfoAsset.damageElement;

        EnemyController = GetComponent<EnemyController>();
    }

    protected virtual void Start()
    {
        SetGameObjectLocalScaleToGamePlayMode();
        speed = SetAttributeToGamePlayMode(attributesInfoAsset.moveSpeed);
        stoppingDistance = SetAttributeToGamePlayMode(attributesInfoAsset.stoppingDistance, extraARFactor);

        EnemyController.MovementController.Agent.speed = speed;
        EnemyController.MovementController.Agent.stoppingDistance = stoppingDistance;
    }

    public virtual void ResetAllAttributes()
    {
        health = attributesInfoAsset.health;
        regularDamage = attributesInfoAsset.regularDamage;
        criticalChance = attributesInfoAsset.criticalChance;
        armorPiece = attributesInfoAsset.armorPiece;
        armor = attributesInfoAsset.armor;
        damageElement = attributesInfoAsset.damageElement;

        SetGameObjectLocalScaleToGamePlayMode();
        speed = SetAttributeToGamePlayMode(attributesInfoAsset.moveSpeed);
        stoppingDistance = SetAttributeToGamePlayMode(attributesInfoAsset.stoppingDistance, extraARFactor);

        EnemyController.MovementController.Agent.speed = speed;
        EnemyController.MovementController.Agent.stoppingDistance = stoppingDistance;

        GameManager.instance.screenUIController.enemyUIController.ResetAllAttributes();
    }
}
