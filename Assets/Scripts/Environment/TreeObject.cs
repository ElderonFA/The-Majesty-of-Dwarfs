using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class TreeObject : InteractableObject
{
    [Space]
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private Sprite cutTreeSprite;

    public override void DoInteract()
    {
        InteractionController.onInteractConfirm?.Invoke();
        
        spriteRenderer.sprite = cutTreeSprite;

        isInteractable = false;
    }
}
