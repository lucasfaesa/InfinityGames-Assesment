using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsManagerData", menuName = "ScriptableObjects/Level/LevelsManagerData")]
public class LevelManagerDataSO : ScriptableObject
{
    [field: SerializeField] public int CurrentLevel { get; set; } = 0;
    [field:Space] 
    [field:SerializeField] public List<Level> LevelsPrefabList { get; set; } = new();
    
    public Level GetCurrentLevel()
    {
        return LevelsPrefabList[CurrentLevel];
    }
}
