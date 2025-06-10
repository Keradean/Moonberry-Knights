using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private readonly int moveX = Animator.StringToHash("MoveX"); // Hash for the horizontal movement parameter
    private readonly int moveY = Animator.StringToHash("MoveY"); // Hash for the vertical movement parameter
    private readonly int moving = Animator.StringToHash("isMoving"); // Hash for the movement state parameter
    private readonly int dead = Animator.StringToHash("isDead"); // Hash for the dead state parameter
    private readonly int revive = Animator.StringToHash("isReviving"); // Hash for the revive state parameter


    private Animator animator; // Reference to the Animator component for animations

    private void Awake()
    {
        animator = GetComponent<Animator>(); // Get the Animator component attached to the player GameObject
    }

    public void SetDeadAnimation()
    {
        animator.SetTrigger(dead); // Trigger the "Dead" animation in the Animator
    }

    public void SetMovingAnimation(bool value)
    {
        animator.SetBool(moving, value); // Set the "isMoving" parameter in the Animator to control movement animations
    }

    public void SetMoveAnimation(Vector2 dir)
    {
        animator.SetFloat(moveX, dir.x); // Set the horizontal movement parameter in the Animator
        animator.SetFloat(moveY, dir.y); // Set the vertical movement parameter in the Animator
    }

    public void ResetStats()
    {
        SetMoveAnimation(Vector2.zero); // Reset movement animation parameters to zero
        animator.SetTrigger(revive); // Trigger the "Revive" animation in the Animator
    }
}
