using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{

    public PlayerControl playerControl;

    

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    public void Buy(int item)
    {
        float itemCost = 0;
        //sword costs 3 coins
        if (item == 1 && playerControl.coins >= 3)
        {
            itemCost = 3;
        }
        //Gun costs 7 coins
        if (item == 2 && playerControl.coins >= 7)
        {
            itemCost = 7;
        }
        //Shield costs 15 coins
        if (item == 2 && playerControl.coins >= 15)
        {
            itemCost = 15;
        }
        playerControl.item = item;
        playerControl.coins -= itemCost;
        playerControl.ItemManager();
    }

    public void Quit()
    {
        playerControl.shopping = false;
        playerControl.ShopCooldown();
        this.gameObject.SetActive(false);
    }


}
