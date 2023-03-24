using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableObjectsButtonConfig", menuName = "Create configs/InteractableObjectsButton", order = 1)]
public class InteractableObjectsButtonConfig : ScriptableObject
{
    [SerializeField]
    private List<IOButtonConfig> Configs;
    public List<IOButtonConfig> GetConfigs => Configs;
}

[Serializable]
public class IOButtonConfig
{
    public InteractableObjectsType type;
    public Sprite buttonBack;
    public Sprite buttonIcon;
}
