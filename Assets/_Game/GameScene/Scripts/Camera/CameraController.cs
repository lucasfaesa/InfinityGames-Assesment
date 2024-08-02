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
    [SerializeField] private AnimationsEventChannelSO animationsEventChannel;
    [Header("Components")]
    [SerializeField] private Camera mainCamera;

    private Vector3 defaultCameraPos = new Vector3(0, 0, -10);
    
    private void OnEnable()
    {
        levelEventsChannel.NewLevelLoaded += OnNewLevelLoaded;
        levelEventsChannel.RequestToLoadNextLevel += OnRequestToLoadNextLevel;
        animationsEventChannel.FinishLevelPostProcessingAnimationsEnded += OffsetCameraUp;
    }

    private void OnDisable()
    {
        levelEventsChannel.NewLevelLoaded -= OnNewLevelLoaded;
        levelEventsChannel.RequestToLoadNextLevel -= OnRequestToLoadNextLevel;
        animationsEventChannel.FinishLevelPostProcessingAnimationsEnded -= OffsetCameraUp;
    }

    private void OnNewLevelLoaded(Level level)
    {
        cameraEventsChannel.OnCameraSizeAnimationStarted();
        
        DOTween.To(x => mainCamera.orthographicSize = x, mainCamera.orthographicSize,
            level.GetLevelSettings().PreferredCameraSize, 1f).SetEase(Ease.OutBack)
            .OnComplete(
                cameraEventsChannel.OnCameraSizeAnimationEnded
            );
    }

    private void OffsetCameraUp()
    {
       DOTween.Sequence().Append(mainCamera.transform.DOMoveY(-0.89f, 0.5f).SetEase(Ease.InOutSine)).PrependInterval(0.3f);
    }

    private void OnRequestToLoadNextLevel()
    {
        mainCamera.transform.position = defaultCameraPos;
    }
}
