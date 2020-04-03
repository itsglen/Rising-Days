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

    void Start()
    {
        this.animator = GetComponent<Animator>();
    }

    private void HandleMovementAnimations(Vector2 input)
    {
        bool isMoving = (input.x != 0 || input.y != 0);
        animator.SetBool("isMoving", isMoving);

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
