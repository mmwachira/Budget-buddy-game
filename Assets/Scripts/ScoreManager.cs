using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int currentScore = 0;
    public int CurrentScore => currentScore; // Public property to read the score

    // Optionally link a UI Text element for display
    // public TMPro.TextMeshProUGUI scoreText; 

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

}