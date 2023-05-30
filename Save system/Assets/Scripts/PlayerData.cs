using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int heath;
    [SerializeField] private int experience;

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
        SaveLoadData saveLoadData = new SaveLoadData(heath, experience);
        File.WriteAllText(Application.dataPath + "/save.json", JsonUtility.ToJson(saveLoadData));

    }
    public void LoadData()
    {
        SaveLoadData saveLoadData = JsonUtility.FromJson<SaveLoadData>(File.ReadAllText(Application.dataPath + "/save.json"));
        heath=saveLoadData.getHealth();
        experience=saveLoadData.getExperience();
    }

    private class SaveLoadData
    {
        public int healthToSave;
        public int experienceToSave;
        public SaveLoadData(int health, int experience)
        {
            healthToSave = health;
            experienceToSave = experience;
        }
        public int getHealth()
        {
            return healthToSave;
        }
        public int getExperience()
        {
            return experienceToSave;
        }

    }

}
