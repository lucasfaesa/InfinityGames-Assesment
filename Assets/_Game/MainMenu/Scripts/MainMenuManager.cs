using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class MainMenuManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private UIEventsChannelSO uiEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;
    [SerializeField] private SaveSystemSO saveSystem;
    [SerializeField] private AudioEventsChannel audioEventsChannel;
    [Header("Components")] 
    [SerializeField] private Light2D spotlight2D;
    [SerializeField] private List<LevelButtonStructure> levelButtonStructures = new(5);
    [Header("Audios")] 
    [SerializeField] private AudioClipSO clickSoundEffect;

    [Header("ShowUp Components")] 
    [SerializeField] private List<GameObject> componentsToShowUp;
    [SerializeField] private ParticleSystem nodesParticlesEffect;

    private void Awake()
    {
        componentsToShowUp.ForEach(x=>x.SetActive(false));
    }

    private IEnumerator Start()
    {
        saveSystem.LoadLevelsManagerData();
        
        int lastLevelReached = levelsManagerData.LastLevelReached;

        yield return new WaitForSeconds(1.6f);
        componentsToShowUp.ForEach(x=>x.SetActive(true));
        nodesParticlesEffect.Play();
        
        spotlight2D.transform.localPosition = levelButtonStructures[levelsManagerData.CurrentLevel].spotlightPos;
        spotlight2D.gameObject.SetActive(true);
        
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
        audioEventsChannel.OnPlayAudioClip(clickSoundEffect);
        
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
        
        audioEventsChannel.OnPlayAudioClip(clickSoundEffect);
        spotlight2D.transform.localPosition = levelButtonStructures[index].spotlightPos;
        
        levelsManagerData.CurrentLevel = index;
    }
    
    private void FlickLight()
    {
        float randomIntensity = Random.Range(0.045f, 0.09f);
        DOTween.To(x => spotlight2D.volumeIntensity = x, spotlight2D.volumeIntensity, randomIntensity, 0.2f)
            .OnComplete(FlickLight);
    }

    public void ClearSave()
    {
        audioEventsChannel.OnPlayAudioClip(clickSoundEffect);
        
        void DoFadeIn()
        {
            uiEventsChannel.FadeInCompleted -= DoFadeIn;
            saveSystem.DeleteLevelsManagerData();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        uiEventsChannel.FadeInCompleted += DoFadeIn;
        
        uiEventsChannel.OnFadeInStarted();
    }


    [Serializable]
    public struct LevelButtonStructure
    {
        public TextMeshProUGUI textNumber;
        public GameObject lockIcon;
        public Vector2 spotlightPos;
    }
}
