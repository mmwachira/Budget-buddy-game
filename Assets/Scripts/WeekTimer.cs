using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeekTimer : MonoBehaviour
{
    public float weekDuration = 30f;
    private float timeLeft;

    public TMP_Text timerText;

    public System.Action OnWeekEnd;

    void Start()
    {
        StartNewWeek();
    }

    void Update()
    {
        timeLeft -= Time.deltaTime;

        if (timeLeft < 0)
        {
            timeLeft = 0;
            OnWeekEnd?.Invoke();
        }

        timerText.text = Mathf.Ceil(timeLeft).ToString();
    }

    public void StartNewWeek()
    {
        timeLeft = weekDuration;
    }
}
