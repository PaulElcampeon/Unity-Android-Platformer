﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    private bool canHit;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);

        IDamageable hit = other.GetComponent<IDamageable>();

        if (hit != null)
        {
            if (canHit)
            {
                hit.Damage();
                canHit = false;
                StartCoroutine(ResetCanHitCo());
            }
        }
    }

    public IEnumerator ResetCanHitCo()
    {
        yield return new WaitForSeconds(0.5f);
        canHit = true;
    }
}