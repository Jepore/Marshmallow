using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManger : MonoBehaviour
{
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

    private void TextUpdate()
    {
        pointsText.text = "Points: " + playerControl.coins;
        hpText.text = "Lives: " + playerControl.hp;

        hpWidth = 600 * (playerControl.hp / 50);
        hpBar.rectTransform.sizeDelta = new Vector2(hpWidth, hpHeight);

    }
}
