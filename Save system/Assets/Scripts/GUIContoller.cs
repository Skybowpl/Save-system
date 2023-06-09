using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GUIContoller : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private TextMeshProUGUI experienceText;
    [SerializeField] private TextMeshProUGUI healthText;
    void Start()
    {
        experienceText.text = playerData.GetExperience().ToString();
        healthText.text = playerData.GetHeath().ToString();
    }

    public void IncreaseHeath()
    {
        playerData.IncrementHealth();
        healthText.text = playerData.GetHeath().ToString();
    }
    public void DecreaseHeath()
    {
        playerData.DecrementHealth();
        healthText.text = playerData.GetHeath().ToString();
    }

    public void IncreaseExperience()
    {
        playerData.IncrementExperience();
        experienceText.text = playerData.GetExperience().ToString();
    }
    public void DecreaseExperience()
    {
        playerData.DecrementExperience();
        experienceText.text = playerData.GetExperience().ToString();
    }
    public void Save()
    {
        playerData.SaveDataJson();
    }
    public void CloudSave()
    {
        playerData.SaveDataCloud();
    }
    public void Load()
    {
        playerData.LoadData();
        experienceText.text = playerData.GetExperience().ToString();
        healthText.text = playerData.GetHeath().ToString();
    }
    public async void CloudLoad()
    {
        await playerData.LoadDataCloud();
        experienceText.text = playerData.GetExperience().ToString();
        healthText.text = playerData.GetHeath().ToString();
    }

}
