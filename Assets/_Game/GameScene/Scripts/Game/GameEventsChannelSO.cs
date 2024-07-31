using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEventsChannel", menuName = "ScriptableObjects/Game/GameEventsChannel")]
public class GameEventsChannelSO : ScriptableObject
{
    public event Action GamePreparingToStart;
    public event Action GameStarted;
    public event Action GamePreparingToEnd;
    public event Action GameEnded;
    
    public void OnGamePreparingToStart()
    {
        Debug.Log("Game Preparing to start");
        GamePreparingToStart?.Invoke();
    }

    public void OnGameStarted()
    {
        Debug.Log("Game Started");
        GameStarted?.Invoke();
    }

    public void OnGamePreparingToEnd()
    {
        Debug.Log("Game Preparing to End");
        GamePreparingToEnd?.Invoke();
    }

    public void OnGameEnded()
    {
        Debug.Log("Game Ended");
        GameEnded?.Invoke();
    }
    
}
