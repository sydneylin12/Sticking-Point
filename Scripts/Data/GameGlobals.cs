using UnityEngine;
public static class GameGlobals
{
    //OBSOLETE CLASS!!!!!!!!!!!
    /// <summary>
    /// The state of the lift tracker (0 = not attempted, 1 = good, 2 = failed).
    /// </summary>
    private static int[] liftTrackerState;

    /// <summary>
    /// The current scene used for resuming the game.
    /// </summary>
    public static string currentScene { get; set; }

    /// <summary>
    /// The number of the current attempt.
    /// </summary>
    private static int attemptOn;

    /// <summary>
    /// The player's level.
    /// </summary>
    private static int playerLevel;

    /// <summary>
    /// The maximum level.
    /// </summary>
    private static int levelCap;

    /// <summary>
    /// The player's current XP.
    /// </summary>
    private static int currentXp;

    /// <summary>
    /// The base incrememnt of XP.
    /// </summary>
    private static int xpBase;

    /// <summary>
    /// The variable for player XP remaining to increase in level.
    /// </summary>
    private static int xpLeft;

    /// <summary>
    /// The 2D array of SBD maxes.
    /// </summary>
    private static int[,] liftsArr;

    /// <summary>
    /// Percent decrease in the timer.
    /// </summary>
    private static float timeDecreasePercentage;

    /// <summary>
    /// Total time allowed.
    /// </summary>
    private static float timeTotal;

    /// <summary>
    /// Total bound for the timer (percentage of time to get a good lift).
    /// </summary>
    private static float bound;

    /// <summary>
    /// The static constructor.
    /// </summary>
    static GameGlobals()
    {
        attemptOn = 0;
        levelCap = 50;
        playerLevel = 1;
        xpBase = 100;
        xpLeft = 100;
        currentXp = 0;
        timeDecreasePercentage = 0.955f;
        bound = 0.5f;
        liftTrackerState = new int[9];
        timeTotal = 2.0f * Mathf.Pow(timeDecreasePercentage, playerLevel);
        ResetLiftTracker();
        InitLiftsArr();
    }

    /// <summary>
    /// Gets the state of the lift tracker [0-9] (0 = not attempted, 1 = good, 2 = failed).
    /// </summary>
    public static int[] GetLiftTrackerState() { return liftTrackerState;  }

    /// <summary>
    /// Gets the attempt on.
    /// </summary>
    public static int GetAttemptOn() { return attemptOn; }

    /// <summary>
    /// Sets the state (0, 1, 2) of the lift tracker at the current attempt.
    /// </summary>
    public static void UpdateAttempt(int value)
    {
        liftTrackerState[attemptOn] = value;
        attemptOn++;
    }

    /// <summary>
    /// Gets the cplayer level.
    /// </summary>
    public static int GetPlayerLevel() { return playerLevel; }

    /// <summary>
    /// Gets the current xp.
    /// </summary>
    public static int GetCurrentXp() { return currentXp; }

    /// <summary>
    /// Gets the xp left.
    /// </summary>
    public static int GetXpLeft() { return xpLeft; }

    /// <summary>
    /// Load player stats from save game.
    /// </summary>
    public static void LoadPlayer(int level, int curr, int left)
    {
        playerLevel = level;
        currentXp = curr;
        xpLeft = left;
    }

    /// <summary>
    /// Reset player stats.
    /// </summary>
    public static void ResetPlayer()
    {
        playerLevel = 1;
        currentXp = 0;
        xpLeft = 100;
        SaveSystem.SaveGame();
    }

    /// <summary>
    /// Gets the time total.
    /// </summary>
    public static float GetTimeTotal() { return timeTotal; }

    /// <summary>
    /// Gets the 2d lifts array.
    /// </summary>
    public static int GetLiftFromLevel(int sbd, int level) { return liftsArr[sbd, level]; }

    /// <summary>
    /// Gets the timer bound.
    /// </summary>
    public static float GetBound() { return bound; }

    /// <summary>
    /// Updates the time bounds (green time = total time * bound) based on the difficulty passed in.
    /// </summary>
    public static void UpdateDifficulty(string difficulty)
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
    /// Resets the lift tracker to all zeroes. 
    /// </summary>
    public static void ResetLiftTracker()
    {
        for(int i = 0; i < 9; i++)
        {
            liftTrackerState[i] = 0;
        }
    }

    /// <summary>
    /// Adds player XP based on the int passed in.
    /// </summary>
    public static void GainXp(int xp)
    {
        if(playerLevel >= levelCap)
        {
            currentXp = 0; //set xp to 0
            return; //back out if level cap has been hit and do not add XP
        }
        currentXp += xp;
        if (currentXp >= xpLeft)
        {
            LevelUp();
        }
    }

    /// <summary>
    /// levels the player up and resets xp needed/
    /// </summary>
    public static void LevelUp()
    {
        currentXp -= xpLeft;
        playerLevel++;
        xpLeft = xpBase * playerLevel;
        timeTotal = 2.0f * Mathf.Pow(timeDecreasePercentage, playerLevel);
        Debug.Log(timeTotal);
    }

    /// <summary>
    /// Initialize the array of 1RMs based on player level.
    /// </summary>
    public static void InitLiftsArr()
    {
        liftsArr = new int[3, levelCap + 1];
        //0 = squat, 1 = bench, 2 = deadlift
        liftsArr[0, 0] = 135;
        liftsArr[1, 0] = 45;
        liftsArr[2, 0] = 225;
        //start at 1, go to level end of array at 50
        //Debug.Log("Level: 0  S: " + liftsArr[0, 0] + " B: " + liftsArr[1, 0] + " D: " + liftsArr[2, 0]);
        for (int i = 1; i < levelCap+1; i++)
        {
            liftsArr[0, i] = liftsArr[0, i - 1] + 9;
            liftsArr[1, i] = liftsArr[1, i - 1] + 8;
            liftsArr[2, i] = liftsArr[2, i - 1] + 10;
            //Debug.Log("Level: " + i + " S: " + liftsArr[0, i] + " B: " + liftsArr[1, i] + " D: " + liftsArr[2, i]);
        }
        //should end up with 585, 445, 725
    }
}
