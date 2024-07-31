using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private LevelSettingsSO levelSettingsSo;
    
    private List<NodeBehavior> _levelNodes = new();
    
    private void Awake()
    {
        _levelNodes = GetComponentsInChildren<NodeBehavior>().ToList();
        
        if(levelSettingsSo.RandomizeNodesRotationOnStart)
            _levelNodes.ForEach(x=>x.RandomizeRotationAtStart());
    }
    
    public void OnGameStarted()
    {
        _levelNodes.ForEach(x=>x.OnGameStarted());
    }

    public bool AllNodesConnected()
    {
        return _levelNodes.TrueForAll(x => x.GetConnectionStatus());
    }
    
}
