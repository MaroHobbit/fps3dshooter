using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Weapon;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; set; }

    public AudioSource ShootingChannel;
    public AudioSource reloadingSoundAK74;
    public AudioSource reloadingSoundM1911;
    public AudioSource emptyMagazineSoundM1911;
    public AudioSource throwablesChannel;
    public AudioSource zombieChannel;
    public AudioSource zombieChannel2;
    public AudioSource playerChannel;
    public AudioSource inGameMusicChannel;

    public AudioClip M1911Shot;
    public AudioClip AK74Shot;
    public AudioClip grenadeSound;
    public AudioClip zombieWalking;
    public AudioClip zombieChasing;
    public AudioClip zombieAttaking;
    public AudioClip zombieHurt;
    public AudioClip zombieDeath;
    public AudioClip playerHurt;
    public AudioClip playerDeath;
    public AudioClip inGameMusic;

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
    }

    public void PlayShootingSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                ShootingChannel.PlayOneShot(M1911Shot);
                break;
            case WeaponModel.AK74:
                ShootingChannel.PlayOneShot(AK74Shot);
                break;
        }
    }

    public void PlayReloadSound(WeaponModel weapon)
    {
        switch (weapon)
        {
            case WeaponModel.M1911:
                reloadingSoundM1911.Play();
                break;
            case WeaponModel.AK74:
                reloadingSoundAK74.Play();
                break;
        }
    }

    // Method to set the music volume
    public void SetMusicVolume(float volume)
    {
        inGameMusicChannel.volume = volume;
    }

    // Method to set the SFX volume
    public void SetSFXVolume(float volume)
    {
        ShootingChannel.volume = volume;
        reloadingSoundAK74.volume = volume;
        reloadingSoundM1911.volume = volume;
        emptyMagazineSoundM1911.volume = volume;
        throwablesChannel.volume = volume;
        zombieChannel.volume = volume;
        zombieChannel2.volume = volume;
        playerChannel.volume = volume;
    }
}
