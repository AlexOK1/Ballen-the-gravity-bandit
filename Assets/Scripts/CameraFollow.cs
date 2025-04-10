using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform followTarget;

    [Header("Camera Follow Settings")]
    public Vector2 followOffset = Vector2.zero;
    public float followSpeed = 15f;

    [Header("Look Ahead Settings")]
    public bool enableLookAhead = false;  // Disabled look-ahead
    public float lookAheadDistance = 2f;  // This is now irrelevant
    public float lookAheadSmoothing = 0.1f;  // This is now irrelevant

    [Header("Follow Multiplier Settings")]
    public float xFollowMultiplier = 1f;  // Multiplier for X axis follow speed
    public float yFollowMultiplier = 1f;  // Multiplier for Y axis follow speed

    private Vector3 currentVelocity = Vector3.zero;
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
            Debug.LogWarning("CameraFollow: No Rigidbody2D found on target.");
        }
    }

    void FixedUpdate()
    {
        if (followTarget == null) return;

        // Camera target position based on followTarget's position + offset
        Vector3 targetPosition = followTarget.position + (Vector3)followOffset;

        // Adjust target position by the follow multipliers
        targetPosition.x = transform.position.x + (targetPosition.x - transform.position.x) * xFollowMultiplier;
        targetPosition.y = transform.position.y + (targetPosition.y - transform.position.y) * yFollowMultiplier;

        // If look-ahead is enabled (but we disabled it above), it would affect the camera position
        Vector3 desiredPosition = targetPosition; // Remove any look-ahead logic

        desiredPosition.z = transform.position.z; // Keep the current camera Z (for 2D)

        // Smoothly move the camera towards the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.fixedDeltaTime);
    }
}
