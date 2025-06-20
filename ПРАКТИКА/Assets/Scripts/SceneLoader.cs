using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject player;

    public void LoadScene(string sceneName)
    {
        print($"загружаем сцену {sceneName}");
        Time.timeScale = 1; // Возобновляем время

        // Отключаем скрипт MouseMovement
        player.GetComponent<MouseMovement>().enabled = false;

        // Разблокируем и отображаем курсор
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // Загружаем указанную сцену
        SceneManager.LoadScene(sceneName);
    }
}
