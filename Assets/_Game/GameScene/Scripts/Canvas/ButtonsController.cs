using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private AnimationsEventChannelSO animationsEventChannel;
    [SerializeField] private UIEventsChannelSO uiEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private AudioEventsChannel audioEventsChannel;
    [Header("Components")] 
    [SerializeField] private CanvasGroup uiCanvasCanvasGroup;
    [SerializeField] private RectTransform nextAndLeaveButtonsParent;
    [SerializeField] private GameObject nextLevelButton;
    [Header("Audio")] 
    [SerializeField] private AudioClipSO clickAudioClip;

    private Vector2 nextAndLeaveButtonsDefaultPos = new Vector2(0, -330f);
    
    private void OnEnable()
    {
        animationsEventChannel.FinishLevelPostProcessingAnimationsEnded += AnimateButtons;
    }

    private void OnDisable()
    {
        animationsEventChannel.FinishLevelPostProcessingAnimationsEnded -= AnimateButtons;
    }
    
    private void AnimateButtons()
    {
        nextLevelButton.SetActive(!levelsManagerData.HasReachedLastLevel);
        
        nextAndLeaveButtonsParent.gameObject.SetActive(true);
        DOTween.Sequence().Append(nextAndLeaveButtonsParent.DOAnchorPos(new Vector2(0, 386f), 0.5f).SetEase(Ease.InOutSine)).PrependInterval(0.3f);
    }

    public void GoToNextLevel()
    {
        audioEventsChannel.OnPlayAudioClip(clickAudioClip);
        void OnFadeInCompleted()
        {
            uiEventsChannel.FadeInCompleted -= OnFadeInCompleted;

            nextAndLeaveButtonsParent.anchoredPosition = nextAndLeaveButtonsDefaultPos;
            nextAndLeaveButtonsParent.gameObject.SetActive(false);
            
            levelEventsChannel.OnRequestToLoadNextLevel();
        }
        uiEventsChannel.FadeInCompleted += OnFadeInCompleted;
        
        uiEventsChannel.OnFadeInStarted();
    }

    public void ReturnToMenu()
    {
        //subscribing and unsubscribing directly
        audioEventsChannel.OnPlayAudioClip(clickAudioClip);
        void OnFadeInCompleted()
        {
            uiEventsChannel.FadeInCompleted -= OnFadeInCompleted;
            SceneManager.LoadScene("MainMenu");
        }
        uiEventsChannel.FadeInCompleted += OnFadeInCompleted;
        
        
        uiEventsChannel.OnFadeInStarted();
    }
}
