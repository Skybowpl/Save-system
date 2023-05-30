using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField] private int heath;
    [SerializeField] private float experience;

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
    public float GetExperience()
    {
        return experience;
    }
}
