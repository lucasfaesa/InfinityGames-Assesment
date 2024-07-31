using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EndPointBehavior : MonoBehaviour
{
    public bool IsNodeConnected { get; set; }
    
    public event Action<bool, EndPointBehavior> EndPointConnected;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        IsNodeConnected = true;
        EndPointConnected?.Invoke(true, this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IsNodeConnected = false;
        EndPointConnected?.Invoke(false, this);
    }

}
