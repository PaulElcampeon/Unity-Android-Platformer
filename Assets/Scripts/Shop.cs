using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject shopPanel;
    public int currentItemSelected;
    public int currentItemCost;
    private Player player;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.GetComponent<Player>();

            if (player != null)
            {
                player.canAttack = false;
                UIManager.Instance.OpenShop(player.diamondCount);
            }
            shopPanel.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        shopPanel.SetActive(false);
        player.canAttack = true;
    }

    public void SelectItem(int itemIndex)
    {
        currentItemSelected = itemIndex;

        switch (itemIndex)
        {
            case 0:
                UIManager.Instance.UpdateShopSelection(83f);
                currentItemCost = 200;
                break;
            case 1:
                UIManager.Instance.UpdateShopSelection(-22f);
                currentItemCost = 400;
                break;
            case 2:
                UIManager.Instance.UpdateShopSelection(-120f);
                currentItemCost = 100;
                break;
        }
    }

    public void Buy()
    {
        if (player.diamondCount >= currentItemCost)
        {
            if(currentItemSelected == 2)
            {
                GameManager.Instance.HasKeyToCastle = true;
            }
            player.diamondCount -= currentItemCost;
        } else
        {
            Debug.Log("You do not have enough gems. Closing shop");
        }

        shopPanel.SetActive(false);
    }
}
