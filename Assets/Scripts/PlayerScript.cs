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
        
    }

    private void Jump(InputAction.CallbackContext ctx)
    {
    
    }

    private void Balance(InputAction.CallbackContext ctx)
    {
    
    }

    void Update()
    {
        
    }
}
