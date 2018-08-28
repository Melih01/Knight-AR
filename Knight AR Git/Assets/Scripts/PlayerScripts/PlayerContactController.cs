using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContactController : CustomMonoBehaviour
{
    public PlayerController PlayerController { get; private set; }

    void Awake()
    {
        PlayerController = GetComponentInParent<PlayerController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHelper.ENEMY_CONTACT))
        {
            other.GetComponentInParent<EnemyController>().ApplyDamage(PlayerController.AttributesController.TotalDamage, PlayerController.AttributesController.damageElement);
        }
    }
}
