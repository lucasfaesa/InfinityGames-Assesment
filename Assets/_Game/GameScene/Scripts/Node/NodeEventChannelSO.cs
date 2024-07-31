using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NodeEventChannel", menuName = "ScriptableObjects/Node/NodeEventChannel")]
public class NodeEventChannelSO : ScriptableObject
{
    public event Action<bool, NodeBehavior> NodeConnectionStatusChanged;

    public void OnNodeConnectionStatusChanged(bool status, NodeBehavior node)
    {
        NodeConnectionStatusChanged?.Invoke(status, node);
    }
}
