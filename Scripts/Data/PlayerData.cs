
[System.Serializable]
public class PlayerData
{
    /// <summary>
    /// The player's name.
    /// </summary>
    public string playerName;

    /// <summary>
    /// The player's level.
    /// </summary>
    public int playerLevel;

    /// <summary>
    /// The player's current XP.
    /// </summary>
    public int currentXp;

    /// <summary>
    /// The player's remaining XP in the current level.
    /// </summary>
    public int xpLeft;

    /// <summary>
    /// The 3-int array of the player's SBD.
    /// </summary>
    public int[] maxes;

    /// <summary>
    /// The player's total.
    /// </summary>
    public int total;

    /// <summary>
    /// The constructor for PlayerData, used in SaveGame().
    /// </summary>
    public PlayerData()
    {
        playerName = "Sid";
        playerLevel = GameGlobals.GetPlayerLevel();
        currentXp = GameGlobals.GetCurrentXp();
        xpLeft = GameGlobals.GetXpLeft();
        maxes = new int[3];
        maxes[0] = GameGlobals.GetLiftFromLevel(0, playerLevel);
        maxes[1] = GameGlobals.GetLiftFromLevel(1, playerLevel);
        maxes[2] = GameGlobals.GetLiftFromLevel(2, playerLevel);
        for (int i = 0; i < 3; i++)
        {
            total += maxes[i];
        }
    }

    
}
