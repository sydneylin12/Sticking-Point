using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionController : MonoBehaviour
{
    /// <summary>
    /// The animator for the fade to black transitions.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// The level to be loaded.
    /// </summary>
    private string levelToLoad;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        animator = GameObject.Find("Transition").GetComponent<Animator>();
    }

    /// <summary>
    /// Loads a scene with the scene name.
    /// </summary>
    public void FadeToLevel(string level)
    {
        levelToLoad = level;
        animator.SetTrigger("FadeOut");
    }

    /// <summary>
    /// Loads the level when the animation is finished.
    /// </summary>
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
