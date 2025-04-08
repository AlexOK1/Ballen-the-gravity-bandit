using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplinghook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;

    private Rigidbody2D _rb;

    [Header("Grappling Settings")]
    [SerializeField] private float maxGrappleDistance = 10f;
    [SerializeField] private float grappleSlack = 0.5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _distanceJoint.enabled = false;

        _lineRenderer.positionCount = 2;
        _lineRenderer.startWidth = 0.05f;
        _lineRenderer.endWidth = 0.05f;
        _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        _lineRenderer.startColor = Color.white;
        _lineRenderer.endColor = Color.white;
        _lineRenderer.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!IsGrounded())
            {
                TryGrapple();
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            ReleaseGrapple();
        }

        if (_distanceJoint.enabled)
        {
            UpdateLineRenderer();
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    void TryGrapple()
    {
        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mouseWorldPos - (Vector2)transform.position;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, maxGrappleDistance, groundLayer);

        if (hit.collider != null)
        {
            Debug.Log("Grapple hit: " + hit.collider.name);

            float actualDistance = Vector2.Distance(transform.position, hit.point);

            _distanceJoint.enabled = true;
            _distanceJoint.autoConfigureDistance = false;
            _distanceJoint.connectedAnchor = hit.point;
            _distanceJoint.distance = actualDistance + grappleSlack;

            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, hit.point);
            _lineRenderer.SetPosition(1, transform.position);
        }
        else
        {
            Debug.Log("No valid grapple target.");
        }
    }

    void ReleaseGrapple()
    {
        _distanceJoint.enabled = false;
        _distanceJoint.distance = 0;
        _lineRenderer.enabled = false;
    }

    void UpdateLineRenderer()
    {
        _lineRenderer.SetPosition(1, transform.position);
    }
}