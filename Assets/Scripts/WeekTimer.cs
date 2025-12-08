using UnityEngine;
using TMPro;

public class WeekTimer : MonoBehaviour
{
    public float weekDuration = 30f;
    private float timeLeft;

    public TMP_Text timerText;

    public System.Action OnWeekEnded;

    private bool timerRunning = false;

    void Start()
    {
        StartNewWeek();
    }

    void Update()
    {
        if (!timerRunning) return;

        timeLeft -= Time.deltaTime;

        if (timeLeft <= 0f)
        {
            timeLeft = 0f;
            timerRunning = false; // stop the timer

            OnWeekEnded?.Invoke(); // notify GameManager
        }

        timerText.text = Mathf.Ceil(timeLeft).ToString();
    }

    public void StartNewWeek()
    {
        timeLeft = weekDuration;
        timerRunning = true;
    }

    public void StopTimer()
    {
        timerRunning = false;
    }
}
