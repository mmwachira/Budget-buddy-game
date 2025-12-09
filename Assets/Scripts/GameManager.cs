using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public ItemSpawner itemSpawner;
    public BudgetManager budgetManager;
    public WeekTimer weekTimer;

    [Header("UI Screens")]
    public GameObject winScreen;
    public GameObject failScreen;

    [Header("Audio")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip winSound;
    public AudioClip failSound;

    public int currentWeek = 1;
    public TMP_Text weekText;
    public TMP_Text winbuttonText;
    public TMP_Text losebuttonText;

    public float difficultyIncreasePerWeek = 0.1f;

    void Start()
    {
        weekTimer.OnWeekEnded += HandleWeekEnd;
        UpdateWeekUI();
    }

    public void OnItemSorted(float cost, bool isCorrect)
    {
        budgetManager.ApplySort(cost, isCorrect);
    }

    void HandleWeekEnd()
    {
        if (budgetManager.currentBudget > 0)
        {
            ShowWin();
        }
        else
        {
            ShowFail();
        }

        // float savings = budgetManager.GetSavings();

        // Debug.Log($"Week {currentWeek} savings: {savings}");

        // currentWeek++;

        // // Optional difficulty ramp — increases drop speed globally
        // Time.timeScale += difficultyIncreasePerWeek;

        // // Reset systems
        // budgetManager.ResetForNewWeek();
        // weekTimer.StartNewWeek();
    }

    void ShowWin()
    {
        itemSpawner.StopSpawning();
        DestroyAllItems();
        budgetManager.GetSavings();

        // Stop background music
        if (bgmSource != null)
            bgmSource.Stop();

        // Play win sound effect
        if (sfxSource != null && winSound != null)
            sfxSource.PlayOneShot(winSound);

        winbuttonText.text = $"CONTINUE TO WEEK {currentWeek + 1}";
        winScreen.SetActive(true);
    }

    void ShowFail()
    {
        itemSpawner.StopSpawning();
        DestroyAllItems();
        budgetManager.GetOverspend();

        // Stop background music
        if (bgmSource != null)
            bgmSource.Stop();

        // Play fail sound effect
        if (sfxSource != null && failSound != null)
            sfxSource.PlayOneShot(failSound);

        losebuttonText.text = $"RETRY WEEK {currentWeek}";
        failScreen.SetActive(true);
    }

    void UpdateWeekUI()
    {
        weekText.text = $"Week {currentWeek} of 4";
    }

    // Called by "Next Week" button
    public void NextWeek()
    {
        currentWeek += 1;

        winScreen.SetActive(false);

        budgetManager.ResetForNewWeek();
        weekTimer.StartNewWeek();
    }

    // Called by "Retry" button
    public void RetryWeek()
    {
        failScreen.SetActive(false);

        budgetManager.ResetForNewWeek();
        weekTimer.StartNewWeek();
    }

    public void DestroyAllItems()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

        foreach (GameObject item in items)
        {
            Destroy(item);
        }
    }

}
