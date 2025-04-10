using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    Rigidbody2D playerRb;
    private GameObject enemyParent; // Store the reference to the enemy parent

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            Die();
        }

        // Check if the collided object is tagged as "Enemy" (the child object)
        if (collision.CompareTag("Enemy"))
        {
            Bounce(collision);
        }
    }

    void Die()
    {
        StartCoroutine(Respawn(0.1f));
    }

    void Bounce(Collider2D collision)
    {
        // Make the player bounce (upward)
        playerRb.velocity = new Vector2(playerRb.velocity.x, 16f);

        // Disable the parent GameObject (the adult) of the collided "Enemy" GameObject (child)
        enemyParent = collision.transform.parent.gameObject; // Store the reference to the parent
        if (enemyParent != null)
        {
            enemyParent.SetActive(false);  // Disable the entire enemy (including its children)
            Debug.Log("Enemy GameObject (parent) has been disabled.");
        }
        else
        {
            Debug.LogWarning("No parent found for the collided 'Enemy'.");
        }
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;  // Disable player sprite renderer
        playerRb.simulated = false;      // Stop player physics
        playerRb.velocity = new Vector2(0, 0);  // Stop the player movement

        yield return new WaitForSeconds(duration);  // Wait for the respawn duration

        transform.position = startPos;  // Reset player position
        spriteRenderer.enabled = true;  // Enable player sprite renderer
        playerRb.simulated = true;      // Resume player physics

        // Re-enable the enemy when the player respawns
        if (enemyParent != null)
        {
            enemyParent.SetActive(true);  // Re-enable the entire enemy (including its children)
            Debug.Log("Enemy GameObject (parent) has been re-enabled.");
        }
    }
}
