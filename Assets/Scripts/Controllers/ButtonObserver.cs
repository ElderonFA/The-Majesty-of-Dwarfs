using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
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
    [Space]
    [SerializeField] 
    private CanvasGroup menuCanvas;
    [SerializeField] 
    private Transform cameraTransform;
    [SerializeField] 
    private UIHelper uiHelper;
    
    [SerializeField] 
    private CutSceneConfig startCutSceneConfig;
    
    [Space]
    [SerializeField] 
    private GameObject CinemachineConfiner;
    
    [Space]
    [SerializeField] 
    private GameObject clickBlocker;

    private void Start()
    {
        uiHelper.Initialize();
        
        playButton.onClick.AddListener(Play);
        settingsButton.onClick.AddListener(OpenSettings);
        exitButton.onClick.AddListener(Exit);
    }

    private void Play()
    {
        UIHelper.hideCanvasEvent?.Invoke(menuCanvas, menuCanvas.gameObject);
        StartCoroutine(CameraMove());
    }

    private IEnumerator CameraMove()
    {
        clickBlocker.SetActive(true);
        
        var pos = cameraTransform.position;
        var distance = 0f;
        
        while (distance > -18f)
        {
            distance -= Time.deltaTime * 5.5f;

            cameraTransform.position = new Vector3(pos.x, distance, pos.z);
            
            yield return null;
        }
        
        clickBlocker.SetActive(false);

        CinemachineConfiner.GetComponent<CinemachineConfiner>().enabled = true;
        //CutSceneController.OnStartCutScene?.Invoke(startCutSceneConfig.GetConfigCutScene);
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
