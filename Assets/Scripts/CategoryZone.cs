using UnityEngine;

public class CategoryZone : MonoBehaviour
{
    public Category zoneCategory;   // Set this in Inspector (Need, Want, Saving/Miss)

    public int matchScore = 1;
    public int mismatchScore = -1;

    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        if (gameManager == null)
            Debug.LogError("GameManager not found in the scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemType item = other.GetComponent<ItemType>();

        if (item != null)
        {
            bool isCorrect = item.itemCategory == zoneCategory;

            Debug.Log($"Item of category {item.itemCategory} entered {zoneCategory} zone.");



            // --- BUDGET ---
            if (gameManager != null)
            {
                gameManager.OnItemSorted(item.itemCost, isCorrect);
            }

            // Destroy the item
            Destroy(other.gameObject);
        }
    }
}
