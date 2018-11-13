using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class GameDataEditor
{
    public GameData gameData;
    public string gameDataProjectFilePath = "/StreamingAssets/data.json";

    private void LoadGameData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        if (File.Exists(filePath))
        {
            // json string
            string dataAsJson = File.ReadAllText(filePath);
            gameData = JsonUtility.FromJson<GameData>(dataAsJson);

        }
        else
        {
            gameData = new GameData();
        }
    }

    private void SaveGameData()
    {
        string dataAsJson = JsonUtility.ToJson(gameData);
        string filePath = Application.dataPath + gameDataProjectFilePath;
		File.WriteAllText(filePath, dataAsJson);
    }
}
