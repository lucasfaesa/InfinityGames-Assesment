using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticlesController : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;
    [SerializeField] private AudioEventsChannel audioEventsChannel;
    [Header("Components")]
    [SerializeField] private ParticleSystem winParticleSystem;
    [Header("Audio")] 
    [SerializeField] private AudioClipSO partyHornAudioClip;
    
    private void OnEnable()
    {
        levelEventsChannel.LevelFinished += OnLevelFinished;
    }

    private void OnDisable()
    {
        levelEventsChannel.LevelFinished -= OnLevelFinished;
    }

    private void OnLevelFinished()
    {
        if (!levelsManagerData.HasReachedLastLevel)
            return;
        
        winParticleSystem.Play();
        StartCoroutine(PlayDelayed());
    }

    private IEnumerator PlayDelayed()
    {
        yield return new WaitForSeconds(1f);
        audioEventsChannel.OnPlayAudioClip(partyHornAudioClip);
    }
}
