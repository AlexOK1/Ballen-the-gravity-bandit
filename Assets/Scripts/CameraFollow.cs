using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset = 5f; // Lookahead distance for X-axis
    public float offsetSmoothing = 5f; // Smoothing speed for camera movement
    public float yOffset = 2f; // Optional vertical offset for Y-axis (can adjust to taste)
    public float yFollowMultiplier = 0.5f; // Multiplier to make Y-axis movement slower (half speed)

    private Vector3 targetPosition;
    private Rigidbody2D rb;

    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Start with the player's current position (use current Y position for camera)
        // Apply yFollowMultiplier to make the Y-axis movement slower
        targetPosition = new Vector3(player.transform.position.x, player.transform.position.y * yFollowMultiplier + yOffset, transform.position.z);

        // Check velocity to decide lookahead direction for X-axis
        if (rb.velocity.x > 0.1f)
        {
            targetPosition.x += offset; // Moving right
        }
        else if (rb.velocity.x < -0.1f)
        {
            targetPosition.x -= offset; // Moving left
        }

        // Smoothly move the camera
        transform.position = Vector3.Lerp(transform.position, targetPosition, offsetSmoothing * Time.deltaTime);
    }
}
