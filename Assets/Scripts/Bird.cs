
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float flapStrength = 5f;

    // Bird rotation settings
    public float upwardAngle = 25f;
    public float downwardAngle = -65f;
    public float rotationSpeed = 5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.position.y < -4.65f ||
            transform.position.y > 4.65f)
        {
            Debug.Log("Game Over! Bird left the screen.");

            if (GameManager.Instance != null)
            {
                GameManager.Instance.GameOver();
            }

            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) ||
            Input.GetMouseButtonDown(0))
        {
            Flap();
        }

        // Tilt upward while rising and downward while falling
        float targetAngle = rb.linearVelocity.y > 0f
            ? upwardAngle
            : downwardAngle;

        Quaternion targetRotation = Quaternion.Euler(
            0f, 0f, targetAngle
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void Flap()
    {
        rb.linearVelocity = new Vector2(
            rb.linearVelocity.x,
            flapStrength
        );
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bird collided with: " + collision.gameObject.name);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.GameOver();
        }
    }
}
