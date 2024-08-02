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
    [SerializeField] private UIEventsChannelSO uiEventsChannel;
    
    private Level _currentLevel;
    private bool _levelFinished;

    private void OnEnable()
    {
        gameEventsChannel.GamePreparingToStart += OnGamePreparingToStart;
        levelEventsChannel.RequestToLoadNextLevel += OnLoadNextLevel;
        nodeEventChannel.NodeConnectionStatusChanged += OnNodeConnectionStatusChanged;
    }

    private void OnDisable()
    {
        gameEventsChannel.GamePreparingToStart -= OnGamePreparingToStart;
        levelEventsChannel.RequestToLoadNextLevel -= OnLoadNextLevel;
        nodeEventChannel.NodeConnectionStatusChanged -= OnNodeConnectionStatusChanged;
        
        levelsManagerData.Reset();
    }

    private void OnGamePreparingToStart()
    {
        InstantiateLevelNodes();
        
        levelEventsChannel.OnNewLevelLoaded(_currentLevel);
    }

    private void OnLoadNextLevel()
    {
        if (levelsManagerData.CheckIfNextLevelExists())
        {
            levelsManagerData.IncrementLevel();
        }
        
        uiEventsChannel.OnFadeOutStarted();
        
        InstantiateLevelNodes();
        
        levelEventsChannel.OnNewLevelLoaded(_currentLevel);
    }

    private void InstantiateLevelNodes()
    {
        _levelFinished = false;
        
        if(_currentLevel?.gameObject != null)
            Destroy(_currentLevel.gameObject);
        
        _currentLevel = Instantiate(levelsManagerData.GetCurrentLevel());
        
        _currentLevel.OnGameStarted();
    }
    
    private void OnNodeConnectionStatusChanged(bool _, NodeBehavior __)
    {
        if (_currentLevel.AllNodesConnected() && !_levelFinished)
        {
            _levelFinished = true;
            
            levelEventsChannel.OnLevelFinished();
        }
    }
}
