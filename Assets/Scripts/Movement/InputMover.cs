using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

/*
 * Any GameObject which contains this script can be controlled by user input
 * Depends on parent GameObject containing: Rigidbody2D
*/
public class InputMover : MonoBehaviour
{

    public Vector2Event movementEvent;
    public FloatVariable movementSpeed;
    private Rigidbody2D rigidbody2D;

    PlayerInputActions inputAction;
    Vector2 movementInput;

    // Awake since inputAction is referenced on Enable
    void Awake()
    {
        // Instantiate PlayerInputActions and pipe input events to movementInput
        this.inputAction = new PlayerInputActions();
        this.inputAction.PlayerControls.Move.performed += ctx => this.movementInput = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        this.rigidbody2D = GetComponent<Rigidbody2D>();
        this.rigidbody2D.freezeRotation = true;
    }

    void FixedUpdate()
    {
        float input_x = movementInput.x;
        float input_y = movementInput.y;

        float calibratedSpeed = this.GetCalibratedSpeed(input_x, input_y);

        movementEvent.Invoke(movementInput);

        rigidbody2D.MovePosition(new Vector2(
            (transform.position.x + input_x * calibratedSpeed),
            (transform.position.y + input_y * calibratedSpeed))
        );
    }

    // Prevent excessive speed in diagonal directions by pythagoras
    private float GetCalibratedSpeed(float x, float y)
    {
        if (Math.Abs(x) > 0 && Math.Abs(y) > 0)
            return movementSpeed.Value / (float) Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));

        return movementSpeed.Value;
    }

    private void OnEnable()
    {
        inputAction.Enable();
    }

    private void OnDisable()
    {
        inputAction.Disable();
    }

}
