using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private CameraEventsChannelSO cameraEventsChannel;

    private void OnEnable()
    {
        cameraEventsChannel.CameraSizeAnimationEnded += StartGame;
    }

    private void OnDisable()
    {
        cameraEventsChannel.CameraSizeAnimationEnded -= StartGame;
    }

    void Start()
    {
        gameEventsChannel.OnGamePreparingToStart();
    }

    private void StartGame()
    {
        gameEventsChannel.OnGameStarted();
    }

}
