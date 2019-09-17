using UnityEngine;

public class BenchAnimationController : PlayerAnimationController
{
    /// <summary>
    /// The animator for the player.
    /// </summary>
    private Animator benchAnimator;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        // Get the animator for the bench press player
        benchAnimator = GameObject.Find("Pixel Player Bench").GetComponent<Animator>();
    }

    /// <summary>
    /// Bench lockout/rack animation.
    /// </summary>
    public void BenchIdle()
    {
        benchAnimator.SetTrigger("bench_idle");
    }

    /// <summary>
    /// Bench down animation.
    /// </summary>
    public void BenchDown()
    {
        benchAnimator.SetTrigger("bench_down");
    }

    /// <summary>
    /// Bench up animation.
    /// </summary>
    public void BenchUp()
    {
        benchAnimator.SetTrigger("bench_up");
    }

    /// <summary>
    /// Bench sticking point animation.
    /// </summary>
    public void BenchSticking()
    {
        benchAnimator.SetTrigger("bench_sticking");
        //animator.ResetTrigger("bench_sticking");
    }

    /// <summary>
    /// Bench lockout/rack animation.
    /// </summary>
    public void BenchLockout()
    {
        benchAnimator.SetTrigger("bench_lockout");
    }

    /// <summary>
    /// Bench lockout/rack animation.
    /// </summary>
    public void BenchFailed()
    {
        benchAnimator.SetTrigger("bench_lockout");
    }

    /// <summary>
    /// Bench lockout/rack animation.
    /// </summary>
    public void BenchFailedIdle()
    {
        benchAnimator.SetTrigger("bench_lockout");
    }
}

