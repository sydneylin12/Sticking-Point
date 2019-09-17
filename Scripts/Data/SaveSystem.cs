using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

[System.Serializable]
public static class SaveSystem
{
    /// <summary>
    /// Saves the Player's statistics to a binary file called savegame.sp.
    /// </summary>
    public static void SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/savegame.sp";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    /// <summary>
    /// Loads the player's statistics from a file called savegame.sp.
    /// </summary>
    public static PlayerData LoadGame()
    {
        string path = Application.persistentDataPath + "/savegame.sp";
        if(File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData data = (PlayerData)formatter.Deserialize(stream);
            stream.Close();

            //load the setting in from the file
            GameGlobals.LoadPlayer(data.playerLevel, data.currentXp, data.xpLeft);
            return data;
        }
        else
        {
            Debug.Log("File not found.");
            return null;
        }
    }
}
