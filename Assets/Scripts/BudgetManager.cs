using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BudgetManager : MonoBehaviour
{
    [Header("Budget Settings")]
    public float weeklyBudget = 1200f;
    public float currentBudget;

    [Header("UI References")]
    public Slider budgetSlider;
    public TMP_Text budgetText; // The text inside/near the slider
    public TMP_Text initialIncome;
    public TMP_Text IncomeSpent;
    public TMP_Text finalSavings;

    [Header("Gameplay Settings")]
    public float incorrectPenalty = 1.5f; // 50% extra cost for wrong category

    void Start()
    {
        ResetForNewWeek();
        UpdateBudgetUI();
    }

    // Called whenever player sorts an item
    public void ApplySort(float itemCost, bool isCorrect)
    {
        float finalCost = isCorrect ? itemCost : itemCost * incorrectPenalty;

        currentBudget -= finalCost;
        if (currentBudget < 0)
            currentBudget = 0;

        UpdateBudgetUI();
    }

    private void UpdateBudgetUI()
    {
        // Update slider range
        budgetSlider.maxValue = weeklyBudget;
        budgetSlider.value = currentBudget;

        // Update text
        budgetText.text = $"Weekly Budget: €{currentBudget:F0} / €{weeklyBudget:F0}";
    }

    public float GetSavings()
    {
        initialIncome.text = $"Initial Income: €{weeklyBudget:F0}";
        float spent = weeklyBudget - currentBudget;
        IncomeSpent.text = $"Total Spent: €{spent:F0}";
        finalSavings.text = $"Total Remaining: €{currentBudget:F0}";
        return currentBudget;

    }

    public void ResetForNewWeek()
    {
        currentBudget = weeklyBudget;

        if (budgetSlider != null)
            budgetSlider.value = currentBudget;

        UpdateBudgetUI();
    }
}
