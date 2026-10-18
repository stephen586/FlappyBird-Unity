
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float moveSpeed = 0.3f;
    public float leftBoundary = -4f;
    public float rightBoundary = 4f;

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < leftBoundary)
        {
            Vector3 newPosition = transform.position;
            newPosition.x = rightBoundary;
            transform.position = newPosition;
        }
    }
}
