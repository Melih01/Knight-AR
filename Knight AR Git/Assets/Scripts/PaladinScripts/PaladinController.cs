using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : ThidPersonExampleController, IDamageable
{
    [HideInInspector]
    public Animator anim;
    public float attackDamage;
    public float health;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public override void Update()
    {
        base.Update();
        anim.SetFloat("Speed", inputMoveVector);
    }

    void IDamageable.ApplyDamage(float damage)
    {
        if (health > 0)
        {
            health -= damage;
        }

        if (health <= 0)
        {
            anim.SetTrigger("Die");
            GetComponent<CharacterController>().detectCollisions = false;
            GetComponent<PaladinController>().enabled = false;
        }
        else
        {
            anim.SetTrigger("Hit");
        }
    }
}
