using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // Reference to PlayerStats script

    public void UseMana(float amount)
    {
        if (stats.Mana >= amount)
        {
            stats.Mana = Mathf.Max(stats.Mana -= amount, 0f); // Deduct mana but ensure it doesn't go below zero
            CurrentMana = stats.Mana; // Update CurrentMana to reflect the new mana value
        }

        }

    }
    public bool CanRecoverMana()
        {
        return stats.Mana >0 && stats.Mana < stats.MaxMana; // Check if mana can be recovered
    }

}
