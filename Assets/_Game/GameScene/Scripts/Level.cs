using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private LevelSettingsSO levelSettingsSo;

    private List<NodeBehavior> _levelNodes = new();
    
    private void Awake()
    {
        _levelNodes = GetComponentsInChildren<NodeBehavior>().ToList();
        
        if(levelSettingsSo.RandomizeNodesRotationOnStart)
            _levelNodes.ForEach(x=>x.RandomizeRotationAtStart());
    }

    //TODO remove later
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        
        
    }
}
