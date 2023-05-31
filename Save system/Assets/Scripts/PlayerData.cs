using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.CloudSave;
using Unity.Services.Core;
using UnityEngine;
using static UnityEditor.Progress;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int heath;
    [SerializeField] private int experience;
    private static string JSON_FILE_PATCH;
    private static string BINARY_FILE_PATCH;
    private ICloudSaveDataClient client;

    async void Awake()
    {
        await UnityServices.InitializeAsync();
        await AuthenticationService.Instance.SignInAnonymouslyAsync();
        client = CloudSaveService.Instance.Data;
        //Debug.Log(UnityServices.State);
    }

    private void Start()
    {
        JSON_FILE_PATCH = Application.dataPath + "/save/save.json";
        BINARY_FILE_PATCH = Application.dataPath + "/save/save.xyz";
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
    public void SaveDataJson()
    {
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.healthToSave = heath;
        saveLoadData.experienceToSave = experience;
        File.WriteAllText(JSON_FILE_PATCH, JsonUtility.ToJson(saveLoadData));

    }
    public void LoadDataJson()
    {
        SaveLoadData saveLoadData = JsonUtility.FromJson<SaveLoadData>(File.ReadAllText(JSON_FILE_PATCH));
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
    }

    public void LoadData()
    {
        if (File.Exists(JSON_FILE_PATCH))
        {
            LoadDataJson();
        }
        else if(File.Exists(BINARY_FILE_PATCH))
        {
            LoadDataBinary();
        }
        else
        {
            Debug.LogError("No save file announcement");
        }
    }

    public void LoadDataBinary()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(BINARY_FILE_PATCH, FileMode.Open);
        SaveLoadData saveLoadData = binaryFormatter.Deserialize(fileStream) as SaveLoadData;
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
        fileStream.Close();
    }

    public async void SaveDataCloud()
    {
        SaveLoadData saveLoadData = new SaveLoadData();
        saveLoadData.healthToSave = heath;
        saveLoadData.experienceToSave = experience;
        Dictionary<string, object> data = new Dictionary<string, object> { {"saved_player", saveLoadData } };
        await client.ForceSaveAsync(data);
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
    }

    public async Task LoadDataCloud()
    {
        var query = await client.LoadAsync(new HashSet<string> {"saved_player"});
        var stringData = query["saved_player"];
        SaveLoadData saveLoadData = JsonConvert.DeserializeObject<SaveLoadData>(stringData);
        heath = saveLoadData.healthToSave;
        experience = saveLoadData.experienceToSave;
    }


    [System.Serializable]
    private class SaveLoadData
    {
        public int healthToSave = 100;
        public int experienceToSave = 50;
    }

    /*public void SaveDataBinary()
{
    BinaryFormatter binaryFormatter = new BinaryFormatter();
    SaveLoadData saveLoadData = new SaveLoadData();
    saveLoadData.healthToSave = heath;
    saveLoadData.experienceToSave = experience;
    FileStream fileStream = new FileStream(BINARY_FILE_PATCH, FileMode.Create);
    binaryFormatter.Serialize(fileStream, saveLoadData);
    fileStream.Close();
}*/
}
