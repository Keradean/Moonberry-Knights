using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMana : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private PlayerStats stats; // Reference to PlayerStats script


    public float CurrentMana { get; private set; }

    private void Start()
    {
        ResetMana(); // Reset mana at the start of the game
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // For testing purposes, press 'L' to use mana
        {
            UseMana(5f); // Call UseMana with a test amount of 5
        }
    }

    public void UseMana(float amount)
    {
        if (stats.Mana >= amount)
        {
            stats.Mana = Mathf.Max(stats.Mana -= amount, 0f); // Deduct mana but ensure it doesn't go below zero
            CurrentMana = stats.Mana; // Update CurrentMana to reflect the new mana value
        }

    }

    public void RecoverMana(float amount)
    {
        stats.Mana += amount; // Increase mana by the specified amount
        stats.Mana = Mathf.Min(stats.Mana, stats.MaxMana); // Ensure mana does not exceed maximum mana
    }
    public bool CanRecoverMana()
    {
        return stats.Mana >0 && stats.Mana < stats.MaxMana; // Check if mana can be recovered
    }

    public void ResetMana()
    {
        CurrentMana = stats.MaxMana; // Reset CurrentMana to maximum mana
    }
}
