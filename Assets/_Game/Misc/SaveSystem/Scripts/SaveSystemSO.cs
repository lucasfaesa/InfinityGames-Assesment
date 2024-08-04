using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;


[CreateAssetMenu(fileName = "SaveSystem", menuName = "ScriptableObjects/Save/SaveSystem")]
public class SaveSystemSO : ScriptableObject
{
    [field:SerializeField] private LevelsManagerDataSO levelsManagerData;
    
    public void SaveLevelsManagerData()
    { 
        string saveFilePath = Path.Combine(Application.persistentDataPath, "LevelsManagerData.json");

        var data = JsonConvert.SerializeObject(levelsManagerData.GetSaveData());
        
        using FileStream fileStream = new FileStream(saveFilePath, FileMode.Create);
        using (BinaryWriter writer = new BinaryWriter(fileStream))
        {
            writer.Write(data);
        }
        
        fileStream.Close();
    }
    
    public void LoadLevelsManagerData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "LevelsManagerData.json");
        
        if (File.Exists(saveFilePath))
        {
            using FileStream fileStream = new FileStream(saveFilePath, FileMode.Open);
            
            using (BinaryReader  reader = new BinaryReader (fileStream))
            {
                var data = JsonConvert.DeserializeObject<LevelsManagerDataSO.LevelsManagerSaveData>(reader.ReadString());

                levelsManagerData.LoadSavedData(data);
            }
            
            fileStream.Close();
        }
    }

    public void DeleteLevelsManagerData()
    {
        string saveFilePath = Path.Combine(Application.persistentDataPath, "LevelsManagerData.json");

        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
        
        levelsManagerData.Reset();
    }
}
