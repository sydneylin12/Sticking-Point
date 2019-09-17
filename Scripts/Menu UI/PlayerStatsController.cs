using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsController : MonoBehaviour
{
    /// <summary>
    /// Text on the pause menu displaying the player's stats.
    /// </summary>
    private Text playerStatsText;

    /// <summary>
    /// Called before the first frame.
    /// </summary>
    void Start()
    {
        playerStatsText = GameObject.Find("PlayerStatsText").GetComponent<Text>();
        UpdatePlayerStatsText();
    }

    /// <summary>
    /// Updates player statistics text on the pause menu.
    /// </summary>
    public void UpdatePlayerStatsText()
    {
        PlayerData data = SaveSystem.LoadGame();
        playerStatsText.text = String.Format(
            "Player Name: {0} \n " +
            "Player Level: {1} \n " +
            "Squat: {2} \n " +
            "Bench: {3} \n " +
            "Deadlift: {4} \n " +
            "Total: {5}", 
            data.playerName, data.playerLevel, data.maxes[0], data.maxes[1], data.maxes[2], data.total);
    }
}
