using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeSetter : MonoBehaviour
{

    #region Variables

    Slider volumeSlider;
    MusicPlayer musicPlayer;

    #endregion

    #region Start & Update

    void Start()
    {
        musicPlayer = MusicPlayer.instance;
        volumeSlider = GetComponent<Slider>();
        volumeSlider.onValueChanged.AddListener(delegate { UpdateVolume(); });

        InitializeSound();
    }

    void InitializeSound()
    {
        bool gameHasOpenedBefore = PlayerPrefs.GetInt("Game Has Opened Before") == 1;

        if (gameHasOpenedBefore)
        {
            bool musicIsMuted = PlayerPrefs.GetInt("Music Is Muted") == 1;
            if (musicIsMuted)
            {
                volumeSlider.value = 0;
                UpdateVolume();
            }
            else
            {
                volumeSlider.value = PlayerPrefs.GetFloat("Music Volume");
                UpdateVolume();
            }
        }
        else
        {
            volumeSlider.value = 1;
            UpdateVolume();
            PlayerPrefs.SetInt("Game Has Opened Before", 1);
        }
    }

    void UpdateVolume()
    {
        float newVolume = volumeSlider.value;
        PlayerPrefs.SetFloat("Music Volume", newVolume);

        if (newVolume <= 0) PlayerPrefs.SetInt("Music Is Muted", 1);
        else PlayerPrefs.SetInt("Music Is Muted", 0);

        musicPlayer.SetMusicVolume(volumeSlider.value);
    }

    #endregion

}
