using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    private float startpos;
    private Transform cam;
    [SerializeField] private float parallax;
    [SerializeField] private float speed = 0.1f;

    void Start()
    {
        cam = GameObject.Find("Main Camera").transform;
        startpos = transform.position.x;
    }

    void LateUpdate()
    {
        float distance = cam.position.x * parallax;
        transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);
    }
}
