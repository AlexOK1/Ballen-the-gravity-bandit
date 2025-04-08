using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 15f;
    public float jumpGravity = 4f;     // Gravity while going up (should be positive)
    public float fallGravity = 4f;     // Gravity while falling (should be positive)
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;               // Disable built-in gravity to apply custom gravity
    }

    void Update()
    {
        // Horizontal input and movement
        float horizontalInput = Input.GetAxis("Horizontal");

        // Apply horizontal velocity while preserving vertical velocity
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Ground check to see if player can jump
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // Jumping
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // Apply custom gravity based on whether we are going up or falling
        ApplyCustomGravity();
    }

    void ApplyCustomGravity()
    {
        // Apply different gravity multiplier for jumping vs falling
        if (rb.velocity.y > 0)
        {
            // Going up
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpGravity - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y < 0)
        {
            // Falling down
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.deltaTime;
        }
    }
}
