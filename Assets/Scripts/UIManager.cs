using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UIManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public GameObject MainMenu;
    public GameObject HighScoreScreen;
    public TextMeshProUGUI highScoreText;

    private void Start()
    {
        if(HighScoreManager.Instance.playerName != "")
        {
            playerNameInput.text = HighScoreManager.Instance.playerName;
        }
    }

    public void StartGame()
    {
        HighScoreManager.Instance.playerName = playerNameInput.text;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void ViewHighScores()
    {
        DisplayHighScoreList();
        MainMenu.SetActive(false);
        HighScoreScreen.SetActive(true);
    }

    public void ReturnToMenu()
    {
        MainMenu.SetActive(true);
        HighScoreScreen.SetActive(false);
    }

    public void DisplayHighScoreList()
    {
        HighScoreManager.Instance.LoadHighScores();
        
        string[] names = HighScoreManager.Instance.highScoreNames;
        int[] scores = HighScoreManager.Instance.highScores;
        highScoreText.text = "";

        for(int i = 0; i < names.Length; i++)
        {
            highScoreText.text += i + ". " + names[i] + ": " + scores[i] + "\n";
        }
        //highScoreText.text = HighScoreManager.Instance.highScoreNames[0] + ": " + HighScoreManager.Instance.highScores[0];
    }

    public void ClearHighScores()
    {
        HighScoreManager.Instance.ClearHighScores();
        DisplayHighScoreList();
    }
}
