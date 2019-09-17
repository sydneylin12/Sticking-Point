using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class PauseController : MonoBehaviour
{
    /// <summary>
    /// The scene transition controller.
    /// </summary>
    private SceneTransitionController sceneTransitionController;

    /// <summary>
    /// The GameData DontDestroyOnLoad object.
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
    }

    /// <summary>
    /// Toggle a pause or unpause for the current scene (SBD).
    /// </summary>
    public void Pause()
    {
        Scene scene = SceneManager.GetActiveScene();
        gameData.CurrentScene = scene.name;
        sceneTransitionController.FadeToLevel("Pause Menu");
    }

    /// <summary>
    /// Resumes the current scene based on GameData's current scene string.
    /// </summary>
    public void Resume()
    {
        if(gameData.CurrentScene == null)
        {
            sceneTransitionController.FadeToLevel("Main Menu");
            return;
        }
        else if(gameData.CurrentAttempt == 3)
        {
            sceneTransitionController.FadeToLevel("Bench");
            return;
        }
        else if (gameData.CurrentAttempt == 6)
        {
            sceneTransitionController.FadeToLevel("Deadlift");
            return;
        }
        else if (gameData.CurrentAttempt == 9)
        {
            sceneTransitionController.FadeToLevel("Main Menu");
            return;
        }
        sceneTransitionController.FadeToLevel(gameData.CurrentScene);
    }

    /// <summary>
    /// Returns to the menu and resets the lift tracker for GameData.
    /// </summary>
    public void OnMenuClicked()
    {
        gameData.ResetLiftTracker();
        sceneTransitionController.FadeToLevel("Main Menu");
    }
}
