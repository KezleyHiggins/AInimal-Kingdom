using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISFXPlayer : MonoBehaviour
{

    #region Variables

    AudioSource uiSFXPlayer;

    #endregion

    #region Awake & Playing Sounds

    void Awake()
    {
        uiSFXPlayer = GetComponent<AudioSource>();
    }

    public void PlayUISoundEffect(AudioClip sfx)
    {
        uiSFXPlayer.clip = sfx;
        uiSFXPlayer.Play();
    }

    #endregion

}
