using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelSettingsSO levelSettingsSo;
    
    private List<NodeBehavior> _levelNodes = new();
    
    private void Awake()
    {
        _levelNodes = GetComponentsInChildren<NodeBehavior>().ToList();
        
        if(levelSettingsSo.RandomizeNodesRotationOnStart)
            _levelNodes.ForEach(x=>x.RandomizeRotationAtStart());
    }

    private void OnEnable()
    {
        gameEventsChannel.GameStarted += OnGameStarted;
    }

    private void OnDisable()
    {
        gameEventsChannel.GameStarted -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _levelNodes.ForEach(x=>x.OnGameStarted());
    }
}
