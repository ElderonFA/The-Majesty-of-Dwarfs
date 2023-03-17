using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioEventPlay : MonoBehaviour
{
    public static Action<AudioClip> playEvent;

    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        playEvent += PlayAudio;
    }

    private void PlayAudio(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }

    private void OnDestroy()
    {
        playEvent -= PlayAudio;
    }
}
