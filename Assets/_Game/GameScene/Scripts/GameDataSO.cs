using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "ScriptableObjects/Game/GameData")]
public class GameDataSO : ScriptableObject
{
    [field: SerializeField] public int CurrentLevel { get; set; } = 0;
    [field:Space] 
    [field:SerializeField] public List<Level> LevelsPrefabList { get; set; } = new();
    
    public event Action GamePreparingToStart;
    public event Action GameStarted;
    public event Action GamePreparingToEnd;
    public event Action GameEnded;

    public Level GetCurrentLevel()
    {
        return LevelsPrefabList[CurrentLevel];
    }
    
    public void OnGamePreparingToStart()
    {
        GamePreparingToStart?.Invoke();
    }

    public void OnGameStarted()
    {
        Debug.Log("Game Started");
        GameStarted?.Invoke();
    }

    public void OnGamePreparingToEnd()
    {
        GamePreparingToEnd?.Invoke();
    }

    public void OnGameEnded()
    {
        GameEnded?.Invoke();
    }
    
}
