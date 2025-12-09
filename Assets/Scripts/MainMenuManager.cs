using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    private GameManager gameManager;

    [Header("Game References")]
    public ItemSpawner itemSpawner;
    public WeekTimer weekTimer;

    [Header("Menu Panels")]
    public GameObject mainMenuPanel;
    public GameObject playPanel;
    public GameObject howToPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();

        // Show menu, hide game when app starts
        mainMenuPanel.SetActive(true);
        playPanel.SetActive(false);
        howToPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);

        // Prevent spawner from running at start
        itemSpawner.StopSpawning();
    }

    // START GAME
    public void StartGame()
    {
        gameManager.bgmSource.Play();
        playPanel.SetActive(true);
        mainMenuPanel.SetActive(false);

        // Start gameplay systems
        itemSpawner.StartSpawning();
        weekTimer.StartNewWeek();
    }

    void OpenMainMenu()
    {
        mainMenuPanel.SetActive(true);
        playPanel.SetActive(false);
        howToPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }

    // HOW TO PLAY
    public void OpenHowToPlay()
    {
        howToPanel.SetActive(true);
    }

    // OPTIONS
    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
    }

    // CREDITS
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);
    }

    // CLOSE ANY PANEL
    public void ClosePanel(GameObject panel)
    {
        panel.SetActive(false);
    }

    // EXIT GAME (optional)
    public void QuitGame()
    {
        Application.Quit();
    }
}
