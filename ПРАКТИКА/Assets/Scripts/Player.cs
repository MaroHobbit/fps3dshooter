using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public string lvlRecordPref;
    public int HP = 100;
    public GameObject damageScreen;

    public TextMeshProUGUI playerHealthUI;
    public GameObject gameOverUI;

    public bool isDead = false;

    public bool isPaused;
    public GameObject GameMenu;

    public GameObject sceneLoader;

    public void Start()
    {
        playerHealthUI.text = $"Health: {HP}";
        SoundManager.Instance.inGameMusicChannel.PlayOneShot(SoundManager.Instance.inGameMusic);
    }

    public void Update()
    {
        // Проверяем нажатие кнопки ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Переключаем состояние паузы
            isPaused = !isPaused;

            // Если игра на паузе, активируем канвас и останавливаем время
            if (isPaused)
            {
                Time.timeScale = 0; // Останавливаем время
                if (GameMenu != null)
                {
                    GameMenu.SetActive(true);
                }
            }
            else
            {
                // Если игра не на паузе, деактивируем канвас и возобновляем время
                Time.timeScale = 1; // Возобновляем время
                if (GameMenu != null)
                {
                    GameMenu.SetActive(false);
                }
            }
        }
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        print($"Player hp = {HP}");

        if (HP < 1)
        {
            isDead = true;
            print("player die");
            
            StartCoroutine(damageScreenEffect());
            PlayerDead();

            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerDeath);
        }
        else
        {

            print("Damage");
            StartCoroutine(damageScreenEffect());
            playerHealthUI.text = $"  Health: {HP}";

            SoundManager.Instance.playerChannel.PlayOneShot(SoundManager.Instance.playerHurt);

        }


    }

    private void PlayerDead()
    {
        
        GetComponent<PlayerMovement>().enabled = false;

        //Dying Animation
        GetComponentInChildren<Animator>().enabled = true;

        playerHealthUI.gameObject.SetActive(false);

        GetComponent<ScreenFader>().StartFade();
        StartCoroutine(ShowGameOwerUI());

    }

    private IEnumerator ShowGameOwerUI()
    {
        yield return new WaitForSeconds(1f);
        gameOverUI.gameObject.SetActive(true);

        int waveSurvived = GlobalReferences.Instance.waveNumber - 1;

        if (waveSurvived > SaveLoadManager.Instance.LoadHighScore(lvlRecordPref))
        {
            SaveLoadManager.Instance.SaveHighScore(lvlRecordPref, waveSurvived);
        }

        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(4f);

        sceneLoader.GetComponent<SceneLoader>().LoadScene("MainMenu");
    }

    private IEnumerator damageScreenEffect()
    {
        if (damageScreen.activeInHierarchy == false) 
        {
            damageScreen.SetActive(true);
        }
        
        var image = damageScreen.GetComponentInChildren<Image>();

        // Set the initial alpha value to 1 (fully visible).
        Color startColor = image.color;
        startColor.a = 1f;
        image.color = startColor;

        float duration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Calculate the new alpha value using Lerp.
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);

            // Update the color with the new alpha value.
            Color newColor = image.color;
            newColor.a = alpha;
            image.color = newColor;

            // Increment the elapsed time.
            elapsedTime += Time.deltaTime;

            yield return null; ; // Wait for the next frame.
        }



        if (damageScreen.activeInHierarchy == true)
        {
            damageScreen.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ZombieHand"))
        {
            if (isDead == false)
            {
                TakeDamage(other.gameObject.GetComponent<ZombieHand>().damage);
            }
            
        }
    }
}
