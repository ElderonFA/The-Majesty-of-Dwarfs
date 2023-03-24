using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(SpriteRenderer))]
public class TreeObject : InteractableObject
{
    [Space]
    [SerializeField] 
    private SpriteRenderer spriteRenderer;
    [SerializeField] 
    private Sprite cutTreeSprite;
    [SerializeField] 
    private int backLayerPos;

    public override void DoInteract()
    {
        InteractionController.onInteractConfirm?.Invoke();
        
        spriteRenderer.sprite = cutTreeSprite;

        isInteractable = false;
    }
    
    public override void OffInteractable()
    {
        isInteractable = false;

        var oldColor = spriteRenderer.color;
        var blackFactor = 0.75f;
        spriteRenderer.color = new Color(
            oldColor.r - blackFactor, 
            oldColor.g - blackFactor, 
            oldColor.b - blackFactor,
            oldColor.a);

        spriteRenderer.sortingOrder = backLayerPos;
    }
    
    public override void OnInteractable()
    {
        isInteractable = true;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }
}
