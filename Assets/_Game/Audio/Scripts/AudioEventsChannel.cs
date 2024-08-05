using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioEventsChannel", menuName = "ScriptableObjects/Audio/AudioEventsChannel")]
public class AudioEventsChannel : ScriptableObject
{
    public event Action<AudioClipSO> PlayAudioClip;

    public void OnPlayAudioClip(AudioClipSO audioClipSo)
    {
        PlayAudioClip?.Invoke(audioClipSo);
    }
}
