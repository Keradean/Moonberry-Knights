using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Vector2 = UnityEngine.Vector2; // Use Unity's Vector2 for 2D vector operations
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour 
{
    [Header("Config")] // Configuration settings for the Player
    [SerializeField] private float moveSpeed; // Speed of the player movement


    private PlayerAnimations playerAnimations; // Reference to the PlayerAnimations script for handling animations
    private PlayerActions actions; // Reference to the PlayerActions script for input handling
    private Player player; // Reference to the Player script for player-related functionality
    private Rigidbody2D rb2D; // Reference to the Rigidbody2D component for physics-based movement
    private Vector2 moveDirection; // Direction of movement input

    private void Awake()
    {
        player = GetComponent<Player>(); // Get the Player component attached to the player GameObject
        actions = new PlayerActions(); // Initialize the PlayerActions script
        rb2D = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component attached to the player GameObject
        playerAnimations = GetComponent<PlayerAnimations>(); // Get the PlayerAnimations component for handling animations

    }

    private void FixedUpdate()
    {
        Move(); // Call the Move method to handle player movement in FixedUpdate for consistent physics updates
        //Debug.Log(moveDirection); // Optional: Log the move direction for debugging purposes
    }

    private void Move()
    {
        if (player.Stats.Health <= 0f) return; // If the player's health is zero or less, do not allow movement
        rb2D.MovePosition(rb2D.position + moveDirection * (moveSpeed * Time.fixedDeltaTime)); // Move the player based on the input direction and speed
        ReadMovement(); // Read the movement input from the PlayerActions script
    }

    private void ReadMovement()
    {
        // Read the movement input from the PlayerActions script
        moveDirection = actions.Movement.Move.ReadValue<Vector2>().normalized; // Normalize the input vector to ensure consistent speed in all directions //wenn du Quer läufst, läufst du schneller und deswegen muss dieser .normalized werden das der wert wieder auf 1 springt.
        if (moveDirection == Vector2.zero)
        {
            playerAnimations.SetMovingAnimation(false); // Set the moving animation to false
            return; // If there is no movement input, exit the method early
        }
        playerAnimations.SetMovingAnimation(true); // Set the moving animation to true if there is movement input
        playerAnimations.SetMoveAnimation(moveDirection); // Set the move animation based on the input direction

    }
    
    private void OnEnable()
    {
        actions.Enable(); // Enable the PlayerActions input handling
    }

    private void OnDisable()
    {
        actions.Disable(); // Disable the PlayerActions input handling
    }
}

