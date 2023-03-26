using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGeneratorController : ObjectsGenerator
{
    [SerializeField] 
    private Vector2 minMaxInFront;

    public static Action onTreeDestroy;

    private int countTreeInFront;
    private int countDestroyedTrees;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Generate();
        StartPlacement();

        onTreeDestroy += ChekTreeCount;
    }

    private void ChekTreeCount()
    {
        countDestroyedTrees++;

        if (countTreeInFront <= countDestroyedTrees)
        {
            UpdateTrees();
        }
    }

    private void UpdateTrees()
    {
        countTreeInFront = 0;
        countDestroyedTrees = 0;
        
        foreach (var obj in pulledObjects)
        {
            var objTransform = obj.transform;
            var objInteractable = objTransform.GetComponent<TreeObject>();

            if (!objTransform.gameObject.activeSelf)
            {
                objInteractable.CreateSelf();
                continue;
            }

            if (!objInteractable.GetIsInteractable)
            {
                countTreeInFront++;
                objInteractable.OnInteractable(true);
            }
        }
    }

    private void StartPlacement()
    {
        countTreeInFront = Mathf.RoundToInt(Random.Range(minMaxInFront[0], minMaxInFront[1]));
        for (var i = 0; i < pulledObjects.Count - countTreeInFront; i++)
        {
            var itemTransform = pulledObjects[i].transform;
            var interactable = itemTransform.GetComponent<InteractableObject>();

            var oldPos = itemTransform.position;
            itemTransform.position = new Vector3(oldPos.x, oldPos.y, -oldPos.z);

            interactable.OffInteractable();
        }
    }
}
