using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopupController : CustomMonoBehaviour
{
    public float damage { get; set; } = 0;

    TextMeshPro text;

    void OnEnable()
    {
        if (!text)
            text = GetComponent<TextMeshPro>();

        text.text = damage.ToString();

        float firstLocalPosionY = transform.localPosition.y;

        StartCoroutine(LerpConditionCoroutine(
        ConditionFunc: () =>
        {
            return transform.localPosition.y > (firstLocalPosionY + 1);
        },
        lerpAction: () =>
        {
            MoveUp();

            transform.LookAt(transform.position - Camera.main.transform.position);

            if (transform.localPosition.y > firstLocalPosionY + (0.2f * GameManager.instance?.characterLocalScaleForAR))
                ChangeTextAlpha();
        },
        finishAction: () =>
        {
            gameObject.SetActive(false);
        }));
    }

    void OnDisable()
    {
        ResetColor();
        transform.localPosition = Vector3.zero;
        GameManager.instance.objectPoolManager.damagePopupObjectPoolList.ObjectPoolReturn(gameObject);
    }

    void MoveUp()
    {
        float moveUp = Mathf.Lerp(0, 1, 0.5f * Time.fixedDeltaTime);
        transform.localPosition += new Vector3(0, moveUp, 0);
    }

    void ChangeTextAlpha()
    {
        float alphaLerp = Mathf.Lerp(0.01f, 0, 0.5f * Time.fixedDeltaTime);
        Color color = text.color;
        color.a -= alphaLerp;
        text.color = color;
    }

    void ResetColor()
    {
        Color color = text.color;
        color.a = 1;
        text.color = color;
    }
}
