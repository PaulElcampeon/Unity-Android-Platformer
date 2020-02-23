using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    public int numberOfDiamonds;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();

            if (player != null) {
                player.AddGems(numberOfDiamonds);
                Destroy(this.gameObject);
            }
        }
    }
}
