using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")] // Configuration settings for the Player
    [SerializeField] private PlayerStats stats; // Reference to the PlayerStats scriptable object

    [Header("Test")]
    public ItemHealthPotion HealthPotion; // Reference to an ItemHealthPotion for testing purposes
    public ItemManaPotion ManaPotion; // Reference to an ItemManaPotion for testing purposes

    public PlayerStats Stats => stats; // Public property to access PlayerStats

    public PlayerMana PlayerMana { get; private set; } // Property to access PlayerMana, which is initialized in Awake
    public PlayerHealth PlayerHealth { get; private set; } // Property to access PlayerHealth, which is initialized in Awake

    private PlayerAnimations animations; // Reference to PlayerAnimations for handling animations


    private void Awake()
    {
        PlayerMana = GetComponent<PlayerMana>(); // Get the PlayerMana component attached to this GameObject
        PlayerHealth = GetComponent<PlayerHealth>(); // Get the PlayerHealth component attached to this GameObject
        animations = GetComponent<PlayerAnimations>(); // Get the PlayerAnimations component attached to this GameObject
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) // For testing purposes, press 'P' to take damage
        {
            
            if (HealthPotion.UseItem())
            {
                Debug.Log("Health Potion used successfully!"); // Log success message
            }
            if (ManaPotion.UseItem())
            {
                Debug.Log("Mana Potion used successfully!"); // Log success message
            }
        }



    }
    public void ResetStats()
    {
        stats.ResetStats(); // Call the ResetStats method from PlayerStats to reset player stats
        animations.ResetStats(); // Reset Animation
    }





}
