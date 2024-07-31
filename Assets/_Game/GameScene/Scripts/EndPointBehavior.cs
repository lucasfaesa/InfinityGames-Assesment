using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPointBehavior : MonoBehaviour
{
    public bool IsNodeConnected { get; set; }
    
    public event Action<bool, EndPointBehavior> endPointConnected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IsNodeConnected = true;
        endPointConnected?.Invoke(true, this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsNodeConnected = false;
        endPointConnected?.Invoke(false, this);
    }

}
