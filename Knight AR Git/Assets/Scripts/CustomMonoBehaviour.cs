using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomMonoBehaviour : MonoBehaviour
{
    protected virtual void SetGameObjectLocalScaleToGamePlayMode(/*ref float speed, float moveSpeedFromScriptableObject*/)
    {
        //gameObject.transform.localScale = Vector3.one;

        //switch (GameManager.instance.gamePlayMode)
        //{
        //    case GamePlayMode.Normal:
        //        break;
        //    case GamePlayMode.AR:
        //        gameObject.transform.localScale *= GameManager.instance.characterLocalScaleForAR;
        //        break;
        //}
    }

    protected virtual float SetAttributeToGamePlayMode(float attributeFromScriptableObject, float extraARFactor = 1f)
    {
        var attribute = attributeFromScriptableObject;

        switch (GameManager.instance.gamePlayMode)
        {
            case GamePlayMode.Normal:
                return attribute;
            case GamePlayMode.AR:
                return attribute *= GameManager.instance.characterLocalScaleForAR * extraARFactor;
            default:
                return attribute;
        }
    }

    public IEnumerator WaitForSecondsCoroutine(float time, System.Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }

    public IEnumerator WaitUntilConditionHappenCoroutine(System.Func<bool> ConditionFunc, System.Action action)
    {
        while (!ConditionFunc())
            yield return null;

        action();
    }

    public IEnumerator LerpConditionCoroutine(System.Func<bool> ConditionFunc, System.Action lerpAction, System.Action finishAction)
    {
        while (!ConditionFunc())
        {
            lerpAction();
            yield return null;
        }

        finishAction();
    }
}