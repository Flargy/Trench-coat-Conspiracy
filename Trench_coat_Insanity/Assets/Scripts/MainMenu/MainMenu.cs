using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public string nameOfStartingScene;

    public Button playButton;
    public Button creditsButton;
    public Button quitGameButton;
    public Button mainMenuButton;
    
    
    
    public Canvas canvasMainMenu;
    public Canvas canvasCredits;

    
    void Awake()
    {
        GoToMainMenu();

        canvasCredits.gameObject.SetActive(false);
        playButton.onClick.AddListener(PlayGame);
        creditsButton.onClick.AddListener(GoToCredits);
        mainMenuButton.onClick.AddListener(GoToMainMenu);
        quitGameButton.onClick.AddListener(QuitGame);
    }

    void GoToCredits()
    {
        canvasCredits.gameObject.SetActive(true);
        canvasMainMenu.gameObject.SetActive(false);
    }

    void GoToMainMenu()
    {
        canvasMainMenu.gameObject.SetActive(true);
        canvasCredits.gameObject.SetActive(false);
    }


    void PlayGame()
    {
        SceneManager.LoadScene(nameOfStartingScene);
    }
    
    void QuitGame()
    {
        Application.Quit();
    }
}
