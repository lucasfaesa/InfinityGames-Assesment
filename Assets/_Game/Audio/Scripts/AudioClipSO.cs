using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioClip", menuName = "ScriptableObjects/Audio/AudioClipSO")]
public class AudioClipSO : ScriptableObject
{
    [SerializeField] private AudioClip audioClip;

    [Range(0f,1f)]
    [SerializeField] private float volume = 1f;

    public (AudioClip clip, float volume) GetClipAndVolume => (audioClip, volume);
}
