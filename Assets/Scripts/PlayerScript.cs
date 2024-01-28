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
    Transform groundcheckL;
    Transform groundcheckR;

    [SerializeField] bool _grounded = true;
    bool _jumping;

    const int GROUNDLAYER = 256;

    [SerializeField, Range(2, 5)] float _moveSpeed = 3;
    [SerializeField, Range(1, 5)] float _jumpForce = 4;
    [SerializeField, Range(3, 10)] float _balanceForce = 6;
    [SerializeField, Range(3, 5)] float _accRate = 3;
    [SerializeField, Range(5, 9)] float _decRate = 5;
    [SerializeField, Range(-0.1f, -1)] float _fallThreshold = -1f;
    [SerializeField, Range(0.1f, 0.5f)] float _groundcheckRadius = 0.1f;
    private float _targetSpeed;
    private float _acceleration;
    private float _movement;
    private float _targetRotation;
    private float _zAngle;
    private float _jumpTimer;
    private float _jumpTime = 0.3f;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        actions = new PlayerMovements();
        actionMap = GetComponent<PlayerInput>().currentActionMap;
        state = PlayerState.Idle;
        groundcheckL = transform.GetChild(0);
        groundcheckR = transform.GetChild(1);
    }

    private void OnEnable()
    {
        actions.Enable();
        actionMap.FindAction("Move").performed += Move;
        actionMap.FindAction("Move").canceled += Move;
        actionMap.FindAction("Jump").started += Jump;
        actionMap.FindAction("Jump").canceled += Jump;
        actionMap.FindAction("Balance").performed += Balance;
        actionMap.FindAction("Balance").canceled += Balance;
    }

    private void OnDisable()
    {
        actionMap.FindAction("Move").performed -= Move;
        actionMap.FindAction("Move").canceled -= Move;
        actionMap.FindAction("Jump").started -= Jump;
        actionMap.FindAction("Jump").canceled -= Jump;
        actionMap.FindAction("Balance").performed -= Balance;
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
        if (ctx.started && _grounded)
        {
            _jumpTimer = 0;
            body.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _jumping = true;
        }
        else if (ctx.canceled)
        {
            if (body.velocity.y > 0)
            {
                body.velocity = new Vector2(body.velocity.x, 0);
            }
        }
    }

    private void Balance(InputAction.CallbackContext ctx)
    {
        balanceInput = ctx.ReadValue<Vector2>();
        balanceInput.y = 0;
    }

    private void Update()
    {
        _jumpTimer += Time.deltaTime;
        if (_jumpTimer > _jumpTime) Groundcheck();

        if (_jumping) state = PlayerState.Jumping;
        else if (Mathf.Abs(moveInput.x) == 0 && _grounded) state = PlayerState.Idle;
        else if (Mathf.Abs(moveInput.x) > 0 && _grounded) state = PlayerState.Walking;

        if (body.velocity.y < _fallThreshold && !_jumping) state = PlayerState.Falling;
        
    }

    private void FixedUpdate()
    {
        _targetSpeed = moveInput.x * _moveSpeed;
        if (Mathf.Abs(_targetSpeed) > 0.1f) _acceleration = _accRate;
        else if (Mathf.Abs(_targetSpeed) <= 0.1f) _acceleration = _decRate;
        _movement = (_targetSpeed - body.velocity.x) * _acceleration;
        body.AddForce(_movement * Vector2.right, ForceMode2D.Force);

        if (Mathf.Abs(balanceInput.x) == 0) return;
        else
        {
            _targetRotation = -balanceInput.x * _balanceForce;
            _zAngle = body.rotation + _targetRotation;
            body.MoveRotation(Quaternion.Euler(0, 0, _zAngle));
        }
    }

    private void Groundcheck()
    {
        if (Physics2D.OverlapCircle(groundcheckL.position, _groundcheckRadius, GROUNDLAYER)
            || Physics2D.OverlapCircle(groundcheckR.position, _groundcheckRadius, GROUNDLAYER))
        {
            _grounded = true;
            _jumping = false;
        }
        else _grounded = false;
    }
}
