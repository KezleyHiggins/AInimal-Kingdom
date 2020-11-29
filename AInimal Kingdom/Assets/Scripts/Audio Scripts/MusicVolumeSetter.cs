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

    private void Start()
    {
        musicPlayer = MusicPlayer.instance;
        volumeSlider = GetComponent<Slider>();
    }

    private void Update()
    {
        musicPlayer.SetMusicVolume(volumeSlider.value);
    }

    #endregion

}
