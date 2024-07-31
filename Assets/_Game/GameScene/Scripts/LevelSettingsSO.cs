using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "ScriptableObjects/Level/LevelSettings")]
public class LevelSettingsSO : ScriptableObject
{
    [field:SerializeField] public bool RandomizeNodesRotationOnStart { get; set; }
}
