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
    [Header("Components")] 
    [SerializeField] private Transform nodeTransform;
    [SerializeField] private SpriteRenderer nodeSprite;
    [Space]
    [SerializeField] private GameObject endPointCollidersParent;
    
    private bool _animating;
    private bool _nodeConnected;
    
    private readonly Color32 _notConnectedColor = new Color32(123,123,123,255);
    private readonly Color32 _connectedColor = new Color32(255,255,255,255);

    private List<EndPointBehavior> _endPointBehaviors = new();
    
    public bool RandomizeAtStart { get; set; }
    public bool GameStarted { get; set; }
    
    private void Awake()
    {
        _endPointBehaviors.AddRange(endPointCollidersParent.GetComponentsInChildren<EndPointBehavior>());
        
        if(_endPointBehaviors.Count == 0)
            Debug.LogError("Node without end point behavior component");
    }

    private void OnEnable()
    {
        SubscribeToEndPointColliderTriggerEvents();
        Debug.Log($"Subscribed: {this.gameObject.name}");
    }

    private void OnDisable()
    {
        UnsubscribeToEndPointColliderTriggerEvents();
    }

    void Start()
    {
        nodeSprite.color = _notConnectedColor;
        
        //if(RandomizeAtStart)
        //    RandomizeRotation();
    }

    public void RandomizeRotation()
    {
        int randomNumber = Random.Range(0, 4); //4 exclusive, so 0 to 3
        int rotationAmount = randomNumber * 90;

        var nodeLocalRotation = nodeTransform.localRotation;

        nodeTransform.localRotation = Quaternion.Euler(
            new Vector3(nodeLocalRotation.x,
                nodeLocalRotation.y,
                nodeLocalRotation.z + rotationAmount));
        
        Debug.Log($"Randomized: {this.gameObject.name}");
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (_animating) return;
        
        _animating = true;
        var currentLocalRotation = nodeTransform.localRotation.eulerAngles;
        var newRotation = new Vector3(currentLocalRotation.x, currentLocalRotation.y, currentLocalRotation.z + 90f);
        
        nodeTransform.DOLocalRotate(newRotation, 0.4f, RotateMode.FastBeyond360).SetEase(Ease.OutBack)
                .OnComplete(() =>
                {
                    _animating = false;
                });
        
    }

    public void OnEndpointConnectionChanged(bool _, EndPointBehavior __)
    {
        if (_endPointBehaviors.TrueForAll(x => x.IsNodeConnected))
        {
            OnNodeConnected();
        }
        else
        {

            OnNodeDisconnected();
        }
    }
    
    public void OnNodeConnected()
    {
        if (!GameStarted)
        {
            RandomizeRotation();
            return;
        }

        if (_nodeConnected) return;
        
        _nodeConnected = true;
        nodeSprite.DOColor(_connectedColor, 0.5f).SetEase(Ease.InOutSine);
        Debug.Log($"Node Connected: {this.gameObject.name}");
    }

    public void OnNodeDisconnected()
    {
        if (!_nodeConnected) return;
        
        _nodeConnected = false;
        nodeSprite.DOColor(_notConnectedColor, 0.5f).SetEase(Ease.InOutSine);
    }
    
    private void SubscribeToEndPointColliderTriggerEvents()
    {
        foreach (var endPointColliderBehavior in _endPointBehaviors)
        {
            endPointColliderBehavior.endPointConnected += OnEndpointConnectionChanged;
        }
    }
    
    private void UnsubscribeToEndPointColliderTriggerEvents()
    {
        foreach (var endPointColliderBehavior in _endPointBehaviors)
        {
            endPointColliderBehavior.endPointConnected -= OnEndpointConnectionChanged;
        }
    }
}
