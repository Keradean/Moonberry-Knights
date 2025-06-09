using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Config")] // Configuration settings for the Player
    [SerializeField] private PlayerStats stats; // Reference to the PlayerStats scriptable object


    public PlayerStats Stats => stats; // Public property to access PlayerStats





}
