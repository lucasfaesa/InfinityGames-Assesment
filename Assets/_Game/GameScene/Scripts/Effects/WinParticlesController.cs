using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinParticlesController : MonoBehaviour
{
    [Header("SOs")]
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private LevelsManagerDataSO levelsManagerData;
    [Header("Components")]
    [SerializeField] private ParticleSystem winParticleSystem;

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
    }
}
