using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private Vector2 startPos;
    private Vector2 length;
    public GameObject cam;
    public Vector2 parallaxEffect;

    // Start is called before the first frame update
        void Start()
{
    startPos = new Vector2(transform.position.x, transform.position.y);

    SpriteRenderer sr = GetComponent<SpriteRenderer>();
    length = sr.bounds.size;
}

    void Update()
    {
        Vector2 camPos = cam.transform.position;

        Vector2 distance = new Vector2(camPos.x * parallaxEffect.x, camPos.y * parallaxEffect.y);
        Vector2 movement = new Vector2(camPos.x * (1 - parallaxEffect.x), camPos.y * (1 - parallaxEffect.y));

        transform.position = new Vector3(startPos.x + distance.x, startPos.y + distance.y, transform.position.z);

        if (movement.x > startPos.x + length.x)
        {
            startPos.x += length.x;
        }
        else if (movement.x < startPos.x - length.x)
        {
            startPos.x -= length.x;
        }

        if (movement.y > startPos.y + length.y)
        {
            startPos.y += length.y;
        }
        else if (movement.y < startPos.y - length.y)
        {
            startPos.y -= length.y;
        }
    }
}