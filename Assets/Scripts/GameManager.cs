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

    [Header("Game Progression")]
    public int currentWeek = 1;
    public float difficultyIncreasePerWeek = 0.1f;
    public float budgetDecreasePercent = 0.1f; // 10% decrease per week

    [Header("UI References")]
    public TMP_Text weekText;
    public TMP_Text winbuttonText;
    public TMP_Text losebuttonText;



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
        else if (budgetManager.currentBudget <= 0)
        {
            ShowFail();
        }

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
        currentWeek++;

        winScreen.SetActive(false);

        itemSpawner.spawnInterval *= 1f - difficultyIncreasePerWeek;

        int newBudget = Mathf.RoundToInt(budgetManager.weeklyBudget * (1f - budgetDecreasePercent));
        budgetManager.SetNewWeekBudget(newBudget);

        weekTimer.StartNewWeek();
        itemSpawner.StartSpawning();

        if (bgmSource != null)
            bgmSource.Play();

        UpdateWeekUI();

        // budgetManager.ResetForNewWeek();
        // weekTimer.StartNewWeek();
    }

    // Called by "Retry" button
    public void RetryWeek()
    {
        failScreen.SetActive(false);

        budgetManager.ResetForNewWeek();
        weekTimer.StartNewWeek();
        itemSpawner.StartSpawning();

        // Resume BGM
        if (bgmSource != null)
            bgmSource.Play();
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
