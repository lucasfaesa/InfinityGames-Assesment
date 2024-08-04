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

    [field:SerializeField] public int LastLevelReached { get; set; } = 3;
    
    public bool HasReachedLastLevel { get; set; }
    
    public Level GetCurrentLevel()
    {
        return LevelsPrefabList[CurrentLevel];
    }

    public bool CheckIfNextLevelExists()
    {
        return CurrentLevel + 1 != LevelsPrefabList.Count;
    }
    
    public void IncrementLevel()
    {
        if (!CheckIfNextLevelExists())
        {
            HasReachedLastLevel = true;
            return;
        }
        
        CurrentLevel++;
        
        if(CurrentLevel > LastLevelReached)
            LastLevelReached = CurrentLevel ;
    }

    public void Reset()
    {
        HasReachedLastLevel = false;
        CurrentLevel = 0;
        LastLevelReached = 0;
        
        #if UNITY_EDITOR
            CurrentLevel = 0;
        #endif
    }

    public LevelsManagerSaveData GetSaveData()
    {
        return new LevelsManagerSaveData { CurrentLevel = this.CurrentLevel, LastLevelReached = this.LastLevelReached };
    }

    public void LoadSavedData(LevelsManagerSaveData data)
    {
        this.CurrentLevel = data.CurrentLevel;
        this.LastLevelReached = data.LastLevelReached;
    }


    public struct LevelsManagerSaveData
    {
        public int CurrentLevel { get; set; }
        public int LastLevelReached { get; set; }
    }
}
