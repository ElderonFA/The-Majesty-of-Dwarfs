using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectButton : MonoBehaviour
{
    [SerializeField] 
    private SpriteRenderer backSpriteRenderer;
    [SerializeField] 
    private SpriteRenderer iconSpriteRenderer;

    private Action interactEvent;

    private void OnMouseUp()
    {
        Debug.Log("Button click");
        interactEvent?.Invoke();

        InteractionController.onInteractConfirm?.Invoke();
    }

    public void UpdateButton(Sprite backSprite, Sprite iconSprite, Action a)
    {
        interactEvent = a;
        
        backSpriteRenderer.sprite = backSprite;
        iconSpriteRenderer.sprite = iconSprite;
    }
}
