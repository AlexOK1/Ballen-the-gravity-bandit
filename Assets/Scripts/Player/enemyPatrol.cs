using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyPatrol : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    private bool movingToPointB = true; // Track which direction the enemy is moving in

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = PointB.transform; // Start by moving towards PointB
    }

    // Update is called once per frame
    void Update()
    {
        // Move towards the current point
        Vector2 direction = (currentPoint.position - transform.position).normalized;

        // Only move on the X-axis: set Y to current Y velocity to prevent vertical movement
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);

        // Check if we've reached the current point
        if (Mathf.Abs(transform.position.x - currentPoint.position.x) < 0.5f)
        {
            // Switch to the other point
            if (movingToPointB)
            {
                currentPoint = PointA.transform;
            }
            else
            {
                currentPoint = PointB.transform;
            }
            // Toggle the direction flag
            movingToPointB = !movingToPointB;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(PointA.transform.position, 0.5f);
        Gizmos.DrawWireSphere(PointB.transform.position, 0.5f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
