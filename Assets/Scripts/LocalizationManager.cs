using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingText = "Missing Text";

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    // Use this for initialization
    void Start()
    {

    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        //string filePath = Path.Combine(Application.streamingAssetsPath, fileName);
        string filePath = Application.dataPath + "/StreamingAssets/" + fileName + ".json";
        Debug.Log("Path: " + filePath);
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            for (int i = 0; i < localizationData.items.Length; i++)
            {
                localizedText.Add(localizationData.items[i].key, localizationData.items[i].value);
            }
            Debug.Log("Data size: " + localizedText.Count);
        }
        else
        {
            Debug.LogError("Error");
        }
        isReady = true;
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingText;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}
