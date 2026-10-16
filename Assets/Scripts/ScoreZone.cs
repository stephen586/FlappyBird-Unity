
using UnityEngine;

public class ScoreZone : MonoBehaviour
{
    private bool hasScored = false;
    private Collider2D birdCollider;
    private Collider2D[] pipeColliders;

    private void Start()
    {
        GameObject bird = GameObject.FindGameObjectWithTag("Player");

        if (bird != null)
        {
            birdCollider = bird.GetComponent<Collider2D>();
        }

        pipeColliders = transform.parent.GetComponentsInChildren<Collider2D>();
    }

    private void LateUpdate()
    {
        if (hasScored || birdCollider == null || Time.timeScale == 0f)
            return;

        float rightmostPipeEdge = float.NegativeInfinity;

        foreach (Collider2D pipe in pipeColliders)
        {
            if (pipe == null || pipe.isTrigger)
                continue;

            rightmostPipeEdge = Mathf.Max(
                rightmostPipeEdge,
                pipe.bounds.max.x
            );
        }

        if (rightmostPipeEdge == float.NegativeInfinity)
            return;

        // Score only when the entire bird is
        // beyond the right edge of both pipes.
        if (rightmostPipeEdge < birdCollider.bounds.min.x)
        {
            hasScored = true;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.AddScore();
            }
        }
    }
}
