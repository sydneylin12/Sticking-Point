using System;
using UnityEngine;

public class LiftTrackerController : MonoBehaviour
{
    /// <summary>
    /// Gameobject for the lift tracker parent object.
    /// </summary>
    private GameObject liftTracker;

    /// <summary>
    /// Sprites for lights and check/x marks.
    /// </summary>
    private Sprite greenCheck, redX, smallDot;

    /// <summary>
    /// SpriteRenderer arrays of the lift tracker [9].
    /// </summary>
    private SpriteRenderer[] liftTrackerArr;

    /// <summary>
    /// The player's game data .
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        InitializeLiftTracker();
        LoadFromGameData();
    }

    /// <summary>
    /// Initializes all the GameObjects and resources.
    /// </summary>
    private void InitializeLiftTracker()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        liftTracker = GameObject.Find("Lift Tracker");
        liftTrackerArr = new SpriteRenderer[9];
        for (int i = 0; i < gameData.GetMaxAllowedAttempts(); i++)
        {
            // Initialize each child component of the lift tracker
            liftTrackerArr[i] = liftTracker.transform.GetChild(i).GetComponent<SpriteRenderer>();
        }
        smallDot = Resources.Load<Sprite>("Sprites/Light");
        greenCheck = Resources.Load<Sprite>("Sprites/Green Check");
        redX = Resources.Load<Sprite>("Sprites/Red X");
    }

    /// <summary>
    /// Load the lift tracker state from the array of AttemptState in GameData.
    /// </summary>
    public void LoadFromGameData()
    {
        for (int i = 0; i < gameData.GetMaxAllowedAttempts(); i++)
        {
            if((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.Success)
            {
                liftTrackerArr[i].sprite = greenCheck; //circle sprite
                liftTrackerArr[i].color = Color.white;
            }
            else if((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.Fail)
            {
                liftTrackerArr[i].sprite = redX; //circle sprite
                liftTrackerArr[i].color = Color.white;
            }
            else if((gameData.LiftTrackerState as AttemptState[])[i] == AttemptState.NotAttempted)
            {
                liftTrackerArr[i].sprite = smallDot; //circle sprite
                liftTrackerArr[i].color = Color.black;
            }
            else
            {
                throw new NullReferenceException("This should never happen.");
            }
        }
    }

    /// <summary>
    /// Update the state of the lift tracker with a given AttemptState enum.
    /// </summary>
    public void UpdateState(AttemptState attemptState)
    {
        if (attemptState == AttemptState.Success)
        {
            liftTrackerArr[gameData.CurrentAttempt].sprite = greenCheck;
            liftTrackerArr[gameData.CurrentAttempt].color = Color.white;
        }
        else if (attemptState == AttemptState.Fail)
        {
            liftTrackerArr[gameData.CurrentAttempt].sprite = redX;
            liftTrackerArr[gameData.CurrentAttempt].color = Color.white;
        }
        else if (attemptState == AttemptState.NotAttempted)
        {
            liftTrackerArr[gameData.CurrentAttempt].sprite = smallDot;
            liftTrackerArr[gameData.CurrentAttempt].color = Color.black;
        }
        else
        {
            throw new InvalidOperationException("This should never happen.");
        }
    }

    /// <summary>
    /// Reset the state of the lift tracker only.
    /// </summary>
    public void ResetLiftTracker()
    {
        for (int i = 0; i < gameData.GetMaxAllowedAttempts(); i++)
        {
            liftTrackerArr[gameData.CurrentAttempt].sprite = smallDot;
            liftTrackerArr[gameData.CurrentAttempt].color = Color.black;
        }
    }
}

