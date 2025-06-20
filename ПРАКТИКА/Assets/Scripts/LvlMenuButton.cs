using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlMenuButton : MonoBehaviour
{

    public TMP_Text highScoreUI01;
    public TMP_Text highScoreUI02;
    public TMP_Text highScoreUI03;
    string newGameScene = "LvlMenu";


    public AudioClip bgMusic;
    public AudioSource mainChannel;

    int highScore01;
    int highScore02;
    int highScore03;


    void Start()
    {
        mainChannel.PlayOneShot(bgMusic);

        // Set the high score text
        int highScore01 = SaveLoadManager.Instance.LoadHighScore("01");

        highScoreUI01.text = $"TopWave Survived: {highScore01}";

        int highScore02 = SaveLoadManager.Instance.LoadHighScore("02");

        highScoreUI02.text = $"TopWave Survived: {highScore02}";

        int highScore03 = SaveLoadManager.Instance.LoadHighScore("03");

        highScoreUI03.text = $"TopWave Survived: {highScore03}";

    }

    public void ResetRecords()
    {
        PlayerPrefs.SetInt("BestWaveSavedValue", 0);
        PlayerPrefs.SetInt("01", 0);
        PlayerPrefs.SetInt("02", 0);
        PlayerPrefs.SetInt("03", 0);
        highScore01 = SaveLoadManager.Instance.LoadHighScore("01");
        highScore02 = SaveLoadManager.Instance.LoadHighScore("02");
        highScore03 = SaveLoadManager.Instance.LoadHighScore("03");
        highScoreUI01.text = $"TopWave Survived: {highScore01}";
        highScoreUI02.text = $"TopWave Survived: {highScore02}";
        highScoreUI03.text = $"TopWave Survived: {highScore03}";
    }

    public void StartNewGame(string lvlName)
    {
        mainChannel.Stop();

        SceneManager.LoadScene(lvlName);
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
