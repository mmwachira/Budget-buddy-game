using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float spawnInterval = 1f;

    private float xMin;
    private float xMax;

    public float spawnHeight = 6f;
    public float padding = 0.5f; // Padding to keep items slightly away from the screen edge

    void Start()
    {
        // Check if the array is populated
        if (itemPrefabs.Length == 0)
        {
            Debug.LogError("Item Prefabs array is empty! Please assign prefabs in the Inspector.");
            return;
        }

        // Calculate the visible world bounds based on the camera
        CalculateScreenBounds();

        // Start repeatedly spawning items
        InvokeRepeating("SpawnItem", 0.5f, spawnInterval);
    }

    // Function to calculate and update the world boundaries
    void CalculateScreenBounds()
    {
        // Z-Position is crucial. Set it to the Z-plane of your falling items (usually 0).
        float zDepth = 0f;

        // Get the bottom-left corner of the camera's view (Viewport X=0, Y=0)
        Vector3 screenBottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, zDepth));

        // Get the top-right corner of the camera's view (Viewport X=1, Y=1)
        Vector3 screenTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, zDepth));

        // Calculate the horizontal bounds, applying padding
        xMin = screenBottomLeft.x + padding;
        xMax = screenTopRight.x - padding;

        // Set spawn height slightly above the top edge of the camera's view
        spawnHeight = screenTopRight.y + 0.5f; // Spawns 0.5 units above the top edge
    }


    // Update is called once per frame
    void SpawnItem()
    {
        // Randomly select a prefab from the array
        int randomIndex = Random.Range(0, itemPrefabs.Length);
        GameObject itemToSpawn = itemPrefabs[randomIndex];

        // Random horizontal position within the calculated screen bounds
        float xPos = Random.Range(xMin, xMax);

        // Spawn position at the top (using the public spawnHeight)
        Vector3 spawnPos = new Vector3(xPos, spawnHeight, 0f);

        // Instantiate the item
        Instantiate(itemToSpawn, spawnPos, Quaternion.identity);
    }
}