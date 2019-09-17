using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SquatController : Timer
{
    /// <summary>
    ///The current phase of the squat exercise.
    /// </summary>
    private CurrentPhaseSquat currentPhase;

    void Start()
    {
        InitializeTimer();
        sc = new SwipeController();
        ResetExercise();
        currentPhase = CurrentPhaseSquat.SquatNone;
    }

    void Update()
    {
        // DEBUG USE ONLY
        if (Input.GetKeyDown(KeyCode.R))
        {
            OverrideWin();
        }

        // Determine what phase we are on
        switch (currentPhase)
        {
            case CurrentPhaseSquat.SquatNone:
                // Do nothing here
                break;
            case CurrentPhaseSquat.SquatEnded:
                // Do nothing here
                break;
            case CurrentPhaseSquat.SquatStart:
                EccentricPhase();
                break;
            case CurrentPhaseSquat.SquatSticking:
                StickingPhase();
                break;
            case CurrentPhaseSquat.SquatRack:
                RackPhase();
                break;
            default:
                Debug.Log("Invalid current state for squat.");
                break;
        }
    }

    /// <summary>
    /// Initiates the eccentric squat phase.
    /// </summary>
    public void StartExercise()
    {
        speechController.ActivateSpeechBubble("Squat!");
        timeRemaining = timeMax;
        canStartLift = false;
        currentPhase = CurrentPhaseSquat.SquatStart;
    }

    /// <summary>
    /// Initiates the sticking point phase.
    /// </summary>
    public void StartSticking()
    {
        speechController.DeactivateSpeechBubble();
        timeRemaining = 1f;
        currentPhase = CurrentPhaseSquat.SquatSticking;
    }

    /// <summary>
    /// Initiates the racking phase of the squat.
    /// </summary>
    public void StartRack()
    {
        speechController.ActivateSpeechBubble("Rack!");
        timeRemaining = timeMax;
        currentPhase = CurrentPhaseSquat.SquatRack;
    }

    /// <summary>
    /// Decreases time and detects down swipe.
    /// </summary>
    public void EccentricPhase()
    {
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();
        // Check if the timer has ran out
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseSquat.SquatEnded;
            playerAnimationController.TriggerAnimation("squat_failed");
            FailedAttempt();
        }
        // Check for input
        if (sc.Swipe() == SwipeController.Directions.DOWN_SWIPE)
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseSquat.SquatEnded;
                playerAnimationController.TriggerAnimation("squat_failed");
                return;
            }
            playerAnimationController.TriggerAnimation("squat_down");
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
            currentPhase = CurrentPhaseSquat.SquatEnded;
            playerAnimationController.TriggerAnimation("squat_failed");
            FailedAttempt();
        }
        else if (timeRemaining == timeMax)
        {
            Invoke("StartRack", 0.5f);
        }
        // Check for input
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
           playerAnimationController.TriggerAnimation("squat_sticking");
        }
        
    }

    /// <summary>
    /// Decreases time and detects down swipe.
    /// </summary>
    public void RackPhase()
    {
        slider.value = CalculateSliderValue();
        SubtractTime();
        bool inBounds = CheckBound();
        if (!CheckTimerEnded())
        {
            currentPhase = CurrentPhaseSquat.SquatEnded;
            playerAnimationController.TriggerAnimation("squat_failed");
            FailedAttempt();
        }

        if (sc.Swipe() == SwipeController.Directions.DOWN_SWIPE)
        {
            if (!inBounds)
            {
                currentPhase = CurrentPhaseSquat.SquatEnded;
                playerAnimationController.TriggerAnimation("squat_failed");
                FailedAttempt();
                return;
            }
            playerAnimationController.TriggerAnimation("squat_lockout");
            currentPhase = CurrentPhaseSquat.SquatEnded;
            SuccessfulAttempt();
        }
    }
}
