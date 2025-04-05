using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplinghook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public SpringJoint2D _springJoint;
    private Rigidbody2D _rb;

    void Start()
    {
        _springJoint.enabled = false;
        _rb = GetComponent<Rigidbody2D>();


        // Optional, but helps if line isn't showing
    _lineRenderer.positionCount = 2;
    _lineRenderer.startWidth = 0.05f;
    _lineRenderer.endWidth = 0.05f;
    _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    _lineRenderer.startColor = Color.white;
    _lineRenderer.endColor = Color.white;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Vector2 mousePos = (Vector2)mainCamera.ScreenToWorldPoint(Input.mousePosition);

            _rb.velocity = Vector2.zero; // Reset velocity to avoid weird forces

            _springJoint.connectedAnchor = mousePos;
            _springJoint.enabled = true;

            _springJoint.distance = Vector2.Distance(transform.position, mousePos);
            _springJoint.frequency = 0.5f; // Controls how bouncy the hook is
            _springJoint.dampingRatio = 0.3f; // Adds some resistance

            _lineRenderer.SetPosition(0, mousePos);
            _lineRenderer.SetPosition(1, transform.position);
            _lineRenderer.enabled = true;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _springJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (_springJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}