using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameEventsChannelSO gameEventsChannel;
    

    IEnumerator Start()
    {
        gameEventsChannel.OnGamePreparingToStart();
        
        yield return new WaitForSeconds(2f);
        
        gameEventsChannel.OnGameStarted();    
    }

}
