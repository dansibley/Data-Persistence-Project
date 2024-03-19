using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance;

    public string playerName;

    public string[] highScoreNames = new string[5];
    public int[] highScores = new int[5];

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    
    class SaveData
    {
        public string[] highScoreNames = new string[5];
        public int[] highScores = new int[5];
    }

    public void NewHighScores(int newHighScore)
    {
        SaveData data = new SaveData();
        Debug.Log("New High Score: " + newHighScore);
        int insertAtPosition = 4;
        for(int i = 4; i>=0; i--)
        { 
            if (newHighScore > highScores[i])
            {
                insertAtPosition = i;
            }
        }
        for(int i = 4; i > insertAtPosition; i--)
        {
            highScores[i] = highScores[i - 1];
            highScoreNames[i] = highScoreNames[i - 1];
        }
        highScoreNames[insertAtPosition] = playerName;
        highScores[insertAtPosition] = newHighScore;

        data.highScores = highScores;
        data.highScoreNames = highScoreNames;


        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            highScoreNames = data.highScoreNames;
            highScores = data.highScores;
        }
        else
        {
            for(int i = 0; i<highScores.Length; i++)
            {
                highScores[i] = 0;
                highScoreNames[i] = "";
            }
        }
    }

    public void ClearHighScores()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if(File.Exists(path))
        {
            File.Delete(path);
        }
        
        LoadHighScores();
    }
}
