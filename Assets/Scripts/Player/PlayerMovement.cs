using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 15f;

    [Header("Custom Gravity")]
    public float jumpGravity = 4f;
    public float fallGravity = 4f;

    [Header("Ground & Wall Detection")]
    public Transform groundCheck;
    public Transform wallCheck;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public float wallCheckRadius = 0.2f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool isTouchingWall;
    private float horizontalInput;
    private bool jumpQueued = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Input
        horizontalInput = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalInput) < 0.2f)
            horizontalInput = 0;

        // Queue jump (input buffering)
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            jumpQueued = true;
        }

        // Ground and wall checks
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
        isTouchingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, wallLayer);

        
    }

    void FixedUpdate()
    {
        // Horizontal movement
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

        // Stop ground sliding when idle (but not while jumping/falling or against walls)
        if (horizontalInput == 0 && isGrounded && !isTouchingWall)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }

        // Jump
        if (jumpQueued && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpQueued = false;
        }
        else
        {
            jumpQueued = false;
        }

        // Custom gravity
        ApplyCustomGravity();
    }

    void ApplyCustomGravity()
    {
        if (rb.velocity.y > 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (jumpGravity - 1) * Time.fixedDeltaTime;
        }
        else if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallGravity - 1) * Time.fixedDeltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, 0.2f);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(wallCheck.position, wallCheckRadius);
        }
    }
}
