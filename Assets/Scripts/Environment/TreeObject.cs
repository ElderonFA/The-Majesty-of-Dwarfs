using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class TreeObject : InteractableObject
{
    [Space]
    [Header("Object elements")]
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private BoxCollider2D boxCollider;
    
    [Header("Sprites")]
    [SerializeField] 
    private Sprite initialSprite;
    [SerializeField] 
    private Sprite stumpSprite;
    
    [Header("Sounds")]
    [SerializeField] 
    private AudioClip treeFallSound;
    [SerializeField] 
    private AudioClip treeCutSound;

    [Space]
    [SerializeField] 
    private int backLayerPos;
    [SerializeField] 
    private int frontLayerPos;
    
    [SerializeField]
    [Range (0f, 1f)]
    private float blackFactor;

    private TreeState currentState = TreeState.Initially;

    private Color interactableColor = new (1f, 1f, 1f, 1f);
    private Color notInteractableColor = new (0.75f, 0.75f, 0.75f, 1f);

    //1 - size, 2 - offset
    private Vector2[] initBoxColliderConfig;
    private Vector2[] felledBoxColliderConfig;

    public override void CustomStart()
    {
        notInteractableColor = new Color(1f - blackFactor, 1f - blackFactor, 1f - blackFactor, 1f);
        
        initBoxColliderConfig = new Vector2[2] {
            boxCollider.size, 
            boxCollider.offset
        };

        felledBoxColliderConfig = new Vector2[2] {
            new (0.32f, 0.37f), 
            new (0f, 0.14f)
        };
    }

    public override void DoInteract()
    {
        InteractionController.onInteractConfirm?.Invoke();
        spriteRenderer.sprite = stumpSprite;

        switch (currentState)
        {
            case TreeState.Initially:
                CutTree();
                break;
            case TreeState.Felled:
                BurnStump();
                break;
        }
        
    }

    private void CutTree()
    {
        spriteRenderer.sprite = stumpSprite;
        
        currentState = TreeState.Felled;
        
        UpdateBoxCollider(currentState);
        
        AudioEventPlay.playEvent?.Invoke(treeFallSound);
        
        //Drop wood
    }
    
    private void BurnStump()
    {
        AudioEventPlay.playEvent?.Invoke(treeCutSound);
        
        DestroySelf();
    }
    
    public override void OffInteractable()
    {
        isInteractable = false;
        
        spriteRenderer.color = notInteractableColor;

        spriteRenderer.sortingOrder = backLayerPos;
    }
    
    public override void OnInteractable(bool onWithAnim = false)
    {
        if (onWithAnim)
        {
            StartCoroutine(AnimOnInteractable());
        }
        else
        {
            isInteractable = true;
            spriteRenderer.color = interactableColor;
        }
    }

    private IEnumerator AnimOnInteractable()
    {
        isInteractable = false;
        spriteRenderer.sortingOrder = frontLayerPos;

        var step = 0f;

        while (spriteRenderer.color.r < 1f)
        {
            step += 0.8f * Time.deltaTime;
            spriteRenderer.color = new Color(notInteractableColor.r + step, notInteractableColor.g + step, notInteractableColor.b + step);
            yield return null;
        }
        
        isInteractable = true;
        
        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, -pos.z);
    }

    private IEnumerator AnimCreateSelf()
    {
        var alfa = 0f;
        spriteRenderer.color = new Color(notInteractableColor.r, notInteractableColor.g, notInteractableColor.b, alfa);

        while (alfa < 1f)
        {
            alfa += 1f * Time.deltaTime;
            spriteRenderer.color = new Color(notInteractableColor.r, notInteractableColor.g, notInteractableColor.b, alfa);
            yield return null;
        }
    }

    public void CreateSelf()
    {
        gameObject.SetActive(true);
        
        isInteractable = false;
        currentState = TreeState.Initially;
        spriteRenderer.sprite = initialSprite;
        spriteRenderer.color = notInteractableColor;
        spriteRenderer.sortingOrder = backLayerPos;

        var pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y, -pos.z);
        
        UpdateBoxCollider(currentState);
        
        StartCoroutine(AnimCreateSelf());
    }

    public void DestroySelf()
    {
        gameObject.SetActive(false);

        spriteRenderer.sortingOrder = backLayerPos;
        
        TreeGeneratorController.onTreeDestroy?.Invoke();
    }

    private void UpdateBoxCollider(TreeState state)
    {
        switch (state)
        {
            case TreeState.Initially:
                boxCollider.size = initBoxColliderConfig[0];
                boxCollider.offset = initBoxColliderConfig[1];
                break;
            case TreeState.Felled:
                boxCollider.size = felledBoxColliderConfig[0];
                boxCollider.offset = felledBoxColliderConfig[1];
                break;
        }
    }

    private enum TreeState
    {
        Initially,
        Felled,
    }
}
