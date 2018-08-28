using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributesController : CustomMonoBehaviour
{
    [Space]
    public PlayerAttributesInfoAsset attributesInfoAsset;

    public float speed;/* { get; set; }*/
    public float health;/* { get; set; }*/
    public float regularDamage { get; set; }
    public float criticalChance { get; set; }
    public float armorPiece { get; set; }
    public float armor { get; set; }
    public DamageElement damageElement { get; set; }

    public virtual float TotalDamage
    {
        get
        {
            return attributesInfoAsset.regularDamage +
              attributesInfoAsset.criticalChance > Random.Range(0, 1) ? (attributesInfoAsset.regularDamage * 2) : 0 +
              attributesInfoAsset.armorPiece;
        }
    }

   void Awake()
    {
        health = attributesInfoAsset.health;
        regularDamage = attributesInfoAsset.regularDamage;
        criticalChance = attributesInfoAsset.criticalChance;
        armorPiece = attributesInfoAsset.armorPiece;
        armor = attributesInfoAsset.armor;
        damageElement = attributesInfoAsset.damageElement;
    }

    void Start()
    {
        SetGameObjectLocalScaleToGamePlayMode();
        speed = SetAttributeToGamePlayMode(attributesInfoAsset.moveSpeed);
    }

    public void ResetAllAttributes()
    {
        health = attributesInfoAsset.health;
        regularDamage = attributesInfoAsset.regularDamage;
        criticalChance = attributesInfoAsset.criticalChance;
        armorPiece = attributesInfoAsset.armorPiece;
        armor = attributesInfoAsset.armor;
        damageElement = attributesInfoAsset.damageElement;

        SetGameObjectLocalScaleToGamePlayMode();
        speed = SetAttributeToGamePlayMode(attributesInfoAsset.moveSpeed);
    }
}