using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : CustomMonoBehaviour
{
    [Header("Enemy Health:")]
    [SerializeField]
    Text healthText;
    [Space]
    [SerializeField]
    Slider healthSlider;

    EnemyController enemyController;

    void Start()
    {
        //Check Until EnemyController is not Null
        StartCoroutine(WaitUntilConditionHappenCoroutine(ConditionFunc: () =>
        {
            bool condition = GameManager.instance?.enemyController;
            return condition;
        },
        action: () =>
        {
            enemyController = GameManager.instance.enemyController;
            SetHealthMaxForTextAndSlider();

            enemyController.EnemyGetDamaged += OnEnemyGetDamage;
        }));
    }

    void OnEnable()
    {
        if (enemyController)
        {
            SetHealthMaxForTextAndSlider();

            enemyController.EnemyGetDamaged += OnEnemyGetDamage;
        }
    }

    void OnDisable()
    {
        if (enemyController)
            enemyController.EnemyGetDamaged -= OnEnemyGetDamage;
    }

    void SetHealthMaxForTextAndSlider()
    {
        healthText.text = enemyController.AttributesController.attributesInfoAsset.health.ToString();
        healthSlider.maxValue = enemyController.AttributesController.attributesInfoAsset.health;
        healthSlider.value = healthSlider.maxValue;
    }

    public void ResetAllAttributes()
    {
        if (enemyController) SetHealthMaxForTextAndSlider();
    }

    void OnEnemyGetDamage(EnemyController enemyController)
    {
        if (enemyController.AttributesController.health > 0)
        {
            healthText.text = enemyController.AttributesController.health.ToString();
            healthSlider.value = enemyController.AttributesController.health;
        }
        else
        {
            healthText.text = "0";
            healthSlider.value = healthSlider.minValue;
        }
    }
}