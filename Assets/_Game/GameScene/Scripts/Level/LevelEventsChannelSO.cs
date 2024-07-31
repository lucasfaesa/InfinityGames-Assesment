using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEventsChannel", menuName = "ScriptableObjects/Level/LevelEventsChannel")]
public class LevelEventsChannelSO : ScriptableObject
{
    public event Action AllNodesConnected;

    public void OnAllNodesConnected()
    {
        AllNodesConnected?.Invoke();
    }
}
