using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    protected Slider slider;
    protected Image sliderFill;

    protected float timeRemaining;
    protected float timeMax;
    protected float bound;

    protected bool canStartLift;
    protected bool isOver;

    protected GameplayController gameplayController;
    protected SpeechController speechController;
    protected SceneTransitionController sceneTransitionController;
    protected PlayerAnimationController playerAnimationController;
    protected StartButtonController startButtonController;
    protected CountdownController countdownController;
    protected SwipeController sc;

    /// <summary>
    /// The game's data .
    /// </summary>
    protected GameData gameData;

    /// <summary>
    /// Initialize all the GameObjects and resources for the Timer superclass.
    /// </summary>
    protected void InitializeTimer()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        slider = GameObject.Find("Slider").GetComponent<Slider>();
        sliderFill = GameObject.Find("Slider Fill").GetComponent<Image>();
        gameplayController = GameObject.Find("Gameplay Controller").GetComponent<GameplayController>();
        speechController = GameObject.Find("Speech Controller").GetComponent<SpeechController>();
        startButtonController = GameObject.Find("Start Button Controller").GetComponent<StartButtonController>();
        sceneTransitionController = GameObject.Find("Transition").GetComponent<SceneTransitionController>();
        playerAnimationController = GameObject.Find("Player Animation Controller").GetComponent<PlayerAnimationController>();
        countdownController = GameObject.Find("Countdown Controller").GetComponent<CountdownController>();

        // Set the total time and bounded time
        timeMax = gameData.GetTimeTotal();
        bound = gameData.GetBound() * timeMax;
    }


    /// <summary>
    /// Resets all booleans, slider, lights, speech bubble. Changes time to 0. 
    /// </summary>
    protected void ResetExercise()
    {
        // Reset all booleans
        canStartLift = true; // Lift can be started
        isOver = false; // Scene has not ended

        // Resetting UI elements
        sliderFill.color = Color.green;
        timeRemaining = 0;
        slider.value = CalculateSliderValue();
    }

    /// <summary>
    /// Starts or continues/resets the game when the button is clicked using the current exercise type.
    /// </summary>
    public void OnStartContinueClicked()
    {
        if (canStartLift && !gameData.CanContinue())
        {
            // Must check if out of bounds and cannot continue attempts
            sceneTransitionController.FadeToLevel("Game Over");
        }
        else if (canStartLift && gameData.CanContinue())
        {
            // If the lift can be started
            startButtonController.DeactivateButton();
            // Abstracted to start every exercise regardless of the type
            Invoke("StartExercise", 0.5f);
        }

        // Must check edge cases to switch scenes
        else if (isOver && gameData.CurrentExerciseType == ExerciseType.Squat)
        {
            sceneTransitionController.FadeToLevel("Squat");
        }
        else if (isOver && gameData.CurrentExerciseType == ExerciseType.Bench)
        {
            sceneTransitionController.FadeToLevel("Bench");
        }
        else if (isOver && gameData.CurrentExerciseType == ExerciseType.Deadlift)
        {
            sceneTransitionController.FadeToLevel("Deadlift");
        }
        else if (isOver && gameData.CurrentAttempt >= gameData.GetMaxAllowedAttempts())
        {
            sceneTransitionController.FadeToLevel("Main Menu");
        }
    }

    /// <summary>
    /// Returns a float indicating the slider fill amount.
    /// </summary>
    /// <returns>The slider value.</returns>
    protected float CalculateSliderValue()
    {
        return (timeRemaining / timeMax);
    }

    /// <summary>
    /// Subtracts time from the slider until time reaches 0.
    /// </summary>
    protected void SubtractTime()
    {
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        else if (timeRemaining > 0)
        {
            // Subtract the change in time
            timeRemaining -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Subtracts time but allows the user to press space for the sticking point.
    /// </summary>
    protected void UpdateTimeSticking()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            timeRemaining += 0.2f;
        }
        //must check for fail condition ahead of time
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
        }
        else if (timeRemaining >= timeMax)
        {
            // Pushed through sticking point
            timeRemaining = timeMax; 
        }
        else if (timeRemaining < timeMax)
        {
            timeRemaining -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks if the timer is in bounds and changes slider color.
    /// </summary>
    /// <returns>True if in bounds and false if not.</returns>
    protected bool CheckBound()
    {
        if(timeRemaining <= bound)
        {
            sliderFill.color = Color.green;
            return true;
        }
        else
        {
            sliderFill.color = Color.red;
            return false;
        }
    }

    /// <summary>
    /// Successful attempt function: handles booleans, gameplay, UI  
    /// </summary>
    protected void SuccessfulAttempt()
    {
        // Stop any phase and set isOver to true
        isOver = true;

        // Handle gameplay and UI
        gameplayController.LogSuccessfulAttempt();
        speechController.DeactivateSpeechBubble();
        startButtonController.SetButtonContinue();
        countdownController.StopCountdown();
    }

    /// <summary>
    /// Failed attempt funciton: handles booleans, gameplay, UI 
    /// </summary>
    protected void FailedAttempt()
    {
        // Stop any phase and set isOver to true
        isOver = true;

        // Ends the game and activates the start button and stops the countdown  
        gameplayController.LogFailedAttempt();
        speechController.DeactivateSpeechBubble();
        startButtonController.SetButtonContinue();
        countdownController.StopCountdown();
    }

    /// <summary>
    /// Used for debugging purposes (click "R" to win). Automatically advances to the next scene.
    /// </summary>
    protected void OverrideWin()
    {
        SuccessfulAttempt();
        if (gameData.CurrentExerciseType == ExerciseType.Squat)
        {
            sceneTransitionController.FadeToLevel("Squat");
        }
        else if (gameData.CurrentExerciseType == ExerciseType.Bench)
        {
            sceneTransitionController.FadeToLevel("Bench");
        }
        else if (gameData.CurrentExerciseType == ExerciseType.Deadlift)
        {
            sceneTransitionController.FadeToLevel("Deadlift");
        }
    }

    /// <summary>
    /// Detects if the wrong swipe was made.
    /// </summary>
    /// <returns>True if the wrong swipe was made.</returns>
    public bool DetectWrongSwipe(SwipeController.Directions s)
    {
        if(sc.Swipe() != SwipeController.Directions.NO_DIRECITON && sc.Swipe() != s)
        {
            Debug.Log("Wrong swipe made!");
            return true;
        }
        return false;
    }

    /// <summary>
    /// Checks if the slider timer has ended.
    /// </summary>
    /// <returns>True if the timer has ended and false if it has not ended.</returns>
    public bool CheckTimerEnded()
    {
        if (timeRemaining <= 0)
        {
            return false;
        }
        return true;
    }
}
