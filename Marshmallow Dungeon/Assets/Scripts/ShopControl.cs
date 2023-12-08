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
        playerControl.item = item;
        playerControl.ItemManager();
    }

    public void Quit()
    {
        playerControl.shopping = false;
        playerControl.ShopCooldown();
        this.gameObject.SetActive(false);
    }


}