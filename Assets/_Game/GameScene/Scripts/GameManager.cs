using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameDataSO gameData;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2f);
        
        gameData.OnGameStarted();    
    }

}
