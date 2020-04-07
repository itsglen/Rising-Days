using System;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * Any input driven GameObject which contains this script can have movement animations
 * Depends on parent GameObject containing: Animator
*/
public class InputMoverAnimations : MonoBehaviour
{

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    bool lastDirectionLeft = false;

    void Start()
    {
        this.animator = GetComponent<Animator>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void HandleMovementAnimations(Vector2 input)
    {
        bool isMoving = (input.x != 0 || input.y != 0);
        spriteRenderer.flipX = input.x < 0 || lastDirectionLeft;
        animator.SetBool("isMoving", isMoving);

        if (input.x < 0 && input.y == 0)
            lastDirectionLeft = true;
        else if (input.x > 0 && input.y == 0)
            lastDirectionLeft = false;

        if (isMoving)
        {
            animator.SetFloat("x", input.x);
            animator.SetFloat("y", input.y);
        }

    }

    public void RespondToMovementEvent(Vector2 eventAction)
    {
        HandleMovementAnimations(eventAction);
    }

}
