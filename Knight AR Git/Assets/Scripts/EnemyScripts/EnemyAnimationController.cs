using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyAnimatorParameter
{
    Speed,
    Hit,
    Attack,
    Revive,
    Die
}

public class EnemyAnimationController : CustomMonoBehaviour
{
    public Animator Anim { get; protected set; }
    public bool IsAttack { get; protected set; }

    #region AnimationParameters
    public static int SpeedParameter = Animator.StringToHash("Speed");
    public static int HitParameter = Animator.StringToHash("Hit");
    public static int AttackParameter = Animator.StringToHash("Attack");
    public static int ReviveParameter = Animator.StringToHash("Revive");
    public static int DieParameter = Animator.StringToHash("Die");
    #endregion

    protected Dictionary<EnemyAnimatorParameter, int> animParameterDic = new Dictionary<EnemyAnimatorParameter, int>();

    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();

        animParameterDic.Add(EnemyAnimatorParameter.Speed, SpeedParameter);
        animParameterDic.Add(EnemyAnimatorParameter.Hit, HitParameter);
        animParameterDic.Add(EnemyAnimatorParameter.Attack, AttackParameter);
        animParameterDic.Add(EnemyAnimatorParameter.Revive, ReviveParameter);
        animParameterDic.Add(EnemyAnimatorParameter.Die, DieParameter);
    }

    #region SetAnimationParameterMethods
    public virtual void SetAnimationParameter<T>(EnemyAnimatorParameter animParameter, T value) // Created for Future
    {
        switch (animParameter)
        {
            case EnemyAnimatorParameter.Speed:
                float speed = float.Parse(value.ToString());
                SetSpeedForParameter(animParameter,speed);
                break;
        }
    }

    public virtual void SetAnimationParameter(EnemyAnimatorParameter animParameter)
    {
        switch (animParameter)
        {   
            case EnemyAnimatorParameter.Attack:
                AttackStateStarted();
                break;
            case EnemyAnimatorParameter.Hit:
            case EnemyAnimatorParameter.Revive:
            case EnemyAnimatorParameter.Die:
                AttackStateFinished();
                break;
        }

        Anim.SetTrigger(animParameterDic[animParameter]);
    }
    #endregion

    protected virtual void SetSpeedForParameter(EnemyAnimatorParameter animParameter,float speed)
    {
        Anim.SetFloat(animParameterDic[animParameter], speed);
    }

    public virtual void AttackStateStarted() //Animation function
    {
        IsAttack = true;
    }

    public virtual void AttackStateFinished() //Animation function
    {
        IsAttack = false;
    }
}
