using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public GameObject player;

    public void LoadScene(string sceneName)
    {
        print($"��������� ����� {sceneName}");
        Time.timeScale = 1; // ������������ �����

        // ��������� ������ MouseMovement
        player.GetComponent<MouseMovement>().enabled = false;

        // ������������ � ���������� ������
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // ��������� ��������� �����
        SceneManager.LoadScene(sceneName);
    }
}
