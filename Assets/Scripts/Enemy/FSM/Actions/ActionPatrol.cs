using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPatrol : FSMAction
{
    [Header("Config")]
    [SerializeField] private float speed;  // Speed of the patrol movement
    [SerializeField] private float patrolTime; // Duration of the patrol before switching direction
    [SerializeField] private Vector2 moveRange; // Range of movement for the patrol 

    private Vector3 movePosition; // Current position to move towards
    private float timer; // Timer to track patrol duration

    private void Start()
    {
        GetNewDestination(); // Initialize the first destination
        timer = patrolTime; // Set the initial timer
    }
    public override void Act()
    {
        timer -= Time.deltaTime; // every time that timer is less or equal tham zero, we will change the destination
        Vector3 moveDirection = (movePosition - transform.position).normalized; // Calculate the direction to the new position
        Vector3 movement = moveDirection * (speed * Time.deltaTime); // Calculate the movement vector based on speed and time
        if (Vector3.Distance(transform.position, movePosition) >= 0.5f)
        {
            transform.Translate(movement); // Move towards the new position
        }

        if (timer <= 0f) 
        {
            GetNewDestination(); // Get a new destination when the timer reaches zero
            timer = patrolTime; // Reset the timer to patrolTime
        }
    }

    private void GetNewDestination()
    {
        float randomX = Random.Range(-moveRange.x, moveRange.x); // Generate random offsets within the defined range
        float randomY = Random.Range(-moveRange.y, moveRange.y); // Generate random offsets within the defined range
        movePosition = transform.position + new Vector3(randomX, randomY);  // Calculate the new destination position
    }

    private void OnDrawGizmosSelected()
    {
        if (moveRange != Vector2.zero)
        {
            Gizmos.color = Color.blue; // Set the color for the gizmo
            Gizmos.DrawWireCube(transform.position, moveRange * 2f); // Draw a wireframe cube to visualize the patrol area
            Gizmos.DrawLine(transform.position, movePosition); // Draw a line from the current position to the move position
        }
    }
}
