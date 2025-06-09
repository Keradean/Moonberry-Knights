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


        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) // For testing purposes, press 'L' to use mana
        {
            UseMana(5f); // Call UseMana with a test amount of 5
        }
    }

}
