using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject howToPanel;
    public GameObject optionsPanel;
    public GameObject creditsPanel;

    // START GAME
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // Let you complete
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
