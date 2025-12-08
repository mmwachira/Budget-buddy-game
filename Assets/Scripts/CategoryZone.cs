using UnityEngine;

public class CategoryZone : MonoBehaviour
{
    public Category zoneCategory;   // Set this in Inspector (Need, Want, Saving/Miss)

    public int matchScore = 1;
    public int mismatchScore = -1;

    private ScoreManager scoreManager;
    private GameManager gameManager;

    void Start()
    {
        scoreManager = FindFirstObjectByType<ScoreManager>();
        gameManager = FindFirstObjectByType<GameManager>();

        if (scoreManager == null)
            Debug.LogError("ScoreManager not found in the scene!");

        if (gameManager == null)
            Debug.LogError("GameManager not found in the scene!");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        ItemType item = other.GetComponent<ItemType>();

        if (item != null)
        {
            bool isCorrect = (item.itemCategory == zoneCategory);

            Debug.Log($"Item of category {item.itemCategory} entered {zoneCategory} zone.");

            // --- SCORING ---
            if (isCorrect)
            {
                scoreManager.AddPoints(matchScore);
                Debug.Log($"SUCCESS: +{matchScore} points! New Score: {scoreManager.CurrentScore}");
            }
            else
            {
                scoreManager.AddPoints(mismatchScore);
                Debug.Log($"MISMATCH: {mismatchScore} penalty. New Score: {scoreManager.CurrentScore}");
            }

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
