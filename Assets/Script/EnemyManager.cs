using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemies = 10;
    public float spawnInterval = 3f;

    public float mapSize = 45f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is not assigned!");
            return;
        }

        if (GameObject.FindGameObjectsWithTag("Enemy").Length >= maxEnemies)
            return;

        Vector3 spawnPos = GetSpawnPosition();

        // Raycast down to find the ground
        RaycastHit hit;
        if (Physics.Raycast(spawnPos + Vector3.up * 50f, Vector3.down, out hit, 100f))
        {
            spawnPos = hit.point;
        }

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    Vector3 GetSpawnPosition()
    {
        int side = Random.Range(0, 2);

        switch (side)
        {
            case 0: // Left side
                return new Vector3(-mapSize, 0f, Random.Range(-mapSize, mapSize));

            default: // Right side
                return new Vector3(mapSize, 0f, Random.Range(-mapSize, mapSize));
        }
    }
}