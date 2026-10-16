
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnInterval = 2f;
    public float heightRange = 2f;

    private float timer;

  private void Start()
{
    timer = 2.2f;
}
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnPipe();
            timer = 0f;
        }
    }

    private void SpawnPipe()
    {
        if (pipePrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = transform.position;

        spawnPosition.y += Random.Range(
            -heightRange,
            heightRange
        );

        Instantiate(
            pipePrefab,
            spawnPosition,
            Quaternion.identity
        );
    }
}
