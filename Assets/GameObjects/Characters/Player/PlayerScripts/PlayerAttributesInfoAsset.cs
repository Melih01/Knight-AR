using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamageElement
{
    Physical = 0,
    Fire = 10,
    Ice = 20,
    Earth = 30,
    Air = 40
};

[CreateAssetMenu(fileName = "PlayerAttributesInfoAsset", menuName = "Knight AR/Players/Player Attributes Info Asset")]
public class PlayerAttributesInfoAsset : ScriptableObject
{
    [Space]
    public float health = 10f;
    [Space]
    public float moveSpeed = 3f;
    public float attackSpeed = 3f;
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
    public DamageElement damageElement;
}