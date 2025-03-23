using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] TMP_Text lifeText;
    [SerializeField] TMP_Text enemyCountText;
    [Header("Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject victoryMenu;

    [Header("SettingsAreas")]
    [SerializeField] GameObject audioArea;
    [SerializeField] GameObject videoArea;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeLife()
    {
        //lifeText.text = $"Life: {StatsManager.Instance.life}";
    }

    public void ChangeEnemyCount()
    {
        enemyCountText.text = $"Enemies left: ";
    }

    public void EnablePauseMenu()
    {
        pauseMenu.SetActive(true);
        GameManager.Instance.gamePaused = true;
        Time.timeScale = 0f;
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1f;
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true);
    }

    public void EnableVictoryMenu()
    {
        victoryMenu.SetActive(true);
    }

    public void Restart()
    {
        GameManager.Instance.ResetProperties();
        SceneLoader.Instance.LoadCurrentScene();
    }

    public void EnableSettingsMenu()
    {
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        EnableAudioArea();
    }

    public void DisableSettingsMenu()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        GameManager.Instance.ResetProperties();
        SceneLoader.Instance.LoadMainMenu();
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
