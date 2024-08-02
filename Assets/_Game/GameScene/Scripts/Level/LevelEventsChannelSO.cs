using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEventsChannel", menuName = "ScriptableObjects/Level/LevelEventsChannel")]
public class LevelEventsChannelSO : ScriptableObject
{
    public event Action<Level> NewLevelLoaded;
    public event Action LevelFinished;
    public event Action RequestToLoadNextLevel;
    
    public void OnNewLevelLoaded(Level level)
    {
        NewLevelLoaded?.Invoke(level);
    }
    
    public void OnLevelFinished()
    {
        LevelFinished?.Invoke();
    }

    public void OnRequestToLoadNextLevel()
    {
        RequestToLoadNextLevel?.Invoke();
    }
}
