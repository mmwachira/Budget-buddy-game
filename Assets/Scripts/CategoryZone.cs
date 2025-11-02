using UnityEngine;

public class CategoryZone : MonoBehaviour
{
    // Set this in the Inspector to tell the script what kind of zone it is
    public Category zoneCategory;

    // Define the score change for a successful match and a mismatch
    public int matchScore = 1;
    public int mismatchScore = -1;

    private ScoreManager scoreManager; // Reference to the scoring system

    void Start()
    {
        // Find the ScoreManager in the scene (assuming you put it on a single GameObject)
        scoreManager = FindFirstObjectByType<ScoreManager>();
        if (scoreManager == null)
        {
            Debug.LogError("ScoreManager not found in the scene! Cannot track points.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger is an item
        ItemType item = other.GetComponent<ItemType>();

        if (item != null)
        {
            Debug.Log($"Item of category {item.itemCategory} entered {zoneCategory} zone.");
            // Check if the item's category matches the zone's category
            if (item.itemCategory == zoneCategory)
                // {
                // Successful Match: Add points
                scoreManager.AddPoints(matchScore);
            Debug.Log($"SUCCESS: {item.itemCategory} sorted into {zoneCategory} zone. Score: {scoreManager.CurrentScore}");
            // }
            // else
            // {
            //     // Mismatch: Subtract points
            //     scoreManager.AddPoints(mismatchScore);
            //     Debug.Log($"MISMATCH: {item.itemCategory} sorted into {zoneCategory} zone. Score: {scoreManager.CurrentScore}");
            // }

            // Always destroy the item after it has been registered by a zone
            Destroy(other.gameObject);
        }
    }
}