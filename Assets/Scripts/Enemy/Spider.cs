﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    public GameObject acideEffectPrefab;

    public int Health { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        Health = base.health;
    }

    public void Damage()
    {
        health--;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            //Destroy(this.gameObject);
        }
    }

    public void Attack()
    {
        Instantiate(acideEffectPrefab, transform.position, Quaternion.identity);
    }
}
