using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private UIEventsChannelSO uiEventsChannel;

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
}
