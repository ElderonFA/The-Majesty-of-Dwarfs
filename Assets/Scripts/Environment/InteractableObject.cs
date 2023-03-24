using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
}
