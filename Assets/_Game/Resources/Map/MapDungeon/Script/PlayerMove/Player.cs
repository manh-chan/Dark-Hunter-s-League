using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement 
    [HideInInspector]
    public float lastHorizontalVector;
    [HideInInspector]
    public float lastVerticalVector;
    [HideInInspector]
    public Vector2 moveDir;
    [HideInInspector]
    public Vector2 lastMovedVector;
    public float moveSpeed;

    //References
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastMovedVector = new Vector2(1, 0f); //if we don't do this and game start up and the player doesn't move, the projectile weapon will have no mometum
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void InputManagement()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); //last move x
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0f, lastVerticalVector); //last move y
        }
        if (moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); //while moving
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
