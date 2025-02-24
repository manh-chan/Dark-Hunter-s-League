using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayerMove : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    private Vector3 movement;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        PlayerMove();
    }
    private void PlayerMove()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        movement = new Vector3(moveX, moveY, 0) * Time.fixedDeltaTime;
        movement.Normalize();
        rb.velocity = movement * speed;
    }
}
