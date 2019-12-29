using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutantEnemyContactController : CustomMonoBehaviour
{
    [Space]
    [SerializeField]
    LayerMask contactLayerMask;

    public EnemyController MutantEnemyController { get; private set; }

    void Awake()
    {
        MutantEnemyController = GetComponentInParent<EnemyController>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagHelper.PLAYER_CONTACT))
        {
            other.GetComponentInParent<PlayerController>().ApplyDamage(MutantEnemyController.AttributesController.TotalDamage, MutantEnemyController.AttributesController.damageElement);
        }
    }
}
