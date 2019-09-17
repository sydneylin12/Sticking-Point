using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameData: MonoBehaviour
{
    // Properties
    public string CurrentScene { get; set; }
    public int PlayerLevel { get; private set; }
    public IEnumerable<AttemptState> LiftTrackerState { get; private set; }
    public int[,] MaxArray { get; private set; }
    public int CurrentAttempt { get; private set; }
    public int CurrentXp { get; private set; }
    public int XpLeft { get; private set; }

    // Fields
    private static GameData thisInstance;
    private int levelCap, xpBase;
    private float timeDecreasePercentage, timeTotal, bound;
    private readonly int maxAllowedAttempts = 9;

    /// <summary>
    /// The GameData Constructor.
    /// </summary>
    public GameData()
    {
        // Properties
        this.CurrentAttempt = 0;
        this.PlayerLevel = 1;
        this.XpLeft = 100;
        this.CurrentXp = 0;
        this.LiftTrackerState = new AttemptState[this.maxAllowedAttempts];

        // Fields
        this.xpBase = 100;
        this.levelCap = 50;
        this.timeDecreasePercentage = 0.955f;
        this.bound = 0.5f;
        this.timeTotal = 2.0f * Mathf.Pow(timeDecreasePercentage, this.PlayerLevel);

        // Initialize
        ResetLiftTracker();
        InitMaxArray();
    }

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        DontDestroyOnLoad(this); // Persist data in between scenes

        if (thisInstance == null)
        {
            thisInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Gets the current exercise type.
    /// </summary>
    /// <returns>The current exercise type.</returns>
    public ExerciseType CurrentExerciseType
    {
        get
        {
            if (this.CurrentAttempt >= 0 && this.CurrentAttempt < 3)
            {
                return ExerciseType.Squat;
            }
            else if (this.CurrentAttempt >= 3 && this.CurrentAttempt < 6)
            {
                return ExerciseType.Bench;
            }
            else if (this.CurrentAttempt >= 6 && this.CurrentAttempt < 9)
            {
                return ExerciseType.Deadlift;
            }
            else
            {
                // Should never happen
                throw new InvalidOperationException("Current Attempt outside expected boundaries!");
            }
        }
    }

    /// <summary>
    /// Returns boolean to check if the attempts may be continued.
    /// </summary>
    /// <returns>True if the user can continue making attempts.</returns>
    public bool CanContinue()
    {
        return (CurrentAttempt < 9);
    }

    /// <summary>
    /// Sets the state of the lift tracker at the current attempt.
    /// </summary>
    /// <param name="currentAttemptState">The current attempt state.</param>
    public void LogCurrentAttempt(AttemptState currentAttemptState)
    {
        // Set the current attempt state
        (this.LiftTrackerState as AttemptState[])[this.CurrentAttempt] = currentAttemptState;

        // Unless we are already at the last attempt ...
        if (this.CurrentAttempt <= this.maxAllowedAttempts)
        {
            // ... increment the current attempt index (e.g. will get up to 9, if your max is 9)
            this.CurrentAttempt++;
        }

        // Throw exception if over
        else if(this.CurrentAttempt > this.maxAllowedAttempts)
        {
            throw new ArgumentOutOfRangeException("Exceeded the max number of attempts.");
        }
    }

    /// <summary>
    /// Load player stats from save game.
    /// </summary>
    /// <param name="level">The player level.</param>
    /// <param name="curr">The current xp.</param>
    /// <param name="left">The xp left.</param>
    public void LoadPlayer(int level, int curr, int left)
    {
        this.PlayerLevel = level;
        this.CurrentXp = curr;
        this.XpLeft = left;
    }

    /// <summary>
    /// Reset player stats.
    /// </summary>
    public void ResetPlayer()
    {
        this.PlayerLevel = 1;
        CurrentXp = 0;
        XpLeft = 100;
        SaveSystem.SaveGame();
    }

    /// <summary>
    /// Gets the time total.
    /// </summary>
    /// <returns>The total time.</returns>
    public float GetTimeTotal() { return timeTotal; }

    /// <summary>
    /// Gets the maximum allowed attempts.
    /// </summary>
    /// <returns>The maximum allowed attempts</returns>
    public int GetMaxAllowedAttempts() { return maxAllowedAttempts; }

    /// <summary>
    /// Gets the timer bound.
    /// </summary>
    /// <returns>The timer bount as a decimal (percentage).</returns>
    public float GetBound() { return bound; }

    /// <summary>
    /// Gets the 2-D lifts array.
    /// </summary>
    /// <param name="exerciseType">The exercise type.</param>
    /// <param name="level">The player level.</param>
    /// <returns>The 2-D array of maxes.</returns>
    public int GetLiftFromLevel(ExerciseType exerciseType, int level) {
        return MaxArray[(int)exerciseType, level];
    }

    /// <summary>
    /// Updates the time bounds (green time = total time * bound) based on the difficulty passed in.
    /// </summary>
    public void UpdateDifficulty(string difficulty)
    {
        switch (difficulty)
        {
            case "Untrained":
                bound = 0.5f;
                break;
            case "Novice":
                bound = 0.4f;
                break;
            case "Intermediate":
                bound = 0.3f;
                break;
            case "Advanced":
                bound = 0.2f;
                break;
            case "Elite":
                bound = 0.1f;
                break;
        }
    }

    /// <summary>
    /// Resets attempts to 0 and the lift tracker to all zeroes. 
    /// </summary>
    public void ResetLiftTracker()
    {
        CurrentAttempt = 0;
        for (int i = 0; i < 9; i++)
        {
            // Must cast to array to index
            (this.LiftTrackerState as AttemptState[])[i] = AttemptState.NotAttempted;
        }
    }

    /// <summary>
    /// Set the lift tracker's previous lifts for debugging and starting on a certian lift. 
    /// </summary>
    /// <param name="exerciseType">The current exercise type.</param>
    public void OverrideAttemptsDebug(ExerciseType exerciseType)
    {
        if(exerciseType == ExerciseType.Squat)
        {
            // Opening lift is squat so override should not be needed
            CurrentAttempt = 0;
        }
        else if(exerciseType == ExerciseType.Bench)
        {
            CurrentAttempt = 3;
            for (int i = 0; i < 3; i++)
            {
                // Must cast to array to index
                (this.LiftTrackerState as AttemptState[])[i] = AttemptState.Success;
            }
        }
        else if(exerciseType == ExerciseType.Deadlift)
        {
            CurrentAttempt = 6;
            for (int i = 0; i < 6; i++)
            {
                // Must cast to array to index
                (this.LiftTrackerState as AttemptState[])[i] = AttemptState.Success;
            }
        }
        else
        {
            throw new InvalidOperationException("Invalid exercise type!");
        }
    }

    /// <summary>
    /// Adds player XP based on the int passed in.
    /// </summary>
    public void GainXp(int xp)
    {
        if (this.PlayerLevel >= levelCap)
        {
            CurrentXp = 0;
            return; // Return if level cap has been hit and do not add XP
        }
        CurrentXp += xp;
        if (CurrentXp >= XpLeft)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// Levels the player up and resets xp needed.
    /// </summary>
    private void LevelUp()
    {
        CurrentXp -= XpLeft;
        this.PlayerLevel++;
        Debug.Log(this.PlayerLevel);
        XpLeft = xpBase * this.PlayerLevel;
        timeTotal = 2.0f * Mathf.Pow(timeDecreasePercentage, this.PlayerLevel);
    }

    /// <summary>
    /// Initialize the array of maxes based on player level.
    /// </summary>
    private void InitMaxArray()
    {
        MaxArray = new int[3, levelCap + 1];
        //0 = squat, 1 = bench, 2 = deadlift
        MaxArray[0, 0] = 135;
        MaxArray[1, 0] = 45;
        MaxArray[2, 0] = 225;
        //start at 1, go to level end of array at 50
        //Debug.Log("Level: 0  S: " + MaxArray[0, 0] + " B: " + MaxArray[1, 0] + " D: " + MaxArray[2, 0]);
        for (int i = 1; i < levelCap + 1; i++)
        {
            MaxArray[0, i] = MaxArray[0, i - 1] + 9;
            MaxArray[1, i] = MaxArray[1, i - 1] + 8;
            MaxArray[2, i] = MaxArray[2, i - 1] + 10;
            //Debug.Log("Level: " + i + " S: " + MaxArray[0, i] + " B: " + MaxArray[1, i] + " D: " + MaxArray[2, i]);
        }
        //should end up with 585, 445, 725
    }
}
