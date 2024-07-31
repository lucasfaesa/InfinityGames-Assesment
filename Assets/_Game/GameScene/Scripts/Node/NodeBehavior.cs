using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class NodeBehavior : MonoBehaviour, IPointerClickHandler
{
    [Header("SOs")] 
    [SerializeField] private NodeEventChannelSO nodeEventChannel;
    [Header("Components")] 
    [SerializeField] private Transform nodeTransform;
    [SerializeField] private SpriteRenderer nodeSprite;
    [Space]
    [SerializeField] private GameObject endPointCollidersParent;
    
    private bool _animating;
    private bool _nodeConnected;
    private bool _randomizeRotationAtStart;
    private bool _gameStarted;
    
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
        SubscribeToEndPointColliderTriggerEvents();
    }

    private void OnDisable()
    {
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
        int randomNumber = Random.Range(0, 4); //4 exclusive, so 0 to 3
        int rotationAmount = randomNumber * 90;

        RotateNode(rotationAmount, 0.2f);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        RotateNode();
    }

    private void RotateNode(float rotateAmount = 90f, float duration = 0.4f)
    {
        if (_animating) return;
        
        _animating = true;
        var currentLocalRotation = nodeTransform.localRotation.eulerAngles;
        var newRotation = new Vector3(currentLocalRotation.x, currentLocalRotation.y, currentLocalRotation.z + rotateAmount);
        
        nodeTransform.DOLocalRotate(newRotation, duration, RotateMode.FastBeyond360).SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                _animating = false;
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
        if (!_gameStarted)
        {
            RandomizeRotation();
            return;
        }

        if (_nodeConnected) return;
        
        _nodeConnected = true;
        nodeSprite.DOColor(_connectedColor, 0.5f).SetEase(Ease.InOutSine);
        nodeEventChannel.OnNodeConnectionStatusChanged(true, this);
    }

    private void OnNodeDisconnected()
    {
        if (!_nodeConnected) return;
        
        _nodeConnected = false;
        nodeSprite.DOColor(_notConnectedColor, 0.5f).SetEase(Ease.InOutSine);
        nodeEventChannel.OnNodeConnectionStatusChanged(false, this);
    }

    public bool GetConnectionStatus()
    {
        return _nodeConnected;
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
