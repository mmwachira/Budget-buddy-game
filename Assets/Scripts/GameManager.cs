using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int currentWeek = 1;

    public BudgetManager budgetManager;
    public WeekTimer weekTimer;

    public float difficultyIncreasePerWeek = 0.1f;

    void Start()
    {
        weekTimer.OnWeekEnd += HandleWeekEnd;
    }

    public void OnItemSorted(float cost, bool isCorrect)
    {
        budgetManager.ApplySort(cost, isCorrect);
    }

    void HandleWeekEnd()
    {
        float savings = budgetManager.GetSavings();

        Debug.Log($"Week {currentWeek} savings: {savings}");

        currentWeek++;

        // Optional difficulty ramp — increases drop speed globally
        Time.timeScale += difficultyIncreasePerWeek;

        // Reset systems
        budgetManager.ResetForNewWeek();
        weekTimer.StartNewWeek();
    }
}
