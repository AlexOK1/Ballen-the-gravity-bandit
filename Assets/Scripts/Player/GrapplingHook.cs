using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grapplinghook : MonoBehaviour
{
    public Camera mainCamera;
    public LineRenderer _lineRenderer;
    public DistanceJoint2D _distanceJoint;
    private Rigidbody2D _rb;

    [SerializeField] private float maxGrappleDistance = 10f;

    void Start()
    {
        _distanceJoint.enabled = false;
        _rb = GetComponent<Rigidbody2D>();

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
            Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mouseWorldPos - (Vector2)transform.position;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction.normalized, maxGrappleDistance, LayerMask.GetMask("Ground"));

            if (hit.collider != null)
            {
                Debug.Log("Grapple hit: " + hit.collider.name);

                _distanceJoint.enabled = true;
                _distanceJoint.connectedAnchor = hit.point;
                _distanceJoint.autoConfigureDistance = false;
                _distanceJoint.distance = Vector2.Distance(transform.position, hit.point);

                _lineRenderer.SetPosition(0, hit.point);
                _lineRenderer.SetPosition(1, transform.position);
                _lineRenderer.enabled = true;
            }
            else
            {
                Debug.Log("No valid grapple target.");
            }
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            _distanceJoint.enabled = false;
            _lineRenderer.enabled = false;
        }

        if (_distanceJoint.enabled)
        {
            _lineRenderer.SetPosition(1, transform.position);
        }
    }
}