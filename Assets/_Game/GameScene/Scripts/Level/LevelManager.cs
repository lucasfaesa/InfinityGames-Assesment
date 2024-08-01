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

    private void Awake()
    {
        levelsManagerData.Reset();
    }

    private void OnEnable()
    {
        gameEventsChannel.GamePreparingToStart += OnGamePreparingToStart;
        gameEventsChannel.GameStarted += OnGameStarted;
        nodeEventChannel.NodeConnectionStatusChanged += OnNodeConnectionStatusChanged;
    }

    private void OnDisable()
    {
        gameEventsChannel.GamePreparingToStart -= OnGamePreparingToStart;
        gameEventsChannel.GameStarted -= OnGameStarted;
        nodeEventChannel.NodeConnectionStatusChanged -= OnNodeConnectionStatusChanged;
    }

    private void OnGamePreparingToStart()
    {
        if(_currentLevel?.gameObject != null)
            Destroy(_currentLevel.gameObject);
        
        _currentLevel = Instantiate(levelsManagerData.GetCurrentLevel());
        
        levelEventsChannel.OnNewLevelLoaded(_currentLevel);
    }
    private void OnGameStarted()
    {
        _currentLevel.OnGameStarted();
    }
    
    private void OnNodeConnectionStatusChanged(bool _, NodeBehavior __)
    {
        if (_currentLevel.AllNodesConnected())
        {
            if (levelsManagerData.CheckIfNextLevelExists())
            {
                levelsManagerData.IncrementLevel();
            }
            
            levelEventsChannel.OnAllNodesConnected();
            
            //TODO Remove Later

        }
    }
}
