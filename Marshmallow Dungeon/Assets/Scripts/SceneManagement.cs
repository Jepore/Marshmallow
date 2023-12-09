using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Liebert, Jasper
// 09/28/2023
// Manages scenes and quits the game

public class SceneManagement : MonoBehaviour
{

    public TMP_Text timerText;
    public TMP_Text marshText;
    public TMP_Text score;

    //called once per frame
    void Start()
    {
        timerText.text = "  " + (int)(UIManger.time*100)/100 + " Seconds";
        marshText.text = "  " + PlayerControl.coins + " Marshmallows";
        score.text = "Score : " + ((int)(UIManger.time * 100) / 100 - PlayerControl.coins);


    }

    /// <summary>
    /// switches scene based off of build index
    /// </summary>
    /// <param name="buildIndex"> index assigned to the scenes </param>
    public void SceneSwitch(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }

    /// <summary>
    /// quits the game
    /// </summary>
    public void QuitGame()
    {
        Debug.Log("Game Quit");
        Application.Quit();
    }

    /// <summary>
    /// Switches to Game Scene and resets timer
    /// </summary>
    public void StartGame()
    {
        SceneSwitch(1);
        PlayerControl.coins = 0;
        UIManger.time = 0;
        UIManger.timing = true;
    }

}
