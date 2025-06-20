using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager Instance { get; set; }


    private string highScoreKey = "BestWaveSavedValue";


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);

    }
    
    public void SaveHighScore(string lvlHighScore, int score)
    {
        PlayerPrefs.SetInt(lvlHighScore, score);
    }

    public int LoadHighScore(string lvlHighScore)
    {
        if (PlayerPrefs.HasKey(lvlHighScore))
        {
            return PlayerPrefs.GetInt(lvlHighScore);
        }
        else
        {
            return 0;
        }
    }


}
