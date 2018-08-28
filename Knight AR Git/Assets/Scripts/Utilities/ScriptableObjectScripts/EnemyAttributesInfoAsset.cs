using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyAttributesInfoAsset", menuName = "Knight AR/Enemies/Enemy Attributes Info Asset")]
public class EnemyAttributesInfoAsset : ScriptableObject
{
    [Space]
    public float health = 10f;
    [Space]
    public float moveSpeed = 3f;
    [Space]
    public float regularDamage = 3f;
    [Space]
    [Range(0,1)]
    public float criticalChance = 0.3f;
    [Space]
    public float armorPiece = 3f;
    [Space]
    public float armor = 3f;
    [Space]
    public float stoppingDistance = 2f;
    [Space]
    public DamageElement damageElement;
}
