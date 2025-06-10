using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")] // Configuration settings for the Player
    [SerializeField] private PlayerStats stats; // Reference to the PlayerStats scriptable object


    public PlayerStats Stats => stats; // Public property to access PlayerStats

    private PlayerAnimations animations; // Reference to PlayerAnimations for handling animations

    private void Awake()
    {
        animations = GetComponent<PlayerAnimations>(); // Get the PlayerAnimations component attached to this GameObject
    }
    public void ResetStats()
    {
        stats.ResetStats(); // Call the ResetStats method from PlayerStats to reset player stats
        animations.ResetStats(); // Reset Animation
    }





}
