using System;
using Unity.VisualScripting;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] 
    protected bool isInteractable = true;
    public bool GetIsInteractable => isInteractable;
    
    [SerializeField] 
    private InteractableObjectsType type;

    public InteractableObjectsType GetInteractableType => type;

    public Action InteractEvent;

    private void Start()
    {
        InteractEvent += DoInteract;
        
        CustomStart();
    }

    public virtual void DoInteract()
    {
        Debug.Log("Interact with object!");
    }

    public virtual void OffInteractable()
    {
        isInteractable = false;
    }
    
    public virtual void OnInteractable(bool onWithAnim = false)
    {
        isInteractable = true;
    }
    
    public virtual void CustomStart()
    {
        //Здесь допы для старта
    }

    private void OnDestroy()
    {
        InteractEvent -= DoInteract;
    }
}
