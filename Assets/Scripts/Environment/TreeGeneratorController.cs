using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGeneratorController : ObjectsGenerator
{
    [SerializeField] 
    private Vector2 minMaxInFront;
    
    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Generate();
        UpdatePlacement();
    }

    private void UpdatePlacement()
    {
        var countTreeInBack = pulledObjects.Count - Random.Range(minMaxInFront[0], minMaxInFront[1]);
        for (var i = 0; i < countTreeInBack; i++)
        {
            var itemTransform = pulledObjects[i].transform;
            var interactable = itemTransform.GetComponent<InteractableObject>();
            var renderer = itemTransform.GetComponent<SpriteRenderer>();

            var oldPos = itemTransform.position;
            itemTransform.position = new Vector3(oldPos.x, oldPos.y, -oldPos.z);

            interactable.OffInteractable();
        }
    }
}
