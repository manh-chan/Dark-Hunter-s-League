using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
public class PlayerMovement : Sortable
{
    public const float DEFAULT_MOVESPEED = 7f;
    private Vector2 moveDir;
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 LastMovedVector;
    Animator anim;
    Rigidbody2D rb;
    PlayerStats stats;

    public VariableJoystick joystick;

    private bool movIng;
    float x;
    float y;

    float joystickX;
    float joystickY;

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        stats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    private void Update()
    {
        InputManager();
    }
    private void FixedUpdate()
    {
        Move();
    }
    private void InputManager()
    {
        if (GameManager.instance.choosingUpgrade)
        {
            return;
        }
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        x = UnityEngine.Input.GetAxisRaw("Horizontal");
        y = UnityEngine.Input.GetAxisRaw("Vertical");

        joystickX = joystick.Horizontal;
        joystickY = joystick.Vertical;

        x = Mathf.Abs(joystickX) > 0.1f ? joystickX : x;
        y = Mathf.Abs(joystickY) > 0.1f ? joystickY : y;

        moveDir = new Vector2(x, y).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            LastMovedVector = new Vector2(lastHorizontalVector, 0f);
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            LastMovedVector = new Vector2(0f, lastVerticalVector);
        }
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            LastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector);
        }

        FlipController(x);
    }

    private void Move()
    {
        if (GameManager.instance.choosingUpgrade)
        {
            return;
        }
        if (GameManager.instance.isGameOver)
        {
            return;
        }
        rb.velocity = moveDir * DEFAULT_MOVESPEED * stats.Stats.moveSpeed;
    }

    #region Flip
    public virtual void Flip()
    {
        facingDir = facingDir * -1;
        facingRight = !facingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();
        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }
    }
    #endregion
}
