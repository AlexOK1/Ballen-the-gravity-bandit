using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public SpringJoint2D _springJoint;
    private Rigidbody2D _rb;

    public float maxGrappleDistance = 7f; // Max distance for grapple
    public float grappleDamping = 0.8f; // Higher value = less swing bounce
    public float grappleFrequency = 1.5f; // Higher value = stiffer rope
    public LayerMask groundLayer; // To detect surfaces for grappling

    private bool hasJumped = false;
    private bool isGrappling = false;

    void Start()
    {
        _springJoint.enabled = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player is on the ground
        bool isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);

        // Reset jump status when on the ground
        if (isGrounded)
        {
            hasJumped = false;
        }

        // Detect if the player has jumped
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            hasJumped = true; // Player is now airborne
        }

        // Start grappling if holding Mouse0 and player has jumped
        if (Input.GetKey(KeyCode.Mouse0) && hasJumped)
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);
            float distance = Vector2.Distance(transform.position, mousePos);

            if (distance <= maxGrappleDistance) // Check if within range
            {
                _springJoint.connectedAnchor = mousePos;
                _springJoint.enabled = true;
                isGrappling = true;

                _springJoint.autoConfigureDistance = false;
                _springJoint.distance = distance * 0.9f; // Some slack for smoother swing
                _springJoint.frequency = grappleFrequency; // Adjust elasticity
                _springJoint.dampingRatio = grappleDamping; // Adjust damping (less bounce)

                // Set line renderer positions
                _lineRenderer.SetPosition(0, mousePos);
                _lineRenderer.SetPosition(1, transform.position);
                _lineRenderer.enabled = true;
            }
        }
        else
        {
            // Stop grappling when Mouse0 is released
            _springJoint.enabled = false;
            _lineRenderer.enabled = false;
            isGrappling = false;
        }

        // Update the line renderer position while swinging
        if (isGrappling)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}