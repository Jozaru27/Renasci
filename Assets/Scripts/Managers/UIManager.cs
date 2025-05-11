using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("RelicsUI")]
    public GameObject relicsUI;

    [Header("UI Elements")]
    [SerializeField] TMP_Text lifeText;
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text bulletCountText;
    [SerializeField] TMP_Text relicText;

    public Slider healthBarSlider; 
    public int maxHealth;
    [SerializeField] private Gradient gradient;
    private Coroutine healthLerpCoroutine;
    private float targetHealthValue;


    [Header("Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject victoryMenu;
    [SerializeField] GameObject inventoryMenu;

    [Header("SettingsAreas")]
    [SerializeField] GameObject audioArea;
    [SerializeField] GameObject videoArea;

    int enemyCount;

    public static UIManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyCountText.text = $"Enemies left: {enemyCount}";
        bulletCountText.text = $"Bullets: 6";
        relicText.text = $"Relic: None";
        healthBarSlider.maxValue = maxHealth;
        ChangeLife();

        GameManager.Instance.ResetProperties();
    }

    public void ChangeLife()
    {
        lifeText.text = $"Life: {StatsManager.Instance.life}";
        targetHealthValue = (float)StatsManager.Instance.life / StatsManager.Instance.maxLife * healthBarSlider.maxValue;

        if (healthLerpCoroutine != null)
            StopCoroutine(healthLerpCoroutine);

        healthLerpCoroutine = StartCoroutine(SmoothHealthBarChange());

        // float normalizedLife = (float)StatsManager.Instance.life / StatsManager.Instance.maxLife;
        // healthBarSlider.fillRect.GetComponent<Image>().color = gradient.Evaluate(normalizedLife);
    }

    private IEnumerator SmoothHealthBarChange()
    {
        float startValue = healthBarSlider.value;
        float duration = 0.5f;
        float elapsed = 0f;

        float startNormalized = startValue / healthBarSlider.maxValue;
        float endNormalized = targetHealthValue / healthBarSlider.maxValue;

        Color startColor = gradient.Evaluate(startNormalized);
        Color endColor = gradient.Evaluate(endNormalized);

        Image fillImage = healthBarSlider.fillRect.GetComponent<Image>();

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            healthBarSlider.value = Mathf.Lerp(startValue, targetHealthValue, t);
            fillImage.color = Color.Lerp(startColor, endColor, t);

            yield return null;
        }

        healthBarSlider.value = targetHealthValue;
        fillImage.color = endColor;
    }



    public void ChangeEnemyCount()
    {
        enemyCount--;
        enemyCountText.text = $"Enemies left: {enemyCount}";
    }

    public void ChangeBulletCount(int amount)
    {
        if (amount <= 0)
            bulletCountText.text = $"Bullet: RECHARGING...";
        else
            bulletCountText.text = $"Bullets: {amount}";
    }

    public void ChangeRelicInfo(string relic)
    {
        if (GameManager.Instance.currentRelicSlots >= 0)
            relicText.text = $"Relic: {relic}";
    }

    public void EnablePauseMenu()
    {
        if (!GameManager.Instance.gameWin && !GameManager.Instance.gameOver && !GameManager.Instance.onInventory)
        {
            pauseMenu.SetActive(true);
            GameManager.Instance.gamePaused = true;
            Time.timeScale = 0f;
        }

        if (GameManager.Instance.onInventory)
            DisableInventoryMenu();
    }

    public void DisablePauseMenu()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1f;

        if (GameManager.Instance.onInventory)
            DisableInventoryMenu();
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

    public void EnableInventoryMenu()
    {
        Time.timeScale = 0f;
        inventoryMenu.SetActive(true);
    }

    public void DisableInventoryMenu()
    {
        Time.timeScale = 1f;
        inventoryMenu.SetActive(false);
    }
}