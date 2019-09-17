using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
    /// <summary>
    /// The scene transition controller.
    /// </summary>
    private SceneTransitionController sceneTransitionController;

    /// <summary>
    /// The GameData DontDestroyOnLoad object.
    /// </summary>
    private GameData gameData;

    private Text gameOverText;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        gameOverText = GameObject.Find("Game Over Text").GetComponent<Text>();
        InitializeGameOverScreen();
    }

    private void InitializeGameOverScreen()
    {
        int s = 0;
        int b = 0;
        int d = 0;
        for(int i = 0; i < 3; i++)
        {
            if ((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.Success)
            {
                s++;
            }
        }
        for (int i = 3; i < 6; i++)
        {
            if ((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.Success)
            {
                b++;
            }
        }
        for (int i = 6; i < 9; i++)
        {
            if ((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.Success)
            {
                d++;
            }
        }
        gameOverText.text = string.Format("-Meet Recap-\n" +
        "Squat: {0} / 3\n" +
        "Bench: {1} / 3\n" +
        "Deadlift: {2} / 3", s, b, d);
    }


    /// <summary>
    /// Returns to the main menu. 
    /// </summary>
    public void OnMenuClicked()
    {
        SaveSystem.SaveGame();
        sceneTransitionController.FadeToLevel("Main Menu");
    }
}
