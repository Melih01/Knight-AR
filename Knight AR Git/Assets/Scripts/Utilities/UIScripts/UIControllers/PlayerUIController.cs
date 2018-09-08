using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerUIController : CustomMonoBehaviour
{
    [Header("Player Health:")]
    [SerializeField]
    Text healthText;
    [Space]
    [SerializeField]
    Slider healthSlider;

    [Header("Player Buttons:")]
    [SerializeField]
    Button slashAttackButton;
    [Space]
    [SerializeField]
    Button jumpAttackButton;
    [Space]
    [SerializeField]
    Button magicAttackButton;
    [Space]
    [SerializeField]
    Button shieldDefenceButton;

    PlayerController playerController;

    void Start()
    {
        //Check Until PlayerController is not Null
        StartCoroutine(WaitUntilConditionHappenCoroutine(ConditionFunc: () =>
        {
            bool condition = GameManager.instance?.playerController;
            return condition;
        },
        action: () =>
        {
            playerController = GameManager.instance.playerController;
            SetHealthMaxForTextAndSlider();
            AddListeners();
        }));
    }

    public void OnEnable()
    {
        if (playerController)
        {
            SetHealthMaxForTextAndSlider();

            AddListeners();
        }
    }

    public void OnDisable()
    {
        if (playerController) RemoveListeners();
    }

    void AddListeners()
    {
        #region AttackButtonAddListeners

        slashAttackButton.onClick.AddListener(call: () => OnClickedSlashAttackButton(playerController));
        jumpAttackButton.onClick.AddListener(call: () => OnClickedJumpAttackButton(playerController));
        magicAttackButton.onClick.AddListener(call: () => OnClickedMagicAttackButton(playerController));
        shieldDefenceButton.onClick.AddListener(call: () => OnClickedShieldDefenceButton(playerController));

        #endregion

        playerController.PlayerGetDamaged += OnPlayerGetDamage;
    }

    void RemoveListeners()
    {
        #region AttackButtonRemoveListeners

        slashAttackButton.onClick.RemoveAllListeners();
        jumpAttackButton.onClick.RemoveAllListeners();
        magicAttackButton.onClick.RemoveAllListeners();
        shieldDefenceButton.onClick.RemoveAllListeners();

        #endregion

        playerController.PlayerGetDamaged -= OnPlayerGetDamage;
    }

    void SetHealthMaxForTextAndSlider()
    {
        healthText.text = playerController.AttributesController.attributesInfoAsset.health.ToString();
        healthSlider.maxValue = playerController.AttributesController.attributesInfoAsset.health;
        healthSlider.value = healthSlider.maxValue;
    }

    public void OnPlayerGetDamage(PlayerController playerController)
    {

        if (playerController.AttributesController.health > 0)
        {
            healthText.text = playerController.AttributesController.health.ToString();
            healthSlider.value = playerController.AttributesController.health;
        }
        else
        {
            healthText.text = "0";
            healthSlider.value = healthSlider.minValue;
        }

    }

    public void OnClickedSlashAttackButton(PlayerController playerController)
    {
        playerController.AnimationController.SetSlashAttack();
        Debug.Log("SlashAttack");
    }

    public void OnClickedJumpAttackButton(PlayerController playerController)
    {
        playerController.AnimationController.SetJumpAttack();
        Debug.Log("JumpAttack");
    }

    public void OnClickedMagicAttackButton(PlayerController playerController)
    {
        playerController.AnimationController.SetMagicAttack();
        Debug.Log("MagicAttack");
    }

    public void OnClickedShieldDefenceButton(PlayerController playerController)
    {
        playerController.AnimationController.SetShieldDefence();
        Debug.Log("ShieldDefence");
    }
}