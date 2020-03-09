using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private BoxCollider2D playerCollider;
    private SpriteRenderer spriteRenderer;

    private float defaultSpeed = 0.05f;

    private float input_x;
    private float input_y;

    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.freezeRotation = true;

        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void Update()
    {
        HandleMovementAnimations();

        GameManager.instance.SetPlayerColliderPosition(GetComponent<Transform>().position);

        spriteRenderer.sortingOrder = Mathf.RoundToInt(transform.position.y * 100f);

        Vector2 size = playerCollider.size;
        Vector3 centerPoint = new Vector3(playerCollider.offset.x, playerCollider.offset.y, 0f);
        Vector3 colliderWorldPos = transform.TransformPoint(playerCollider.offset);

        GameManager.instance.SetPlayerColliderPosition(colliderWorldPos);
    }

    private void MovePlayer()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        float calibratedSpeed = GetCalibratedSpeed(input_x, input_y);

        rigidbody2D.MovePosition(new Vector2(
            (transform.position.x + input_x * calibratedSpeed),
            (transform.position.y + input_y * calibratedSpeed))
        );
    }

    private float GetCalibratedSpeed(float x, float y)
    {
        if (Math.Abs(x) + Math.Abs(y) == 2)
            return defaultSpeed / (float) Math.Sqrt(2);

        return defaultSpeed;
    }

    private void HandleMovementAnimations()
    {
        input_x = Input.GetAxisRaw("Horizontal");
        input_y = Input.GetAxisRaw("Vertical");

        bool isRunning = (input_x != 0 || input_y != 0);
        animator.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            animator.SetFloat("x", input_x);
            animator.SetFloat("y", input_y);
        }
    }

}
