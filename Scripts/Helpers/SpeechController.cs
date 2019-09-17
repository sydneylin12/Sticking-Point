using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechController : MonoBehaviour
{
    /// <summary>
    /// The speech bubble's text.
    /// </summary>
    private Text speechText;

    /// <summary>
    /// The speech bubble GameObject.
    /// </summary>
    private GameObject speechBubble;

    /// <summary>
    /// The animator for the speech bubble.
    /// </summary>
    protected Animator speechBubbleAnimator;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        speechBubble = GameObject.Find("SpeechBubble");
        speechText = GameObject.Find("SpeechText").GetComponent<Text>();
        speechBubbleAnimator = GameObject.Find("SpeechBubble").GetComponent<Animator>();
        DeactivateSpeechBubble();
    }

    /// <summary>
    /// Activate the speech bubble and speech text with a small delay.
    /// </summary>
    /// <param name="text">The speech text.</param>
    public IEnumerator ActivateTimedSpeechBubble(string text)
    {
        speechBubble.gameObject.SetActive(true);
        speechBubbleAnimator.SetTrigger("SpeechActivated");
        speechBubbleAnimator.ResetTrigger("SpeechDeactivated");
        yield return new WaitForSeconds(0.12f);
        speechText.text = text;
        speechText.gameObject.SetActive(true);
    }

    /// <summary>
    /// Activates the speech bubble and sets its text..
    /// </summary>
    /// <param name="text">The speech text.</param>
    public void ActivateSpeechBubble(string text)
    {
        speechBubble.gameObject.SetActive(true);
        speechText.text = text;
        speechText.gameObject.SetActive(true);
        speechBubbleAnimator.SetTrigger("SpeechActivated");
        speechBubbleAnimator.ResetTrigger("SpeechDeactivated");
    }

    /// <summary>
    /// Deactivates the speech bubble and speech text.
    /// </summary>
    public void DeactivateSpeechBubble()
    {
        speechText.gameObject.SetActive(false);
        speechBubble.gameObject.SetActive(false);
        speechBubbleAnimator.SetTrigger("SpeechDeactivated");
        speechBubbleAnimator.ResetTrigger("SpeechActivated");
    }
}
