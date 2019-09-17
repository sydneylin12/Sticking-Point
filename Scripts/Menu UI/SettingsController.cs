using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{
    /// <summary>
    /// The dropdown object for difficulty.
    /// </summary>
    private Dropdown difficulty;

    /// <summary>
    /// The scene transition controller.
    /// </summary>
    private SceneTransitionController sceneTransitionController;

    /// <summary>
    /// The gameobject of the popup (new game).
    /// </summary>
    private GameObject popup;

    /// <summary>
    /// The GameData DontDestroyOnLoad object.
    /// </summary>
    private GameData data;

    void Start()
    {
        difficulty = GameObject.Find("DifficultyDropdown").GetComponent<Dropdown>();
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
        popup = GameObject.Find("Popup");
        data = GameObject.Find("Game Data").GetComponent<GameData>();
        popup.SetActive(false);
        InitializeDropdowns();
    }

    /// <summary>
    /// Update dropdown boxes based on settings set prior.
    /// </summary>
    public void InitializeDropdowns()
    {
        //for difficulty dropdown
        if (data.GetBound() == 0.5f)
        {
            difficulty.value = 0;
        }
        else if (data.GetBound() == 0.4f)
        {
            difficulty.value = 1;
        }
        else if (data.GetBound() == 0.3f)
        {
            difficulty.value = 2;
        }
        else if (data.GetBound() == 0.2f)
        {
            difficulty.value = 3;
        }
        else if (data.GetBound() == 0.1f)
        {
            difficulty.value = 4;
        }
    }

    /// <summary>
    /// Update difficulty in GameGlobals. Difficulty is the BOUND not the time.
    /// </summary>
    public void OnDifficultyUpdated()
    {
        data.UpdateDifficulty(difficulty.options[difficulty.value].text);
    }

    /// <summary>
    /// Toggles the game audio.
    /// </summary>
    public void OnSoundUpdated()
    {
        //deactivate audio here
        //to be implemented
    }

    /// <summary>
    /// Activates the confirmation popup.
    /// </summary>
    public void OnNewGameClicked()
    {
        popup.SetActive(true);
    }

    /// <summary>
    /// Deactivates the confirmation popup.
    /// </summary>
    public void OnNewGameBackout()
    {
        popup.SetActive(false);
    }

    /// <summary>
    /// Starts a new game on button clicked.
    /// </summary>
    public void OnNewGameConfirmed()
    {
        data.ResetPlayer();
        popup.SetActive(false);
    }

    /// <summary>
    /// Return back to the previous scene (Main Menu or Pause Menu).
    /// </summary>
    public void OnMenuClicked()
    {
        SaveSystem.SaveGame();
        sceneTransitionController.FadeToLevel("Main Menu");
    }

}
