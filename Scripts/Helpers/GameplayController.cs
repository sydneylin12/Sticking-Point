using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameplayController : MonoBehaviour
{
    /// <summary>
    /// The controller for the 3 lights.
    /// </summary>
    private LightsController lightsController;

    /// <summary>
    /// The controller for lift tracker and its 9 elements.
    /// </summary>
    private LiftTrackerController liftTrackerController;

    /// <summary>
    /// The info text controller.
    /// </summary>
    private InfoTextController infoTextController;

    /// <summary>
    /// The experience controller.
    /// </summary>
    private ExperienceController experienceController;

    /// <summary>
    /// The player's game data.
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        InitializeGameplayController();
    }

    /// <summary>
    /// Initialize all gameObjects and controllers for the gameplay controller. 
    /// </summary>
    private void InitializeGameplayController()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        liftTrackerController = GameObject.Find("Lift Tracker Controller").GetComponent<LiftTrackerController>();
        lightsController = GameObject.Find("Lights Controller").GetComponent<LightsController>();
        infoTextController = GameObject.Find("Info Text Controller").GetComponent<InfoTextController>();
        experienceController = GameObject.Find("Experience Controller").GetComponent<ExperienceController>();
    }

    /// <summary>
    /// Force the loading from game data.
    /// </summary>
    public void OverrideLoad()
    {
        liftTrackerController.LoadFromGameData();
    }

    /// <summary>
    /// Success: handles UI, updates XP
    /// </summary>
    public void LogSuccessfulAttempt()
    {
        StartCoroutine(lightsController.SetLights(AttemptState.Success));
        liftTrackerController.UpdateState(AttemptState.Success);
        infoTextController.UpdateLevelTextOnly();
        gameData.LogCurrentAttempt(AttemptState.Success);
        gameData.GainXp(100);
        StartCoroutine(experienceController.GrowXpBar());
        infoTextController.UpdateLevelTextOnly();
    }

    /// <summary>
    /// Failure: handles UI, updates XP
    /// </summary>
    public void LogFailedAttempt()
    {
        StartCoroutine(lightsController.SetLights(AttemptState.Fail));
        liftTrackerController.UpdateState(AttemptState.Fail);
        gameData.LogCurrentAttempt(AttemptState.Fail);
        gameData.GainXp(50);
        StartCoroutine(experienceController.GrowXpBar());
        infoTextController.UpdateLevelTextOnly();
    }
}
