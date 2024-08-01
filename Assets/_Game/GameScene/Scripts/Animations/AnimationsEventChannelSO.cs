using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationsEventChannel", menuName = "ScriptableObjects/Animation/AnimationsEventChannel")]
public class AnimationsEventChannelSO : ScriptableObject
{
    public event Action FinishLevelAnimationsStarted;
    public event Action FinishLevelAnimationsEnded;

    public event Action FinishLevelPostProcessingAnimationsStarted;
    public event Action FinishLevelPostProcessingAnimationsEnded;

    public void OnFinishLevelAnimationsStarted()
    {
        FinishLevelAnimationsStarted?.Invoke();
    }

    public void OnFinishLevelAnimationsEnded()
    {
        FinishLevelAnimationsEnded?.Invoke();
    }

    public void OnFinishLevelPostProcessingAnimationsStarted()
    {
        FinishLevelPostProcessingAnimationsStarted?.Invoke();
    }

    public void OnFinishLevelPostProcessingAnimationsEnded()
    {
        FinishLevelPostProcessingAnimationsEnded?.Invoke();
    }
}
