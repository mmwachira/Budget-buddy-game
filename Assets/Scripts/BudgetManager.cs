using UnityEngine;
using UnityEngine.UI;

public class BudgetManager : MonoBehaviour
{
    public float weeklyBudget = 300f;
    public float currentBudget;

    public Slider budgetSlider;

    public float incorrectPenalty = 1.5f; // 50% extra cost

    void Start()
    {
        currentBudget = weeklyBudget;
        budgetSlider.maxValue = weeklyBudget;
        budgetSlider.value = currentBudget;
    }

    public void ApplySort(float itemCost, bool isCorrect)
    {
        float cost = itemCost;

        if (!isCorrect)
            cost *= incorrectPenalty;

        currentBudget -= cost;

        if (currentBudget < 0)
            currentBudget = 0;

        budgetSlider.value = currentBudget;
    }

    public float GetSavings()
    {
        return currentBudget;
    }

    public void ResetForNewWeek()
    {
        currentBudget = weeklyBudget;
        budgetSlider.value = currentBudget;
    }
}
