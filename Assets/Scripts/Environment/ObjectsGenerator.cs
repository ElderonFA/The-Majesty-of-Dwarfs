using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObjectsGenerator : MonoBehaviour
{
    [Header("Generate settings")]
    [SerializeField] 
    protected List<GameObject> pulledObjects;
    [SerializeField] 
    private float distance;
    [SerializeField] 
    private float zLayer;

    private void Start()
    {
        Generate();
    }

    protected void Generate()
    {
        var posY = transform.position.y;
        
        for (var i = 0; i < pulledObjects.Count; i++)
        {
            var posX = transform.position.x + Random.Range(-distance, distance);
            
            pulledObjects[i].transform.position = new Vector3(posX, posY, zLayer);
        }
    }
}
