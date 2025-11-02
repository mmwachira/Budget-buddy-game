using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public float spawnInterval = 1f;

    private float xMin;
    private float xMax;

    // We'll calculate these dynamically, but keep them for editor convenience if needed
    // private float xMinWorld; 
    // private float xMaxWorld; 

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
        // Get the bottom-left corner of the screen in World Space (where x=0, y=0 on screen)
        Vector3 screenBottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));

        // Get the top-right corner of the screen in World Space (where x=Screen.width, y=Screen.height on screen)
        Vector3 screenTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        // Use the calculated world X boundaries and apply padding
        // Note: For a falling game, spawnHeight might be better defined based on screenTopRight.y
        xMin = screenBottomLeft.x + padding;
        xMax = screenTopRight.x - padding;

        // If you want the spawn height to also be dynamic:
        // spawnHeight = screenTopRight.y + 1f; // Spawn slightly above the top edge
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