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


    [Header("EXP")]
    public float CurrentExp; // Current experience points
    public float NextLevelExp; // Experience points required for the next level
    public float InitialNextLevelExp; // Initial experience points required for the next level
    [Range (1f, 100f)]public float ExpMultiplier; // Multiplier for experience points

    public void ResetStats()
    {
        Health = MaxHealth; // Reset health to maximum health
        Mana = MaxMana; // Reset mana to maximum mana
        CurrentExp = 0f; // Reset current experience points to zero
        NextLevelExp = InitialNextLevelExp; // Reset next level experience points to initial value
    }
}
