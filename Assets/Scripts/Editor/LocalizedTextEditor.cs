using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
public class LocalizedTextEditor : EditorWindow
{
	//Photon networking
	//https://www.youtube.com/watch?v=evrth262vfs
	
	//https://www.youtube.com/channel/UCTY3kks3U4RDvpMX87fvo1A
    private LocalizationData localizationData;
    [MenuItem("Window/Localized Text Editor")] //title window
    static void Init()
    {
        EditorWindow.GetWindow(typeof(LocalizedTextEditor)).Show();
        // LocalizedTextEditor window = (LocalizedTextEditor)EditorWindow.GetWindow(typeof(LocalizedTextEditor));
        // window.Show();
    }

    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    private void OnGUI()
    {
        if (localizationData != null)
        {
            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("localizationData");
            EditorGUILayout.PropertyField(serializedProperty, true);
            serializedObject.ApplyModifiedProperties();

            if (GUILayout.Button("Save Data"))
            {
                SaveGameData();
            }

        }

        if (GUILayout.Button("Load Data"))
        {
            LoadGameData();
        }

        if (GUILayout.Button("Create New Data"))
        {
            CreateNewData();
        }
    }
    private void LoadGameData()
    {
        //Get file path
        string filePath = EditorUtility.OpenFilePanel("Select localization data file", Application.streamingAssetsPath,"json");
        //string filePath = Application.dataPath + gameDataProjectFilePath;

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            localizationData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        }
    }
    private void CreateNewData()
    {
        localizationData = new LocalizationData();
    }

    private void SaveGameData()
    {
        string filePath = EditorUtility.SaveFilePanel("Save localization data file", Application.streamingAssetsPath, "", "json");

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(localizationData);
            File.WriteAllText(filePath, dataAsJson);
        }
    }
}
