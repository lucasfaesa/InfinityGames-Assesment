using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private CameraEventsChannelSO cameraEventsChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [Header("Components")]
    [SerializeField] private Camera mainCamera;

    private void OnEnable()
    {
        levelEventsChannel.NewLevelLoaded += OnNewLevelLoaded;
    }

    private void OnDisable()
    {
        levelEventsChannel.NewLevelLoaded -= OnNewLevelLoaded;
    }

    private void OnNewLevelLoaded(Level level)
    {
        cameraEventsChannel.OnCameraSizeAnimationStarted();
        
        DOTween.To(x => mainCamera.orthographicSize = x, mainCamera.orthographicSize,
            level.GetLevelSettings().PreferredCameraSize, 2f).SetEase(Ease.OutBack)
            .OnComplete(cameraEventsChannel.OnCameraSizeAnimationEnded);

    }
}
