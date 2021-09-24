using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animal))]
public class AnimalSoundPlayer : MonoBehaviour
{

    #region Variables

    AudioSource animalAudioSource;
    [SerializeField] AudioClip noise;
    AnimalAnimationPlayer animationPlayer;

    #endregion

    #region Awake

    private void Awake()
    {
        animalAudioSource = GetComponent<AudioSource>();
        animationPlayer = GetComponent<AnimalAnimationPlayer>();
    }

    #endregion

    #region Playing noise

    public void DoNoiseCheck()
    {
        float randomNum = Random.Range(0f, 1f);

        if (randomNum > 0.99f)
        {
            PlayNoise();
        }
    }

    void PlayNoise()
    {
        if (gameObject.activeSelf == false)
        {
            animationPlayer.HideMusicNote();
            return;
        }

        animationPlayer.DisplayMusicNote();
        animalAudioSource.clip = noise;
        animalAudioSource.Play();
    }

    #endregion

}
