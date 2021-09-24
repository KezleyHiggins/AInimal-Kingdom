using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    #region Variables
    public static MusicPlayer instance;
    [SerializeField] AudioSource jukebox;
    #endregion

    #region Awake & Singleton setting

    private void Awake()
    {
        SetSingleton();
        if (jukebox == null) { jukebox = GetComponent<AudioSource>(); }
        if (jukebox.isPlaying == false) { PlayMusic(); }
        jukebox.volume = PlayerPrefs.GetFloat("Music Volume");
    }

    private void SetSingleton()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region Using the music player

    public void SetMusicVolume(float newVolume)
    {
        jukebox.volume = newVolume;
    }

    void PlayMusic()
    {
        jukebox.Play();
    }

    #endregion

}
