using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEventsChannel", menuName = "ScriptableObjects/Level/LevelEventsChannel")]
public class LevelEventsChannelSO : ScriptableObject
{
    public event Action<Level> NewLevelLoaded;
    public event Action AllNodesConnected;
    public event Action LevelFinished;

    public void OnNewLevelLoaded(Level level)
    {
        NewLevelLoaded?.Invoke(level);
    }
    
    public void OnAllNodesConnected()
    {
        AllNodesConnected?.Invoke();
    }
    
    public void OnLevelFinished()
    {
        LevelFinished?.Invoke();
    }
}
