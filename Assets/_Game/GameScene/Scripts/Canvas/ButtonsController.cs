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

    [Header("Components")] 
    [SerializeField] private RectTransform nextAndLeaveButtonsParent;
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
        nextAndLeaveButtonsParent.gameObject.SetActive(true);
        DOTween.Sequence().Append(nextAndLeaveButtonsParent.DOAnchorPos(new Vector2(0, 386f), 0.5f).SetEase(Ease.InOutSine)).PrependInterval(0.3f);
    }

    public void GoToNextLevel()
    {
        
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
