using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        playerData.SaveData();

    }
    public void Load()
    {
        playerData.LoadData();
        experienceText.text = playerData.GetExperience().ToString();
        healthText.text = playerData.GetHeath().ToString();
    }
}
