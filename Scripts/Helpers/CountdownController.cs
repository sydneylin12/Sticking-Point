using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    /// <summary>
    /// The timer countdown text.
    /// </summary>
    private Text countdownText;

    /// <summary>
    /// The current number of seconds on the timer.
    /// </summary>
    private int seconds;

    /// <summary>
    /// The timer countdown text.
    /// </summary>
    private readonly string TIME_MAX = "1:00";

    /// <summary>
    /// The timer countdown text.
    /// </summary>
    private readonly string TIME_MIN = "0:00";

    /// <summary>
    /// Indicates if the countdown timer can continue.
    /// </summary>
    private bool continueCountdown;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        countdownText = GameObject.Find("Countdown Text").GetComponent<Text>();
        seconds = 60;
        continueCountdown = true;
    }

    /// <summary>
    /// Starts the countdown coroutine.
    /// </summary>
    public void BeginCountdown()
    {
        StartCoroutine(CountingDown());
    }

    /// <summary>
    /// Subtracts a second off the timer in a loop.
    /// </summary>
    private IEnumerator CountingDown()
    {
        while(seconds > 0 && continueCountdown)
        {
            if (seconds == 60)
            {
                countdownText.text = TIME_MAX;
                seconds--;
                yield return new WaitForSeconds(0.5f);
            }
            else if (seconds >= 10)
            {
                countdownText.text = "0:" + seconds;
                seconds--;
                yield return new WaitForSeconds(0.5f);
            }
            else if (seconds > 0 && seconds < 10)
            {
                countdownText.text = "0:0" + seconds;
                seconds--;
                yield return new WaitForSeconds(0.5f);
            }
            else
            {
                throw new System.Exception("Invalid time. This should never happen.");
            }
        }

        // Loop has ended, update final time
        if (seconds >= 10)
        {
            countdownText.text = "0:" + seconds;
        }
        else if (seconds > 0 && seconds < 10)
        {
            countdownText.text = "0:0" + seconds;
        }
        else if(seconds <= 0)
        {
            countdownText.text = TIME_MIN;
        }
        else
        {
            throw new System.Exception("Invalid time given. This should never happen.");
        }
    }

    /// <summary>
    /// Completely stops the countdown.
    /// </summary>
    public void StopCountdown()
    {
        continueCountdown = false;
    }

    /// <summary>
    /// Returns if the countdown has ended.
    /// </summary>
    /// <returns>True if the countdown is over and false if above 0.</returns>
    public bool CountdownOver()
    {
        return (seconds == 0);
    }
}
