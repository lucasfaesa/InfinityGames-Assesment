using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MainMenuManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private UIEventsChannelSO uiEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;

    [Header("Components")] 
    [SerializeField] private Light2D spotlight2D;
    [SerializeField] private List<LevelButtonStructure> levelButtonStructures = new(5);

    private void Start()
    {
        int lastLevelReached = levelsManagerData.LastLevelReached;
        levelsManagerData.CurrentLevel = lastLevelReached;
        
        spotlight2D.transform.localPosition = levelButtonStructures[lastLevelReached].spotlightPos;
        
        for (int i = 1; i < levelButtonStructures.Count; i++)
        {
            if (i <= lastLevelReached)
            {
                levelButtonStructures[i].lockIcon.SetActive(false);
                levelButtonStructures[i].textNumber.gameObject.SetActive(true);
            }
            else
            {
                levelButtonStructures[i].lockIcon.SetActive(true);
                levelButtonStructures[i].textNumber.gameObject.SetActive(false);
            }
        }

        FlickLight();
    }

    public void GoToGameScene()
    {
        void DoFadeIn()
        {
            uiEventsChannel.FadeInCompleted -= DoFadeIn;
            SceneManager.LoadScene("GameScene");
        }

        uiEventsChannel.FadeInCompleted += DoFadeIn;
        
        uiEventsChannel.OnFadeInStarted();

    }

    public void OnLevelClicked(int index)
    {
        if (index > levelsManagerData.LastLevelReached) return;
        
        spotlight2D.transform.localPosition = levelButtonStructures[index].spotlightPos;
        
        levelsManagerData.CurrentLevel = index;
    }
    
    private void FlickLight()
    {
        float randomIntensity = Random.Range(0.045f, 0.09f);
        DOTween.To(x => spotlight2D.volumeIntensity = x, spotlight2D.volumeIntensity, randomIntensity, 0.2f)
            .OnComplete(FlickLight);
    }


    [Serializable]
    public struct LevelButtonStructure
    {
        public TextMeshProUGUI textNumber;
        public GameObject lockIcon;
        public Vector2 spotlightPos;
    }
}
