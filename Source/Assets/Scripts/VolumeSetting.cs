using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider music;
    [SerializeField] private Slider sfx;
    [SerializeField] private Slider main;
    audio_manager aud;


    private void Start()
    {
        aud = FindObjectOfType<audio_manager>();
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetMainVolume();
        }

    }

    public void SetMusicVolume()
    {
        float volume = music.value;
        mixer.SetFloat("Music", Mathf.Log10(volume)*20);
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = sfx.value;
        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    public void SetMainVolume()
    {
        float volume = main.value;
        mixer.SetFloat("Main", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("MainVolume", volume);
    }

    private void LoadVolume()
    {
        music.value = PlayerPrefs.GetFloat("MusicVolume");
        sfx.value = PlayerPrefs.GetFloat("SFXVolume");
        main.value = PlayerPrefs.GetFloat("MainVolume");
        SetMusicVolume();
        SetSFXVolume();
        SetMainVolume();
    }
}
