using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuController : MonoBehaviour
{
    /// <summary>
    /// The shaking bar GameObject.
    /// </summary>
    private GameObject shakingBar;

    /// <summary>
    /// The shaking bar's position.
    /// </summary>
    private Vector2 barPos;

    /// <summary>
    /// The change in Y position for the bar.
    /// </summary>
    private float yPos = 0.05f;

    /// <summary>
    /// The scene transition controller.
    /// </summary>
    private SceneTransitionController sceneTransitionController;

    /// <summary>
    /// The game's data .
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Sets the play button to say start and activates it.
    /// </summary>
    void Start()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        gameData.ResetLiftTracker();
        shakingBar = GameObject.Find("MenuBarbell");
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
    }

    /// <summary>
    /// Shakes the bar every frame.
    /// </summary>
    void Update()
    {
        ShakeBar();
    }

    /// <summary>
    /// Starts the squat scene when PLAY is clicked. 
    /// </summary>
    public void OnPlayClicked()
    {
        gameData.ResetLiftTracker();
        SaveSystem.SaveGame();
        sceneTransitionController.FadeToLevel("Squat");
    }

    /// <summary>
    /// Starts the help menu scene when HELP is clicked. 
    /// </summary>
    public void OnHelpClicked()
    {
        sceneTransitionController.FadeToLevel("Help");
    }

    /// <summary>
    /// Starts the settings menu. 
    /// </summary>
    public void OnSettingsClicked()
    {
        sceneTransitionController.FadeToLevel("Settings");
    }

    /// <summary>
    /// Contantly shakes the bar on the menu screen.
    /// </summary>
    private void ShakeBar()
    {
        barPos = shakingBar.transform.position;
        barPos.y += yPos;
        shakingBar.transform.position = barPos;
        yPos *= -1;
    }
}
