using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoTextController : MonoBehaviour
{
    /// <summary>
    /// The info text at the bottom of the level. 
    /// </summary>
    private Text infoText;

    /// <summary>
    /// The game data. 
    /// </summary>
    private GameData gameData;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        gameData = GameObject.Find("Game Data").GetComponent<GameData>();
        infoText = GameObject.Find("Info Text").GetComponent<Text>();
        UpdateInfoText(gameData.CurrentExerciseType);
    }

    /// <summary>
    /// Updates the player level text ONLY. 
    /// </summary>
    public void UpdateLevelTextOnly()
    {
        string oldText = infoText.text;
        string newLevel = "LV: " + gameData.PlayerLevel;
        infoText.text = newLevel + oldText.Substring(oldText.IndexOf('|') - 1); //get the white space
    }

    /// <summary>
    /// Updates the player level and load for the scene based on previous attemptes. 
    /// </summary>
    /// <param name="exerciseType">The exercise type.</param>
    public void UpdateInfoText(ExerciseType exerciseType)
    {
        AttemptState[] tracker = gameData.LiftTrackerState as AttemptState[];
        int level = gameData.PlayerLevel;
        int currAttempt = gameData.CurrentAttempt;
        int currWeight = 0; // To be assigned

        // 90%, 95%, 100%
        int opener = (int)(0.9 * gameData.GetLiftFromLevel(exerciseType, level));
        int middle = (int)(0.95 * gameData.GetLiftFromLevel(exerciseType, level));
        int final = gameData.GetLiftFromLevel(exerciseType, level);

        if (currAttempt == 0 || currAttempt == 3 || currAttempt == 6)
        {
            currWeight = opener;
        }
        else if (currAttempt == 1 || currAttempt == 4 || currAttempt == 7)
        {
            if (tracker[currAttempt - 1] == AttemptState.Fail)
            {
                currWeight = opener;
            }
            else
            {
                currWeight = middle;
            }
        }
        else if (currAttempt == 2 || currAttempt == 5 || currAttempt == 8)
        {
            if (tracker[currAttempt - 2] == AttemptState.Fail && tracker[currAttempt - 1] == AttemptState.Fail)
            {
                currWeight = opener;
            }
            else if (tracker[currAttempt - 2] == AttemptState.Fail && tracker[currAttempt - 1] == AttemptState.Success)
            {
                currWeight = middle;
            }
            else if (tracker[currAttempt - 2] == AttemptState.Success && tracker[currAttempt - 1] == AttemptState.Fail)
            {
                currWeight = middle;
            }
            else
            {
                currWeight = final;
            }
        }
        infoText.text = "LV: " + gameData.PlayerLevel + " | LOAD: " + currWeight;
    }
}
