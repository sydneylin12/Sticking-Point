using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HelpMenuController : MonoBehaviour
{
    /// <summary>
    /// The scene transition controller.
    /// </summary>
    private SceneTransitionController sceneTransitionController;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
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
