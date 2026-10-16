
using UnityEngine;

public class PipeMove : MonoBehaviour
{
    public float moveSpeed = 2f;

    private void Update()
    {
        // Move pipes to the left
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        // Remove pipes after they leave the screen
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }
}
