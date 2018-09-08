using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationParameter
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

    protected Dictionary<AnimationParameter, int> animParameterDic = new Dictionary<AnimationParameter, int>();
    protected Animation animation;

    protected virtual void Awake()
    {
        Anim = GetComponent<Animator>();

        animParameterDic.Add(AnimationParameter.Speed, SpeedParameter);
        animParameterDic.Add(AnimationParameter.Hit, HitParameter);
        animParameterDic.Add(AnimationParameter.Attack, AttackParameter);
        animParameterDic.Add(AnimationParameter.Revive, ReviveParameter);
        animParameterDic.Add(AnimationParameter.Die, DieParameter);
    }

    #region SetAnimationParameterMethods
    public virtual void SetAnimationParameter<T>(AnimationParameter animParameter, T value) // Created for Future
    {
        switch (animParameter)
        {
            case AnimationParameter.Speed:
                float speed = float.Parse(value.ToString());
                SetSpeedForParameter(animParameter,speed);
                break;
        }
    }

    public virtual void SetAnimationParameter(AnimationParameter animParameter)
    {
        switch (animParameter)
        {   
            case AnimationParameter.Attack:
                AttackStateStarted();
                break;
            case AnimationParameter.Hit:
            case AnimationParameter.Revive:
            case AnimationParameter.Die:
                AttackStateFinished();
                break;
        }

        Anim.SetTrigger(animParameterDic[animParameter]);
    }
    #endregion

    protected virtual void SetSpeedForParameter(AnimationParameter animParameter,float speed)
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
