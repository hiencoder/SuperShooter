using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
public class DataController : MonoBehaviour
{
    private RoundData[] allRoundData;
    public string fileNameData = "data.json";
    private PlayerProgress playerProgress;
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //Load data from json assets
        LoadGameData();
        LoadPlayerProgress();
        SceneManager.LoadScene("MenuScene");
    }

    public RoundData getCurrentRoundData()
    {
        return allRoundData[0];
    }

    private void LoadPlayerProgress()
    {
        playerProgress = new PlayerProgress();
        if (PlayerPrefs.HasKey("highestScore"))
        {
            playerProgress.highScore = PlayerPrefs.GetInt("highestScore");

        }
        else
        {
            playerProgress.highScore = 0;
        }
    }

    private void SavePlayerProgress()
    {
        PlayerPrefs.SetInt("highestScore", playerProgress.highScore);
    }

    public void SubmitNewHightScore(int newScore)
    {
        if (newScore > playerProgress.highScore)
        {
            playerProgress.highScore = newScore;
            SavePlayerProgress();
        }
    }

    public int GetHighestScore()
    {
        return playerProgress.highScore;
    }


    private void LoadGameData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, fileNameData);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            GameData loadedData = JsonUtility.FromJson<GameData>(dataAsJson);
            allRoundData = loadedData.allRoundData;
        }
        else
        {
            Debug.LogError("Cannot load game data");
        }
    }
}
