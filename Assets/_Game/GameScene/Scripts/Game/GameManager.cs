using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    

    IEnumerator Start()
    {
        gameEventsChannel.OnGamePreparingToStart();

        yield return new WaitForSeconds(0.5f);

        StartGame();
    }

    private void StartGame()
    {
        gameEventsChannel.OnGameStarted();
    }

}
