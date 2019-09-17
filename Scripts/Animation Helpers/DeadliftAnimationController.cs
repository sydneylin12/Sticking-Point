using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadliftAnimationController : MonoBehaviour
{
    /// <summary>
    /// The animator for the player.
    /// </summary>
    private Animator deadliftAnimator;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        // Get the animator from the pixel player deadlifting
        deadliftAnimator = GameObject.Find("Pixel Player Deadlift").GetComponent<Animator>();
    }

    /// <summary>
    /// Deadlift breaking the floor animation.
    /// </summary>
    public void DeadliftBreakFloor()
    {
        deadliftAnimator.SetTrigger("deadlift_breakfloor");
    }

    /// <summary>
    /// Deadlift sticking point animation.
    /// </summary>
    public void DeadliftSticking()
    {
        deadliftAnimator.SetTrigger("deadlift_sticking");
        //animator.ResetTrigger("deadlift_sticking");
    }

    /// <summary>
    /// Deadlift lockout animation.
    /// </summary>
    public void DeadliftLockout()
    {
        deadliftAnimator.SetTrigger("deadlift_lockout");
    }

    /// <summary>
    /// Deadlift down.
    /// </summary>
    public void DeadliftDown()
    {
        deadliftAnimator.SetTrigger("deadlift_down");
    }

    /// <summary>
    /// Deadlift failed animation.
    /// </summary>
    public void DeadliftFailed()
    {
        deadliftAnimator.SetTrigger("deadlift_failed");
    }
       
    /// <summary>
    /// Deadlift failed idle animation.
    /// </summary>
    public void DeadliftFailedIdle()
    {
        deadliftAnimator.SetTrigger("deadlift_failed_idle");
    }

}
