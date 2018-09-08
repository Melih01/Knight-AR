using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodEffectController : CustomMonoBehaviour
{
    ParticleSystem particleSystem;

    void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        //GetComponentsInChildren<Transform>().DoForAll(transforms =>
        //{
        //    transforms.localScale = GameManager.instance.playerController.transform.localScale;
        //    Debug.Log("Transform : " + transforms.name);
        //});

        StartCoroutine(WaitForSecondsCoroutine(particleSystem.main.duration,
            action: () =>
              {
                  gameObject.SetActive(false);
              }));
    }

    void OnDisable()
    {
        GameManager.instance.objectPoolManager.bloodSprayEffectObjectPoolList.ObjectPoolReturn(gameObject);
    }
}
