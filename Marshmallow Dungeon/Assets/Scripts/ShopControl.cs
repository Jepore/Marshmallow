using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopControl : MonoBehaviour
{

    //variables
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

    /// <summary>
    /// Activates Shop
    /// </summary>
    public void Activate()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// changes player item and takes coins
    /// </summary>
    /// <param name="item"> item index </param>
    public void Buy(int item)
    {
        float itemCost = 0;
        bool buyable = false;
        //sword costs 3 coins
        if (item == 1 && PlayerControl.coins >= 3)
        {
            buyable = true;
            itemCost = 3;
        }
        //Gun costs 7 coins
        if (item == 2 && PlayerControl.coins >= 7)
        {
            buyable = true;
            itemCost = 7;
        }
        //Shield costs 15 coins
        if (item == 3 && PlayerControl.coins >= 15)
        {
            buyable = true;
            itemCost = 15;
        }
        if (buyable)
        {
            playerControl.item = item;
            PlayerControl.coins -= itemCost;
            playerControl.ItemManager();
        }
    }

    /// <summary>
    /// quit shop
    /// </summary>
    public void Quit()
    {
        playerControl.shopping = false;
        playerControl.ShopCooldown();
        this.gameObject.SetActive(false);
    }


}
