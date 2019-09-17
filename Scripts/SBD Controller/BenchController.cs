using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BenchController : Timer
{
    /// <summary>
    ///The current phase of the bench exercise.
    /// </summary>
    private CurrentPhaseBench currentPhase;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        InitializeTimer();
        sc = new SwipeController();
        ResetExercise();
        currentPhase = CurrentPhaseBench.BenchNone;
        // DEBUG USE ONLY
        if (gameData.CurrentAttempt == 0)
        {
            gameData.OverrideAttemptsDebug(ExerciseType.Bench);
            gameplayController.OverrideLoad();
        }
    }

    /// <summary>
    /// Called every frame.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            OverrideWin();
        }

        // Determine what phase we are on
        switch (currentPhase)
        {
            case CurrentPhaseBench.BenchNone:
                // Do nothing here
                break;
            case CurrentPhaseBench.BenchEnded:
                // Do nothing here
                break;
            case CurrentPhaseBench.BenchStart:
                EccentricPhase();
                break;
            case CurrentPhaseBench.BenchPress:
                PressPhase();
                break;
            case CurrentPhaseBench.BenchSticking:
                StickingPhase();
                break;
            case CurrentPhaseBench.BenchRack:
                RackPhase();
                break;
            default:
                Debug.Log("Invalid current state for bench.");
                break;
        }
    }

    /// <summary>
    /// Starts the eccentric bench phase.
    /// </summary>
    public void StartExercise()
    {
        // Start counting down on the timer
        countdownController.BeginCountdown();
        // Start command
        speechController.ActivateSpeechBubble("Start!");
        // Set timer to max time
        timeRemaining = timeMax;
        canStartLift = false;
        currentPhase = CurrentPhaseBench.BenchStart;
    }

    /// <summary>
    /// Starts bench "press" command phase.
    /// </summary>
    public void StartPress()
    {
        // Press command
        speechController.ActivateSpeechBubble("Press!");
        timeRemaining = timeMax;
        currentPhase = CurrentPhaseBench.BenchPress;
    }

    /// <summary>
    /// Starts bench sticking point phase.
    /// </summary>
    public void StartSticking()
    {
        speechController.DeactivateSpeechBubble();
        timeRemaining = 0.5f;
        currentPhase = CurrentPhaseBench.BenchSticking;
    }

    /// <summary>
    /// Starts bench "rack" command phase.
    /// </summary>
    public void StartRack()
    {
        speechController.ActivateSpeechBubble("Rack!");
        timeRemaining = timeMax;
        currentPhase = CurrentPhaseBench.BenchRack;
    }

    /// <summary>
    /// Decrements timer and ends on down swipe.
    /// </summary>
    public void EccentricPhase()
    {
        // Set value of the time slider
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();

        // Check if the timer has ran out
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseBench.BenchEnded;
            playerAnimationController.TriggerAnimation("bench_failed");
            FailedAttempt();
        }

        // Detect a swipe
        if (sc.Swipe() == SwipeController.Directions.DOWN_SWIPE)
        {
            // If it is not in bounds (red bar)
            if (!inBounds)
            {
                currentPhase = CurrentPhaseBench.BenchEnded;
                playerAnimationController.TriggerAnimation("bench_failed");
                FailedAttempt();
                return;
            }
            playerAnimationController.TriggerAnimation("bench_down");
            Invoke("StartPress", 0.5f);
        }
    }

    /// <summary>
    /// Decrements timer and ends on up swipe.
    /// </summary>
    public void PressPhase()
    {
        slider.value = CalculateSliderValue(); //decrement the slider
        SubtractTime();
        bool inBounds = CheckBound();
        // Check for time ended (failed)
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseBench.BenchEnded;
            playerAnimationController.TriggerAnimation("bench_failed");
            FailedAttempt();
        }
        // Check for input touch/click
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseBench.BenchEnded;
                playerAnimationController.TriggerAnimation("bench_failed_idle");
                FailedAttempt();
                return;
            }
            playerAnimationController.TriggerAnimation("bench_up");
            Invoke("StartSticking", 0.5f);
        }
    }

    /// <summary>
    /// Decreases time and detects rapid taps.
    /// </summary>
    public void StickingPhase()
    {
        slider.value = CalculateSliderValue();
        UpdateTimeSticking();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseBench.BenchEnded;
            playerAnimationController.TriggerAnimation("bench_failed");
            FailedAttempt();
        }
        else if (timeRemaining == timeMax)
        {
            Invoke("StartRack", 1f);
        }

        // Check for input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerAnimationController.TriggerAnimation("bench_sticking");
        }
    }

    /// <summary>
    /// Decrements timer and ends on right swipe.
    /// </summary>
    public void RackPhase()
    {
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseBench.BenchEnded;
            playerAnimationController.TriggerAnimation("bench_failed");
            FailedAttempt();
        }

        if (sc.Swipe() == SwipeController.Directions.DOWN_SWIPE)
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseBench.BenchEnded;
                playerAnimationController.TriggerAnimation("bench_failed");
                FailedAttempt();
                return;
            }
            playerAnimationController.TriggerAnimation("bench_lockout");
            SuccessfulAttempt();
            gameplayController.LogSuccessfulAttempt();
        }
    }
}
