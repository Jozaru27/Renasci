using System.Collections;
using TMPro;
using UnityEditor;
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

    [SerializeField] Image fadeImg;
    [SerializeField] Button[] menuButtons;

    private void Start()
    {
        GamepadMenuSupport.Instance.inMenu = true;
        GamepadMenuSupport.Instance.lastSelectedObject = playButton.gameObject;

        Time.timeScale = 1f;
    }

    public void Settings()
    {
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
        EnableVideoArea();
        EventSystem.current.SetSelectedGameObject(videoButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = videoButton.gameObject;
    }

    public void Credits()
    {
        foreach (Button currentButton in menuButtons)
        {
            currentButton.interactable = false;
        }

        fadeImg.gameObject.SetActive(true);

        StartCoroutine(FadeToBlack());
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

    IEnumerator FadeToBlack()
    {
        Color fadeColor = Color.black;
        fadeColor.a = 0;

        fadeImg.color = fadeColor;

        float timeElapsed = 0f;

        while (fadeImg.color.a < 1)
        {
            timeElapsed += Time.deltaTime;

            fadeColor.a += 1 * 0.35f * Time.deltaTime;
            fadeImg.color = fadeColor;

            yield return null;
        }

        SceneLoader.Instance.LoadSpecificSceneAsync(2);
    }
}
