using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    public Slider musicVolumeSlider; // Slider for music volume
    public Slider sfxVolumeSlider; // Slider for SFX volume

    private void Start()
    {
        // Initialize sliders with current volume levels from SoundManager
        if (musicVolumeSlider != null)
        {
            musicVolumeSlider.value = SoundManager.Instance.inGameMusicChannel.volume;
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
        }

        if (sfxVolumeSlider != null)
        {
            sfxVolumeSlider.value = SoundManager.Instance.ShootingChannel.volume; // Assuming all SFX channels have the same volume initially
            sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }

    private void SetMusicVolume(float volume)
    {
        SoundManager.Instance.SetMusicVolume(volume);
    }

    private void SetSFXVolume(float volume)
    {
        SoundManager.Instance.SetSFXVolume(volume);
    }
}
