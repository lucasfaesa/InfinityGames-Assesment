using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelManagerDataSO levelManagerData;

    private Level _currentLevel;
    
    private void OnEnable()
    {
        gameEventsChannel.GamePreparingToStart += OnGameStarted;
    }

    private void OnDisable()
    {
        gameEventsChannel.GamePreparingToStart -= OnGameStarted;
    }

    private void OnGameStarted()
    {
        _currentLevel = Instantiate(levelManagerData.GetCurrentLevel()); 
    }
}
