using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Menus")]
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject settingsMenu;
    [Header("Setting Areas")]
    [SerializeField] GameObject audioArea;
    [SerializeField] GameObject videoArea;
    [Header("Buttons")]
    [SerializeField] Button videoButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button playButton;

    private void Start()
    {
        GamepadMenuSupport.Instance.inMenu = true;
        GamepadMenuSupport.Instance.lastSelectedObject = playButton.gameObject;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        EnableVideoArea();
        EventSystem.current.SetSelectedGameObject(videoButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = videoButton.gameObject;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = playButton.gameObject;
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
