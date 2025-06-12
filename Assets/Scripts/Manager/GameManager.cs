using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    internal object PlayerHealth;
    [SerializeField] private Player player;

    public Player Player => player; // Public property to access the player instance


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            player.ResetStats(); // Call the ResetStats method on the player instance
        }
    }

}
