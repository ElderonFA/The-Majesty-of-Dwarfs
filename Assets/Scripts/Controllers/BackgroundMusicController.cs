using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField] 
    private AudioSource mainAudioSource;
    
    [Space]
    [SerializeField] 
    private AudioClip menuSound;
    [SerializeField] 
    private AudioClip gameSound;

    private void Start()
    {
        ButtonObserver.startGame += PlayGameMusic;
    }

    private void PlayGameMusic()
    {
        mainAudioSource.clip = gameSound;
        mainAudioSource.Play();
    }
}
