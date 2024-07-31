using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NodeEventChannelSO : ScriptableObject
{
    public event Action<bool, NodeBehavior> NodeConnectionStatusChanged;

    public void OnNodeConnectionStatusChanged(bool status, NodeBehavior node)
    {
        NodeConnectionStatusChanged?.Invoke(status, node);
    }
}
