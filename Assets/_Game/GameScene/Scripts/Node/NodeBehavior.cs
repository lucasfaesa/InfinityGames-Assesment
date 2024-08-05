using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class NodeBehavior : MonoBehaviour, IPointerClickHandler
{
    [Header("SOs")] 
    [SerializeField] private NodeEventChannelSO nodeEventChannel;
    [SerializeField] private LevelEventsChannelSO levelEventsChannel;
    [SerializeField] private AudioEventsChannel audioEventsChannel;
    [Header("Components")] 
    [SerializeField] private Transform nodeTransform;
    [SerializeField] private SpriteRenderer nodeSprite;
    [Space]
    [SerializeField] private GameObject endPointCollidersParent;
    [Space] 
    [SerializeField] private Light2D nodeLight;
    [Header("Audio")] 
    [SerializeField] private AudioClipSO switchAudioClip;
    [SerializeField] private AudioClipSO connectAudioClip;
    
    private bool _animating;
    private bool _nodeConnected;
    private bool _randomizeRotationAtStart;
    private bool _gameStarted;
    private bool _randomized;
    
    private float _minLightIntensity = 0.36f;
    private float _maxLightIntensity = 0.7f;
    
    private readonly Color32 _notConnectedColor = new Color32(123,123,123,255);
    private readonly Color32 _connectedColor = new Color32(255,255,255,255);

    private List<EndPointBehavior> _endPointBehaviors = new();
    
    
    private void Awake()
    {
        _endPointBehaviors.AddRange(endPointCollidersParent.GetComponentsInChildren<EndPointBehavior>());
        
        if(_endPointBehaviors.Count == 0)
            Debug.LogError("Node without end point behavior component");
    }

    private void OnEnable()
    {
        levelEventsChannel.LevelFinished += OnLevelFinished;
        SubscribeToEndPointColliderTriggerEvents();
    }

    private void OnDisable()
    {
        levelEventsChannel.LevelFinished -= OnLevelFinished;
        UnsubscribeToEndPointColliderTriggerEvents();
    }

    public void RandomizeRotationAtStart()
    {
        _randomizeRotationAtStart = true;
    }
    
    void Start()
    {
        nodeSprite.color = _notConnectedColor;
        
        if(_randomizeRotationAtStart)
            RandomizeRotation();
    }

    public void OnGameStarted()
    {
        _gameStarted = true;
    }
    
    private void RandomizeRotation()
    {
        int randomNumber = Random.Range(1, 4); //4 exclusive, so 1 to 3
        int rotationAmount = randomNumber * 90;
        
        RotateNode(rotationAmount, 0.1f, () =>
        {
            _randomized = true;
        });
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        RotateNode();
    }

    private void RotateNode(float rotateAmount = 90f, float duration = 0.4f, Action rotationCompleted = null)
    {
        if (_animating) return;
        
        if(_randomized)
            audioEventsChannel.OnPlayAudioClip(switchAudioClip);
        
        _animating = true;
        var currentLocalRotation = nodeTransform.localRotation.eulerAngles;
        var newRotation = new Vector3(currentLocalRotation.x, currentLocalRotation.y, currentLocalRotation.z + rotateAmount);
        
        nodeTransform.DOLocalRotate(newRotation, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack, 1.3f)
            .OnComplete(() =>
            {
                _animating = false;
                rotationCompleted?.Invoke();
            });
    }

    private void OnEndpointConnectionChanged(bool _, EndPointBehavior __)
    {
        if (_endPointBehaviors.TrueForAll(x => x.IsNodeConnected))
            OnNodeConnected();
        else
            OnNodeDisconnected();
    }
    
    private void OnNodeConnected()
    {
        if (!_randomized || _nodeConnected) return;
        
        _nodeConnected = true;
        audioEventsChannel.OnPlayAudioClip(connectAudioClip);
        nodeSprite.DOColor(_connectedColor, 0.5f).SetEase(Ease.InOutSine);
        nodeEventChannel.OnNodeConnectionStatusChanged(true, this);
    }

    private void OnNodeDisconnected()
    {
        if (!_randomized || !_nodeConnected) return;
        
        _nodeConnected = false;
        
        nodeSprite.DOColor(_notConnectedColor, 0.5f).SetEase(Ease.InOutSine);
        nodeEventChannel.OnNodeConnectionStatusChanged(false, this);
    }

    public bool GetConnectionStatus()
    {
        return _nodeConnected;
    }

    private void OnLevelFinished()
    {
        if (nodeLight)
        {
            nodeLight.enabled = true;
            FlickLight();
        }
    }

    private void FlickLight()
    {
        float randomIntensity = Random.Range(_minLightIntensity, _maxLightIntensity);
        DOTween.To(x => nodeLight.intensity = x, nodeLight.intensity, randomIntensity, 0.2f)
            .OnComplete(FlickLight);
    }
    
    private void SubscribeToEndPointColliderTriggerEvents()
    {
        foreach (var endPointColliderBehavior in _endPointBehaviors)
        {
            endPointColliderBehavior.EndPointConnected += OnEndpointConnectionChanged;
        }
    }
    
    private void UnsubscribeToEndPointColliderTriggerEvents()
    {
        foreach (var endPointColliderBehavior in _endPointBehaviors)
        {
            endPointColliderBehavior.EndPointConnected -= OnEndpointConnectionChanged;
        }
    }
}
