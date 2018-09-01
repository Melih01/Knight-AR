using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : CustomMonoBehaviour
{
    public Animator Anim { get; private set; }
    public bool IsAttack { get; private set; }

    [Space]
    [SerializeField]
    Collider swordContactCollider;

    PlayerController playerController;

    void Awake()
    {
        Anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }

    public void SetSlashAttack()
    {
        Anim.SetTrigger("SlashAttack");
        AttackStateStarted();
    }

    public void SetJumpAttack()
    {
        Anim.SetTrigger("JumpAttack");
        AttackStateStarted();
    }

    public void SetMagicAttack()
    {
        Anim.SetTrigger("MagicAttack");
        AttackStateStarted();
    }

    public void SetShieldDefence()
    {
        Anim.SetTrigger("ShieldDefence");
    }

    public void SetSpeed(float speed)
    {
        Anim.SetFloat("Speed", speed);
    }

    public void SetHit()
    {
        SetDisableSwordCollider();
        Anim.SetTrigger("Hit");
        HitAnimationStateStarted();
    }

    public void SetRevive()
    {
        Anim.SetTrigger("Revive");
        playerController.PlayerMovementController.IsCanControl = true;
    }

    public void SetDie()
    {
        SetDisableSwordCollider();
        Anim.SetTrigger("Die");
        playerController.PlayerMovementController.IsCanControl = false;
    }

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
