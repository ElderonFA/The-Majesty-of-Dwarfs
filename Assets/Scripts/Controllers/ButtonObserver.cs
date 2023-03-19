using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonObserver : MonoBehaviour
{
    [SerializeField] 
    private Button playButton;
    [SerializeField] 
    private Button settingsButton;
    [SerializeField] 
    private Button exitButton;
    
    
    //Надо будет поменять
    [SerializeField] 
    private CanvasGroup menuCanvas;
    [SerializeField] 
    private Transform cameraTransform;
    [SerializeField] 
    private UIHelper uiHelper;

    private void Start()
    {
        uiHelper.Initialize();
        
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(Exit);
    }

    private void Play()
    {
        UIHelper.hideCanvasEvent?.Invoke(menuCanvas, null);
        StartCoroutine(CameraMove());
    }

    private IEnumerator CameraMove()
    {
        var pos = cameraTransform.position;
        var distance = 0f;
        
        while (distance > -18f)
        {
            distance -= Time.deltaTime * 5.5f;

            cameraTransform.position = new Vector3(pos.x, distance, pos.z);
            
            yield return null;
        }
    }
    
    /*private IEnumerator CheckMenuAlpha()
    {
        while (menuCanvas.alpha > 0)
        {
            
        }
    }*/

    private void OpenSettings()
    {
        Debug.Log("Settings was open!");
    }

    private void Exit()
    {
        Application.Quit();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveAllListeners();
        settingsButton.onClick.RemoveAllListeners();
        exitButton.onClick.RemoveAllListeners();
    }
}
