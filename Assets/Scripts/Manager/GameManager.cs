using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance; // Singleton instance of GameManager
    internal object PlayerHealth;
    [SerializeField] private Player player;

    public Player Player => player; // Public property to access the player instance

    public void Awake()
    {
        Instance = this; // Set the singleton instance
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            player.ResetStats(); // Call the ResetStats method on the player instance
        }
    }

}
