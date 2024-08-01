using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Fader : MonoBehaviour
{
    
    [Header("SOs")] 
    [SerializeField] private UIEventsChannelSO uiEventsChannel;

    [Header("Components")] 
    [SerializeField] private Image fadeImage;

    private void Awake()
    {
        DoFadeOut();
    }

    private void OnEnable()
    {
        uiEventsChannel.FadeInStarted += DoFadeIn;
        uiEventsChannel.FadeOutStarted += DoFadeOut;
    }

    private void OnDisable()
    {
        uiEventsChannel.FadeInStarted -= DoFadeIn;
        uiEventsChannel.FadeOutStarted -= DoFadeOut;
    }

    private void DoFadeIn()
    {
        fadeImage.color = new Color32((byte)0f,(byte)0f,(byte)0f,(byte)0f );

        fadeImage.DOFade(1f, 0.5f).SetEase(Ease.InOutSine)
            .OnComplete(uiEventsChannel.OnFadeInCompleted);
    }
    
    private void DoFadeOut()
    {
        fadeImage.color = new Color32((byte)0f,(byte)0f,(byte)0f,(byte)255f );

        fadeImage.DOFade(0f, 0.5f).SetEase(Ease.InOutSine)
            .OnComplete(uiEventsChannel.OnFadeOutCompleted);
    }
}
