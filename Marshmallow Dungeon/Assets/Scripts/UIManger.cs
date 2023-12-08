using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Liebert, Jasper
//12/06/2023
//Updates Text and Health Bar at the top of the screen, also manages scene switching

public class UIManger : MonoBehaviour
{
    //Variables
    public PlayerControl playerControl;
    public Image hpBar;
    public Image itemBar;
    private float itemWidth = 600;
    private float itemHeight = 30;
    private float hpWidth = 600;
    private float hpHeight = 50;
    public TMP_Text pointsText;
    public TMP_Text hpText;
    public TMP_Text timer;
    public double time;
    public float itemTimer;
    public float itemTemp;

    // Start is called before the first frame update
    void Start()
    {
        itemBar.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
        ItemBarUpdate();
        time += 1 * Time.deltaTime;

    }

    /// <summary>
    /// Updates Text and health bar size
    /// </summary>
    private void TextUpdate()
    {
        pointsText.text = "Marshmallows: " + playerControl.coins;
        hpText.text = "Lives: " + playerControl.hp;
        timer.text = "" + ((float)((int)(time * 100))/100) ;
        //adjusts health bar width with player hp
        hpWidth = 600 * (playerControl.hp / 50);
        hpBar.rectTransform.sizeDelta = new Vector2(hpWidth, hpHeight);
    }

    /// <summary>
    /// if item bar should be showing, set it to active and have it disappear after timer
    /// </summary>
    public void ItemBarUpdate()
    {
        if (itemTemp > 0)
        {
            itemBar.gameObject.SetActive(true);
            itemTemp -= 1 * Time.deltaTime;
        }
        else
        {
            itemBar.gameObject.SetActive(false);
            itemTemp = 0;
        }

        itemWidth = 600 * (itemTemp / itemTimer);
        itemBar.rectTransform.sizeDelta = new Vector2(itemWidth, itemHeight);

    }
}
