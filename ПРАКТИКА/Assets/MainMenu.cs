using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TMP_Text highScoreUI;
    string newGameScene = "LvlMenu";


    public AudioClip bgMusic;
    public AudioSource mainChannel;
    int highScore;


    void Start()
    {
        mainChannel.PlayOneShot(bgMusic);

        // Set the high score text
        int highScore = SaveLoadManager.Instance.LoadHighScore("BestWaveSavedValue");

        highScoreUI.text = $"TopWave Survived: {highScore}";

    }
    
    public void ResetRecords()
    {
        PlayerPrefs.SetInt("BestWaveSavedValue", 0);
        highScore = SaveLoadManager.Instance.LoadHighScore("BestWaveSavedValue");
        highScoreUI.text = $"TopWave Survived: {highScore}";
    }

    public void StartNewGame()
    {
        mainChannel.Stop();

        SceneManager.LoadScene(newGameScene);
    }

    public void ExitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
