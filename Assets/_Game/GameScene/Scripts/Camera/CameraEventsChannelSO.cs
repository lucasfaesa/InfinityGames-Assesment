using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraEventsChannel", menuName = "ScriptableObjects/Camera/CameraEventsChannel")]
public class CameraEventsChannelSO : ScriptableObject
{
    public event Action CameraSizeAnimationStarted;
    public event Action CameraSizeAnimationEnded;

    public void OnCameraSizeAnimationStarted()
    {
        CameraSizeAnimationStarted?.Invoke();
    }

    public void OnCameraSizeAnimationEnded()
    {
        CameraSizeAnimationEnded?.Invoke();
    }
}
