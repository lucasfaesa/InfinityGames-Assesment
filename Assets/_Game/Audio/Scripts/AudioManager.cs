using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [Header("SOs")] 
    [SerializeField] private AudioEventsChannel audioEventsChannel;

    [Header("Components")] 
    [SerializeField] private AudioSource bgmAudioSource;
    
    private List<AudioSource> _audioSourcePool = new();

    public static AudioManager _instance;

    private void Awake()
    {
        if (!_instance)
        {
            _instance = this;
            return;
        }
        if(_instance != this)
            Destroy(this.gameObject);
        
    }

    private void OnEnable()
    {
        audioEventsChannel.PlayAudioClip += OnPlayAudioClip;
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        DontDestroyOnLoad(this);
    }

    private void OnDisable()
    {
        SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        audioEventsChannel.PlayAudioClip -= OnPlayAudioClip;
    }
    
    private void OnPlayAudioClip(AudioClipSO audioClipSo)
    {
        (AudioClip audioClip, float volume) tuple = audioClipSo.GetClipAndVolume;
        int poolCount = _audioSourcePool.Count;

        for (int i = 0; i < poolCount; i++)
        {
            if (!_audioSourcePool[i].isPlaying)
            {
                _audioSourcePool[i].volume = tuple.volume;
                _audioSourcePool[i].PlayOneShot(tuple.audioClip);
                return;
            }
        }
        
        InstantiateAudioSource();
        _audioSourcePool[^1].volume = tuple.volume;
        _audioSourcePool[^1].PlayOneShot(tuple.audioClip);
    }

    private void InstantiateAudioSource()
    {
        var audioSource = Instantiate(new GameObject(),this.transform).AddComponent<AudioSource>();
        _audioSourcePool.Add(audioSource);
    }

    private void OnActiveSceneChanged(Scene arg0, Scene scene)
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            bgmAudioSource.time = 0f;
        }
    }
    
}
