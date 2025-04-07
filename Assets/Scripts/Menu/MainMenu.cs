using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [Header("Setting Areas")]
    [SerializeField] GameObject audioArea;
    [SerializeField] GameObject videoArea;
    
    public void Play()
    {
        SceneLoader.Instance.LoadNextScene();
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        EnableAudioArea();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EnableAudioArea()
    {
        audioArea.SetActive(true);
        videoArea.SetActive(false);
    }

    public void EnableVideoArea()
    {
        audioArea.SetActive(false);
        videoArea.SetActive(true);
    }
}
