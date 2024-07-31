using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;

    private void OnEnable()
    {
        levelEventsChannel.AllNodesConnected += OnAllNodesConnected;
    }

    private void OnDisable()
    {
        levelEventsChannel.AllNodesConnected -= OnAllNodesConnected;
    }

    IEnumerator Start()
    {
        gameEventsChannel.OnGamePreparingToStart();
        
        yield return new WaitForSeconds(2f);
        
        gameEventsChannel.OnGameStarted();    
    }
    
    private void OnAllNodesConnected()
    {
        gameEventsChannel.OnGameEnded();
    }

}
