using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquatAnimationController : MonoBehaviour
{
    /// <summary>
    /// The animator for the player.
    /// </summary>
    private Animator squatAnimator;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        // Get the animator from the pixel player squatting
        squatAnimator = GameObject.Find("Pixel Player Squat").GetComponent<Animator>();
    }

    /// <summary>
    /// Squat down animation.
    /// </summary>
    public void SquatDown()
    {
        squatAnimator.SetTrigger("squat_down");
    }

    /// <summary>
    /// Squat up animation.
    /// </summary>
    public void SquatConcentric()
    {
        squatAnimator.SetTrigger("squat_up");
    }

    /// <summary>
    /// Squat sticking animation.
    /// </summary>
    public void SquatSticking()
    {
        squatAnimator.SetTrigger("squat_sticking");
    }

    /// <summary>
    /// Squat lockout animation.
    /// </summary>
    public void SquatLockout()
    {
        squatAnimator.SetTrigger("squat_lockout");
    }

    /// <summary>
    /// Squat failed animation.
    /// </summary>
    public void SquatFailed()
    {
        squatAnimator.SetTrigger("squat_failed");
    }

    /// <summary>
    /// Squat lockout animation.
    /// </summary>
    public void SquatFailedIdle()
    {
        squatAnimator.SetTrigger("squat_failed_idle");
    }
}
