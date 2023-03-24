using System;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    [SerializeField] 
    private LayerMask interactableLayer;
    
    [SerializeField] 
    private InteractableObjectButton standardButton;

    [SerializeField] 
    private InteractableObjectsButtonConfig _interactableObjectsButtonConfig;

    private Vector2 startPosButton;
    private bool buttonShow;

    private Camera mainCamera;

    public static Action onInteractConfirm;

    private void Start()
    {
        Init();
    }

    private void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        if (Input.GetMouseButton(0))
        {
            var hit = Physics2D.Raycast(
                mainCamera.ScreenToWorldPoint(Input.mousePosition), 
                Vector2.zero, 
                1000f, 
                interactableLayer);
            
            if (hit.collider != null)
            {
                var interactable = hit.transform.GetComponent<InteractableObject>();

                if (!interactable.GetIsInteractable)
                {
                    return;
                }
                
                ShowInteractionButton(interactable);
            }
            else
            {
                HideInteractionButton();
            }
        }
    }

    public void Init()
    {
        mainCamera = Camera.main;

        startPosButton = standardButton.transform.position;

        onInteractConfirm += HideInteractionButton;
    }

    private void ShowInteractionButton(InteractableObject interactableObject)
    {
        buttonShow = true;
        
        var configs = _interactableObjectsButtonConfig.GetConfigs;

        var objectConfig = configs.Find(x => x.type == interactableObject.GetInteractableType);
        
        standardButton.UpdateButton(objectConfig.buttonBack, objectConfig.buttonIcon, interactableObject.InteractEvent);

        var posObj = interactableObject.transform;
        //надо менять GetComponent
        standardButton.transform.position = new Vector3(posObj.position.x, posObj.position.y + posObj.gameObject.GetComponent<SpriteRenderer>().bounds.size.y + 1f, -2);
    }

    private void HideInteractionButton()
    {
        if (!buttonShow)
        {
            return;
        }

        standardButton.transform.position = startPosButton;
    }
}

public enum InteractableObjectsType
{
    Tree = 0,
}
