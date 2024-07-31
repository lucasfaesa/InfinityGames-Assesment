using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class NodeBehavior : MonoBehaviour, IPointerClickHandler
{
    [Header("Components")] 
    [SerializeField] private Transform nodeTransform;
    [SerializeField] private SpriteRenderer nodeSprite;

    private bool _animating;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
