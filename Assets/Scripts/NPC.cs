using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // The object that the NPC is trying to go towards
    private GameObject targetObject = null;
    private MachineI targetObjectScript = null;

    private float defaultSpeed = 0.025f;

    private float dir_x;
    private float dir_y;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;

        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        if (targetObject != null) { MoveNPC(); }
    }

    void Update()
    {
        HandleMovementAnimations();
        spriteRenderer.sortingOrder = Mathf.RoundToInt((transform.position.y - 0.5f) * 100f) * -1;

        foreach (GameObject storeObject in GameManager.instance.storeObjects)
        {
            if (!targetObject)
            {
                targetObject = storeObject;
                targetObjectScript = targetObject.GetComponent<MachineI>();
            }
        }
    }

    private void MoveNPC()
    {
        float calibratedSpeed = GetCalibratedSpeed(dir_x, dir_y);

        if (targetObject.transform.position.x + 0.05f >= transform.position.x && targetObject.transform.position.x - 0.05f <= transform.position.x)
        {
            dir_x = 0;
        }
        else if (targetObject.transform.position.x > transform.position.x)
        {
            dir_x = 1.0f;
        } 
        else
        {
            dir_x = -1.0f;
        }

        if (targetObject.transform.position.y - 0.5f <= transform.position.y)
        {
            dir_y = 0;
        }
        else if (targetObject.transform.position.y > transform.position.y)
        {
            dir_y = 1.0f;
        }
        else
        {
            dir_y = -1.0f;
        }

        if (dir_x == 0 && dir_y == 0)
        {
            InteractWithObject();
        }

        rigidbody2D.MovePosition(new Vector2(
            (transform.position.x + dir_x * calibratedSpeed),
            (transform.position.y + dir_y * calibratedSpeed))
        );
    }

    private float GetCalibratedSpeed(float x, float y)
    {
        if (Math.Abs(x) + Math.Abs(y) == 2)
            return defaultSpeed / (float)Math.Sqrt(2);

        return defaultSpeed;
    }

    private void HandleMovementAnimations()
    {
        bool isRunning = (dir_x != 0 || dir_y != 0);
        animator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            animator.SetFloat("x", dir_x);
            animator.SetFloat("y", dir_y);
        }
    }

    private void InteractWithObject()
    {
        if (targetObjectScript != null && !targetObjectScript.IsOccupied())
        {
            targetObjectScript.Interact();
        }
    }

}
