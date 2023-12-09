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

    public TMP_Text marshmallows;
    public PlayerControl playerControl;
    public UIManger uiManager;

    //called once per frame
    void Start()
    {
        marshmallows.text = "Score: " + playerControl.coins;
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

    public void StartGame()
    {
        SceneSwitch(1);
        uiManager.time = 0;
    }

}
