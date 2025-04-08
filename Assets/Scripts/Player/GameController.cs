using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 startPos;
    SpriteRenderer spriteRenderer;
    Rigidbody2D playerRb;

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
    }
    

    void Die()
    {
        StartCoroutine(Respawn(0.1f));
    }

    IEnumerator Respawn(float duration)
    {
        spriteRenderer.enabled = false;
        playerRb.simulated = false;
        playerRb.velocity = new Vector2(0,0); // Stop the player movement
        yield return new WaitForSeconds(duration);
        transform.position = startPos;
        spriteRenderer.enabled = true;
        playerRb.simulated = true;
    }











}
