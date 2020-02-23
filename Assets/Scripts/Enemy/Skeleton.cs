using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        Health = base.health;
    }

    public void Damage()
    {
        if (isDead) return;

        health--;
        animator.SetTrigger("Hit");
        isHit = true;
        animator.SetBool("InCombat", true);
        if (health <= 0)
        {
            animator.SetTrigger("Death");
            isDead = true;
            GameObject diamond = (GameObject)Instantiate(diamondPrefab, transform.position, Quaternion.identity);
            diamond.GetComponent<Diamond>().numberOfDiamonds = base.gems;
        }
    }
}
