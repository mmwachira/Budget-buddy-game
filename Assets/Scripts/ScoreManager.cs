using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    public int CurrentScore => currentScore; // Public property to read the score

    public float weeklyBudget = 1500f;
    private float currentSpending = 0f;

    // UI References
    public Slider budgetMeter;
    public Text budgetText;
    public Text scoreText; // Assuming a running score

    void Start()
    {
        currentScore = 0;
        // UpdateScoreDisplay();
    }

    public void AddPoints(int points)
    {
        currentScore += points;
        UpdateScoreDisplay();
    }


    // Uncomment this method if you have a UI Text object you want to update
    private void UpdateScoreDisplay()
    {
        Debug.Log("Score updated: " + currentScore);
        // if (scoreText != null)
        // {
        //     scoreText.text = "Score: " + currentScore.ToString();
        // }
    }

    // Call this method when an item is successfully allocated
    public void AllocateSpending(float cost, string itemTag)
    {
        // 1. Check if we have enough budget
        if (currentSpending + cost > weeklyBudget)
        {
            // OVER BUDGET SCENARIO
            Debug.Log("OVER BUDGET! Item cost: " + cost);
            currentScore -= 100; // Small penalty for going over
        }

        // 2. Process the transaction
        currentSpending += cost;
        UpdateBudgetUI();

        // 3. Optional: Add a small score for a successful allocation
        currentScore += 50;
        scoreText.text = "Score: " + currentScore.ToString();
    }

    // Call this method when an item hits the bottom of the screen
    public void HandleItemMissed(string itemTag)
    {
        if (itemTag == "NeedItem")
        {
            // Severe penalty for missing a need
            currentScore -= 500;
            Debug.LogError("MISSED NEED! Severe Penalty!");
        }
        else if (itemTag == "WantItem")
        {
            // Reward for avoiding an unnecessary want
            currentScore += 100;
            Debug.Log("MISSED WANT! Budget Discipline Bonus!");
        }

        scoreText.text = "Score: " + currentScore.ToString();
    }

    void UpdateBudgetUI()
    {
        float remainingBudget = weeklyBudget - currentSpending;

        // Update the slider value
        budgetMeter.value = remainingBudget;

        // Update the text display
        budgetText.text = "$" + remainingBudget.ToString("F0");
    }

}