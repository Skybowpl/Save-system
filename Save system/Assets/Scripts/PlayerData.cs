using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int heath;
    [SerializeField] private int experience;
    private static string FILE_PATCH;

    private void Awake()
    {
        FILE_PATCH = Application.dataPath + "/save.json";
    }

    public void IncrementHealth()
    {
        heath += 1;
    }

    public void DecrementHealth()
    {
        heath -= 1;
    }

    public void IncrementExperience()
    {
        experience += 1;
    }

    public void DecrementExperience()
    {
        experience -= 1;
    }

    public int GetHeath()
    {
        return heath;
    }
    public int GetExperience()
    {
        return experience;
    }
    public void SaveData()
    {
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.healthToSave = heath;
        saveLoadData.experienceToSave = experience;
        File.WriteAllText(FILE_PATCH, JsonUtility.ToJson(saveLoadData));

    }
    public void SaveDataBinary()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.healthToSave = heath;
        saveLoadData.experienceToSave = experience;
        FileStream fileStream = new FileStream(Application.dataPath + "/save.xyz",FileMode.Create);
        binaryFormatter.Serialize(fileStream, saveLoadData);
        fileStream.Close();
    }
    public void LoadData()
    {
        SaveLoadData saveLoadData = JsonUtility.FromJson<SaveLoadData>(File.ReadAllText(FILE_PATCH));
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
    }

    public void LoadDataBinary()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(Application.dataPath + "/save.xyz", FileMode.Open);
        SaveLoadData saveLoadData = binaryFormatter.Deserialize(fileStream) as SaveLoadData;
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
    }

    [System.Serializable]
    private class SaveLoadData
    {
        public int healthToSave;
        public int experienceToSave;
    }

}
