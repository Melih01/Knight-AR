using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAnimatorParameter
{
    Speed,
    Hit,
    SlashAttack,
    JumpAttack,
    MagicAttack,
    ShieldDefence,
    Revive,
    Die
}

public class PlayerAnimationController : CustomMonoBehaviour
{
    public Animator Anim { get; private set; }
    public bool IsAttack { get; private set; }

    [Space]
    [SerializeField]
    Collider swordContactCollider;

    #region AnimationParameters
    public static int SpeedParameter = Animator.StringToHash("Speed");
    public static int HitParameter = Animator.StringToHash("Hit");
    public static int SlashAttackParameter = Animator.StringToHash("SlashAttack");
    public static int JumpAttackParameter = Animator.StringToHash("JumpAttack");
    public static int MagicAttackParameter = Animator.StringToHash("MagicAttack");
    public static int ShieldDefenceParameter = Animator.StringToHash("ShieldDefence");
    public static int ReviveParameter = Animator.StringToHash("Revive");
    public static int DieParameter = Animator.StringToHash("Die");
    #endregion

    protected Dictionary<PlayerAnimatorParameter, int> animParameterDic = new Dictionary<PlayerAnimatorParameter, int>();

    PlayerController playerController;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        animParameterDic.Add(PlayerAnimatorParameter.Speed, SpeedParameter);
        animParameterDic.Add(PlayerAnimatorParameter.Hit, HitParameter);
        animParameterDic.Add(PlayerAnimatorParameter.SlashAttack, SlashAttackParameter);
        animParameterDic.Add(PlayerAnimatorParameter.JumpAttack, JumpAttackParameter);
        animParameterDic.Add(PlayerAnimatorParameter.MagicAttack, MagicAttackParameter);
        animParameterDic.Add(PlayerAnimatorParameter.ShieldDefence, ShieldDefenceParameter);
        animParameterDic.Add(PlayerAnimatorParameter.Revive, ReviveParameter);
        animParameterDic.Add(PlayerAnimatorParameter.Die, DieParameter);
    }

    #region SetAnimationParameterMethods
    public virtual void SetAnimationParameter<T>(PlayerAnimatorParameter animParameter, T value) // Created for Future
    {
        switch (animParameter)
        {
            case PlayerAnimatorParameter.Speed:
                float speed = float.Parse(value.ToString());
                SetSpeedForParameter(animParameter, speed);
                break;
        }
    }

    public virtual void SetAnimationParameter(PlayerAnimatorParameter animParameter)
    {
        switch (animParameter)
        {
            case PlayerAnimatorParameter.SlashAttack:
            case PlayerAnimatorParameter.JumpAttack:
            case PlayerAnimatorParameter.MagicAttack:
                AttackStateStarted();
                break;
            case PlayerAnimatorParameter.ShieldDefence:
            case PlayerAnimatorParameter.Hit:
            case PlayerAnimatorParameter.Revive:
            case PlayerAnimatorParameter.Die:
                AttackStateFinished();
                break;
        }

        Anim.SetTrigger(animParameterDic[animParameter]);
    }
    #endregion

    protected virtual void SetSpeedForParameter(PlayerAnimatorParameter animParameter, float speed)
    {
        Anim.SetFloat(animParameterDic[animParameter], speed);
    }

    //public void SetSlashAttack()
    //{
    //    Anim.SetTrigger("SlashAttack");
    //    AttackStateStarted();
    //}

    //public void SetJumpAttack()
    //{
    //    Anim.SetTrigger("JumpAttack");
    //    AttackStateStarted();
    //}

    //public void SetMagicAttack()
    //{
    //    Anim.SetTrigger("MagicAttack");
    //    AttackStateStarted();
    //}

    //public void SetShieldDefence()
    //{
    //    Anim.SetTrigger("ShieldDefence");
    //}

    //public void SetSpeed(float speed)
    //{
    //    Anim.SetFloat("Speed", speed);
    //}

    //public void SetHit()
    //{
    //    SetDisableSwordCollider();
    //    Anim.SetTrigger("Hit");
    //    HitAnimationStateStarted();
    //}

    //public void SetRevive()
    //{
    //    Anim.SetTrigger("Revive");
    //    playerController.PlayerMovementController.IsCanControl = true;
    //}

    //public void SetDie()
    //{
    //    SetDisableSwordCollider();
    //    Anim.SetTrigger("Die");
    //    playerController.PlayerMovementController.IsCanControl = false;
    //}

    public void AttackStateStarted() //Animation function
    {
        IsAttack = true;
        playerController.PlayerMovementController.IsCanControl = false;
    }

    public void AttackStateFinished() //Animation function
    {
        IsAttack = false;
        playerController.PlayerMovementController.IsCanControl = true;
    }

    public void HitAnimationStateStarted() //Animation function
    {
        playerController.PlayerMovementController.IsCanControl = false;
    }

    public void HitAnimationStateFinished() //Animation function
    {
        playerController.PlayerMovementController.IsCanControl = true;
    }

    public void SetEnableSwordCollider() //Animation function
    {
        swordContactCollider.enabled = true;
    }

    public void SetDisableSwordCollider() //Animation function
    {
        swordContactCollider.enabled = false;
    }

    public void GetMagicProjectile() //Animation function
    {

    }

    public void ThrowMagicProjectile() //Animation function
    {

    }
}
