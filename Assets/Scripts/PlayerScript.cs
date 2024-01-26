using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum PlayerState
{
    Idle,
    Climbing,
    Walking,
    Falling,
    Jumping,
}


public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    CapsuleCollider2D coll;
    PlayerMovements actions;
    InputActionMap actionMap;
    Vector2 moveInput;
    Vector2 balanceInput;
    public PlayerState state;

    bool grounded;

    [SerializeField, Range(0.5f, 3)] float moveSpeed;
    [SerializeField, Range(1, 5)] float jumpForce;
    [SerializeField, Range(0.5f, 2)] float balanceForce;

    private float _targetSpeed;
    private float _acceleration;
    private float _movement;
    private float _jumpTimer;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        actions = new PlayerMovements();
        state = PlayerState.Idle;
    }

    private void OnEnable()
    {
        actions.Enable();
        actionMap.FindAction("Move").performed += Move;
        actionMap.FindAction("Move").canceled += Move;
        actionMap.FindAction("Jump").started += Jump;
        actionMap.FindAction("Jump").canceled += Jump;
        actionMap.FindAction("Balance").started += Balance;
        actionMap.FindAction("Balance").canceled += Balance;
    }

    private void OnDisable()
    {
        actionMap.FindAction("Move").performed -= Move;
        actionMap.FindAction("Move").canceled -= Move;
        actionMap.FindAction("Jump").started -= Jump;
        actionMap.FindAction("Jump").canceled -= Jump;
        actionMap.FindAction("Balance").started -= Balance;
        actionMap.FindAction("Balance").canceled -= Balance;
        actions.Disable();
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
        moveInput.y = 0;
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
        if (ctx.started && grounded)
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void Balance(InputAction.CallbackContext ctx)
    {
        balanceInput = ctx.ReadValue<Vector2>();
        balanceInput.y = 0;
    }

    void FixedUpdate()
    {
        _movement = (_targetSpeed - body.velocity.x) * _acceleration;
        body.AddForce(_movement * Vector2.right, ForceMode2D.Force);
    }
}
