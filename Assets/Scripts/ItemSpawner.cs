using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public float spawnInterval = 1f;
    public float xMin = -8f;
    public float xMax = 8f;            
    public float spawnHeight = 6f;
    void Start()
    {
        // Start repeatedly spawning items
        InvokeRepeating("SpawnItem", 0.5f, spawnInterval);
    }

    // Update is called once per frame
    void SpawnItem()
    {
        // Random horizontal position
        float xPos = Random.Range(xMin, xMax);

        // Spawn position at the top
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0f);

        // Instantiate the item
        Instantiate(itemPrefab, spawnPos, Quaternion.identity);
    }
}
