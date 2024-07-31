using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;
    [SerializeField] private NodeEventChannelSO nodeEventChannel;
    
    private Level _currentLevel;
    
    private void OnEnable()
    {
        gameEventsChannel.GamePreparingToStart += OnGameStarted;
        nodeEventChannel.NodeConnectionStatusChanged += OnNodeConnectionStatusChanged;
    }

    private void OnDisable()
    {
        gameEventsChannel.GamePreparingToStart -= OnGameStarted;
        nodeEventChannel.NodeConnectionStatusChanged -= OnNodeConnectionStatusChanged;
    }

    private void OnGameStarted()
    {
        _currentLevel = Instantiate(levelsManagerData.GetCurrentLevel()); 
    }
    
    private void OnNodeConnectionStatusChanged(bool _, NodeBehavior __)
    {
        if (_currentLevel.AllNodesConnected())
        {
            levelEventsChannel.OnAllNodesConnected();
        }
    }
}
