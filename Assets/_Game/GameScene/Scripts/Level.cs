using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{

    [SerializeField] private bool randomizeNodesOnAwake;
    
    private void Awake()
    {
        if (randomizeNodesOnAwake)
        {
            var list = GetComponentsInChildren<NodeBehavior>();

            foreach (var nodeBehavior in list)
            {
                nodeBehavior.RandomizeAtStart = true;
            }
        }
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        
        var list = GetComponentsInChildren<NodeBehavior>();

        foreach (var nodeBehavior in list)
        {
            nodeBehavior.GameStarted = true;
        }
    }
}
