using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickSoundPlayer : MonoBehaviour
{
    [SerializeField] 
    private AudioClip buttonSound;

    private Button selfButton;

    private void Start()
    {
        selfButton = GetComponent<Button>();

        selfButton.onClick.AddListener(PlayButtonSound);
    }

    private void PlayButtonSound()
    {
        AudioEventPlay.playEvent?.Invoke(buttonSound);
    }

    private void OnDestroy()
    {
        selfButton.onClick.RemoveAllListeners();
    }
}
