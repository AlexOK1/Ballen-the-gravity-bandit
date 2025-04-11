using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;            // The player's transform
    public float smoothSpeed = 0.125f;  // Smooth speed for camera movement
    public Vector3 offset;             // Camera offset (distance between camera and player)

    public float yFollowFactor = 0.5f; // Public float to control how much the camera follows the Y-axis (0 to 1)

    private float initialY;             // To keep track of the initial Y position

    private void Start()
    {
        // Set the initial Y position of the camera relative to the player
        initialY = transform.position.y - player.position.y;
    }

    private void FixedUpdate()
    {
        // Follow the player on the X-axis
        float desiredX = player.position.x + offset.x;
        
        // Follow the player's Y-axis, adjusted by the yFollowFactor (0 to 1)
        float desiredY = initialY + (player.position.y - initialY) * yFollowFactor + offset.y;

        // Set the new camera position
        Vector3 desiredPosition = new Vector3(desiredX, desiredY, transform.position.z);
        
        // Smoothly move the camera to the desired position
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}
