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
    private float hpWidth = 600;
    private float hpHeight = 50;
    public TMP_Text pointsText;
    public TMP_Text hpText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TextUpdate();
    }

    /// <summary>
    /// Updates Text and health bar size
    /// </summary>
    private void TextUpdate()
    {
        pointsText.text = "Points: " + playerControl.coins;
        hpText.text = "Lives: " + playerControl.hp;

        hpWidth = 600 * (playerControl.hp / 50);
        hpBar.rectTransform.sizeDelta = new Vector2(hpWidth, hpHeight);
    }
}
