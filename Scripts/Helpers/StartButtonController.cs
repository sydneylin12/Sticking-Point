using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    /// <summary>
    /// The start/continue button GameObject.
    /// </summary>
    private Button startButton;

    /// <summary>
    /// Text for the start/continue button.
    /// </summary>
    private Text startButtonText;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        startButton = GameObject.Find("StartButton").GetComponent<Button>();
        startButtonText = GameObject.Find("StartText").GetComponent<Text>();
        SetButtonStart();
    }

    /// <summary>
    /// Sets the play button to say start and activates it.
    /// </summary>
    public void SetButtonStart()
    {
        startButtonText.text = "Start";
        startButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Sets the play button to say continue and activates it.
    /// </summary>
    public void SetButtonContinue()
    {
        startButtonText.text = "Continue";
        startButton.gameObject.SetActive(true);
    }

    /// <summary>
    /// Deactivates the play button.
    /// </summary>
    public void DeactivateButton()
    {
        startButton.gameObject.SetActive(false);
    }
}
