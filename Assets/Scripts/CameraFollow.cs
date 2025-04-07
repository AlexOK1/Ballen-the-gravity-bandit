using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform followTarget;

    [Header("Camera Follow Settings")]
    public Vector2 followOffset = Vector2.zero;
    public float followSpeed = 15f;

    [Header("Look Ahead Settings")]
    public bool enableLookAhead = true;
    public float lookAheadDistance = 2f;
    public float lookAheadSmoothing = 0.1f;

    private Vector3 currentVelocity = Vector3.zero;
    private Vector3 lookAheadPos = Vector3.zero;
    private Rigidbody2D rb;

    void Start()
    {
        if (followTarget == null)
        {
            Debug.LogError("CameraFollow: No follow target assigned!");
            return;
        }

        rb = followTarget.GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogWarning("CameraFollow: No Rigidbody2D found on target. Look-ahead will be disabled.");
            enableLookAhead = false;
        }
    }

    void FixedUpdate()
    {
        if (followTarget == null) return;

        Vector3 targetPosition = followTarget.position + (Vector3)followOffset;

        if (enableLookAhead && rb != null)
        {
            Vector3 targetLookAhead = Vector3.right * Mathf.Sign(rb.velocity.x) * lookAheadDistance;
            lookAheadPos = Vector3.SmoothDamp(lookAheadPos, targetLookAhead, ref currentVelocity, lookAheadSmoothing);
        }
        else
        {
            lookAheadPos = Vector3.zero;
        }

        Vector3 desiredPosition = targetPosition + lookAheadPos;
        desiredPosition.z = transform.position.z; // Keep the current camera Z (for 2D)

        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);
    }
}