using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaladinController : ThidPersonExampleController, IDamageable
{
    [Space]
    public bool AR = false;

    [Space]
    public float attackDamage;
    public float health;

    [HideInInspector]
    public Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        PaladinController paladin = GetComponent<PaladinController>();

        if (AR)
        {
            gameObject.transform.localScale = new Vector3(.1f, .1f, .1f);
            paladin.MovementSpeed *= gameObject.transform.localScale.x;
        }
        else
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            paladin.MovementSpeed = 10;
        }
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
