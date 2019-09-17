using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class DeadliftController : Timer
{
    /// <summary>
    ///The current phase of the squat exercise.
    /// </summary>
    private CurrentPhaseDeadlift currentPhase;

    /// <summary>
    ///Called before the first frame.
    /// </summary>
    void Start()
    {
        InitializeTimer();
        sc = new SwipeController();
        ResetExercise();
        currentPhase = CurrentPhaseDeadlift.DeadliftNone;

        // DEBUG USE ONLY
        if(gameData.CurrentAttempt == 0)
        {
            gameData.OverrideAttemptsDebug(ExerciseType.Deadlift);
            gameplayController.OverrideLoad();
        }
    }

    /// <summary>
    ///Called every frame.
    /// </summary>
    void Update()
    {
        // DEBUG USE ONLY
        Debug.Log(gameData.CurrentAttempt);
        if (Input.GetKeyDown(KeyCode.R) && !gameData.CanContinue())
        {
            sceneTransitionController.FadeToLevel("Game Over");
        }
        else if (Input.GetKeyDown(KeyCode.R) && gameData.CanContinue())
        {
            OverrideWin();
        }

        // Determine what phase we are on
        switch (currentPhase)
        {
            case CurrentPhaseDeadlift.DeadliftNone:
                // Do nothing here
                break;
            case CurrentPhaseDeadlift.DeadliftEnded:
                // Do nothing here
                break;
            case CurrentPhaseDeadlift.DeadliftStart:
                ConcentricPhase();
                break;
            case CurrentPhaseDeadlift.DeadliftSticking:
                StickingPhase();
                break;
            case CurrentPhaseDeadlift.DeadliftDown:
                EccentricPhase();
                break;
            default:
                Debug.Log("Invalid current state for deadlift.");
                break;
        }
    }

    /// <summary>
    ///Starts the deadlift exercise start phase.
    /// </summary>
    public void StartExercise()
    {
        timeRemaining = timeMax;
        sliderFill.color = Color.green;
        canStartLift = false;
        currentPhase = CurrentPhaseDeadlift.DeadliftStart;
    }

    /// <summary>
    ///Starts the deadlift sticking phase.
    /// </summary>
    public void StartSticking()
    {
        timeRemaining = 1f;
        currentPhase = CurrentPhaseDeadlift.DeadliftSticking;
    }

    /// <summary>
    ///Starts the deadlift down phase.
    /// </summary>
    public void StartEccentric()
    {
        playerAnimationController.TriggerAnimation("deadlift_lockout");
        speechController.ActivateSpeechBubble("Down!");
        timeRemaining = timeMax;
        currentPhase = CurrentPhaseDeadlift.DeadliftDown;
    }

    /// <summary>
    ///The concentric phase.
    /// </summary>
    public void ConcentricPhase()
    {
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
            // Failed to break the floor
            playerAnimationController.TriggerAnimation("deadlift_failed_idle");
            FailedAttempt();
        }

        if (sc.Swipe() == SwipeController.Directions.UP_SWIPE || sc.Swipe() == SwipeController.Directions.UP_SWIPE)
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
                FailedAttempt();
                return;
            }
            playerAnimationController.TriggerAnimation("deadlift_breakfloor");
            Invoke("StartSticking", 0.5f);
        }
    }

    /// <summary>
    ///The deadlift sticking phase.
    /// </summary>
    public void StickingPhase()
    {
        slider.value = CalculateSliderValue();
        UpdateTimeSticking();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
            // Failed to break the floor
            playerAnimationController.TriggerAnimation("deadlift_failed_idle");
            FailedAttempt();
        }
        else if(timeRemaining == timeMax)
        {
            Invoke("StartEccentric", 0.5f);
        }

        // Detect input rapid tap/click
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAnimationController.TriggerAnimation("deadlift_sticking");
        }
    }

    /// <summary>
    ///The deadlift down phase.
    /// </summary>
    public void EccentricPhase()
    {
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
            // Failed to grind through the rep
            playerAnimationController.TriggerAnimation("deadlift_failed");
            FailedAttempt();
        }

        if (sc.Swipe() == SwipeController.Directions.DOWN_SWIPE)
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
                FailedAttempt();
                return;
            }
            currentPhase = CurrentPhaseDeadlift.DeadliftEnded;
            playerAnimationController.TriggerAnimation("deadlift_down");
            SuccessfulAttempt();
        }
    }
}
