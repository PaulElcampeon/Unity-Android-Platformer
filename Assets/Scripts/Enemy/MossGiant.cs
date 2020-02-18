using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MossGiant : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Initialize()
    {
        base.Initialize();
        Health = base.health;
    }

    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
