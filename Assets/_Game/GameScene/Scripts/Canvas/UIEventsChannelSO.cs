using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIEventsChannel", menuName = "ScriptableObjects/UI/UIEventsChannel")]
public class UIEventsChannelSO : ScriptableObject
{
    
    public event Action FadeOutStarted;
    public event Action FadeOutCompleted;
    
    public event Action FadeInStarted;
    public event Action FadeInCompleted;
    
    public void OnFadeOutStarted()
    {
        FadeOutStarted?.Invoke();
    }

    public void OnFadeOutCompleted()
    {
        FadeOutCompleted?.Invoke();
    }

    public void OnFadeInStarted()
    {
        FadeInStarted?.Invoke();
    }

    public void OnFadeInCompleted()
    {
        FadeInCompleted?.Invoke();
    }
    
}
