using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player Stats")] // Create a new PlayerStats asset

public class PlayerStats : ScriptableObject
{
    [Header("Configuration")]
    public int Level; // Player level

    [Header("Health")]
    public float Health; // Player health
    public float MaxHealth; // Maximum player health
    
    [Header("Mana")]
    public float Mana; // Player mana
    public float MaxMana; // Maximum player mana

    public void ResetStats()
    {
        Health = MaxHealth; // Reset health to maximum health
        Mana = MaxMana; // Reset mana to maximum mana
    }
}
