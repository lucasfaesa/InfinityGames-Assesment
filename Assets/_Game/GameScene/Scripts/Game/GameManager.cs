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

    void Start()
    {
        StartCoroutine(StartGame());
    }
    
    private void OnAllNodesConnected()
    {
        gameEventsChannel.OnGameEnded();

        StartCoroutine(StartGame());
    }

    //TODO Remove this
    private IEnumerator StartGame()
    {
        gameEventsChannel.OnGamePreparingToStart();
        
        yield return new WaitForSeconds(1f);
        
        gameEventsChannel.OnGameStarted();
    }

}
