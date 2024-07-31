using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsManagerData", menuName = "ScriptableObjects/Level/LevelsManagerData")]
public class LevelsManagerDataSO : ScriptableObject
{
    [field: SerializeField] public int CurrentLevel { get; set; } = 0;
    [field:Space] 
    [field:SerializeField] public List<Level> LevelsPrefabList { get; set; } = new();

    private bool _reachedLastLevel;
    
    public Level GetCurrentLevel()
    {
        return LevelsPrefabList[CurrentLevel];
    }

    public bool CheckIfNextLevelExists()
    {
        return CurrentLevel != LevelsPrefabList.Count - 1;
    }
    
    public void IncrementLevel()
    {
        if (CurrentLevel > LevelsPrefabList.Count - 1)
            return;
        
        CurrentLevel++;
    }

    public void Reset()
    {
        _reachedLastLevel = false;
    }
}
