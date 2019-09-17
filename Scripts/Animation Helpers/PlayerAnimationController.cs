using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    /// <summary>
    /// The animator for the player (squat, bench, or deadlift).
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        // Get the animator from the pixel player (universal for all 3)
        animator = GameObject.Find("Pixel Player").GetComponent<Animator>();
    }

    /// <summary>
    /// Triggers an animation.
    /// </summary>
    /// <param name="animation">The animation name</param>
    public void TriggerAnimation(string animation)
    {
        animator.SetTrigger(animation);
    }
}
