using System;
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
    }

    public virtual void DoInteract()
    {
        Debug.Log("Interact with object!");
    }

    public virtual void OffInteractable()
    {
        isInteractable = false;
    }
    
    public virtual void OnInteractable()
    {
        isInteractable = true;
    }
}
