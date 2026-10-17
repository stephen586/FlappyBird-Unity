
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float flapStrength = 5f;

    // Bird rotation settings
    public float upwardAngle = 25f;
    public float downwardAngle = -65f;
    public float rotationSpeed = 5f;

    // Bird sound effects
    public AudioClip flapSound;
    public AudioClip hitSound;

    private AudioSource audioSource;
    private Rigidbody2D rb;

    // Prevent repeated collision sounds
    private bool isDead = false;

    private void Awake()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (isDead)
            return;

        if (transform.position.y < -4.65f ||
            transform.position.y > 4.65f)
        {
            Debug.Log("Game Over! Bird left the screen.");
            Die();
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

        // Play sound whenever the bird flaps
        if (audioSource != null && flapSound != null)
        {
            audioSource.PlayOneShot(flapSound);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bird collided with: " + collision.gameObject.name);
        Die();
    }

    private void Die()
    {
        // Prevent repeated collision sounds
        if (isDead)
            return;

        if (GameManager.Instance == null)
            return;

        isDead = true;

        // Play collision sound once
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        // Trigger the existing Game Over system
        GameManager.Instance.GameOver();
    }
}
