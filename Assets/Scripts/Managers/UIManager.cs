using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    [Header("RelicsUI")]
    public GameObject relicsUI;

    [Header("UI Elements")]
    [SerializeField] TMP_Text lifeText;
    [SerializeField] TMP_Text enemyCountText;
    [SerializeField] TMP_Text bulletCountText;
    [SerializeField] TMP_Text relicText;
    [SerializeField] GameObject shootCooldown;
    [SerializeField] GameObject relicCooldown;
    [SerializeField] GameObject confussionIcon;

    [Header("UI Player Info Healthbar")]
    public Slider healthBarSlider; 
    public int maxHealth;
    [SerializeField] private Gradient gradient;
    private Coroutine healthLerpCoroutine;
    private float targetHealthValue;

    [Header("UI Player Info Staminabar")]
    [SerializeField] private Slider staminaBarSlider;
    public int maxStamina;
    [SerializeField] private Color staminaColor = new Color(0f, 0.992f, 1f);
    private Coroutine staminaRegenCoroutine;
    private float targetStaminaValue;

    [Header("UI Player Info AmmoLeft")]
    [SerializeField] private List<Image> bulletIcons;
    [SerializeField] private Color bulletFullColor = Color.white;
    [SerializeField] private Color bulletEmptyColor = new Color(0.2f, 0.2f, 0.2f, 1f);

    [Header("UI Player Info ActiveRelic")]
    [SerializeField] private Transform relicCircleParent;
    [SerializeField] private int currentRelicIndex = 0;
    [SerializeField] private List<RectTransform> relicIcons;
    [SerializeField] private int availableRelicCount = 0;

    [SerializeField] private GameObject fireRelicSprite;
    [SerializeField] private GameObject iceRelicSprite;
    [SerializeField] private GameObject windRelicSprite;

    [SerializeField] private List<Vector3> defaultScales = new List<Vector3>();
    
    [SerializeField] private bool isRotating = false;
    [SerializeField] public bool IsRotating => isRotating;

    [Header("Menus")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject gameOverMenu;
    [SerializeField] GameObject victoryMenu;
    [SerializeField] GameObject inventoryMenu;

    [Header("SettingsAreas")]
    [SerializeField] GameObject audioArea;
    [SerializeField] GameObject videoArea;

    [Header("Buttons")]
    [SerializeField] Button resumeButton;
    [SerializeField] Button settingsButton;
    [SerializeField] Button videoButton;
    [SerializeField] Button retryButtonWin;
    [SerializeField] Button retryButtonLose;
    [SerializeField] Button firstRelic;

    int enemyCount;

    public static UIManager Instance { get; private set; }

    public Attack attackScript;

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

        staminaBarSlider.maxValue = 1f;
        staminaBarSlider.value = 1f;

        ChangeLife();

        GameManager.Instance.ResetProperties();

        foreach (RectTransform icon in relicIcons)
        {
            defaultScales.Add(icon.localScale);
        }

        GamepadMenuSupport.Instance.lastSelectedObject = resumeButton.gameObject;
    }


    // HP BAR
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

    // DASH BAR
    public void ResetStamina()
    {
        if (staminaRegenCoroutine != null)
            StopCoroutine(staminaRegenCoroutine);

        staminaBarSlider.value = 0f;
        staminaRegenCoroutine = StartCoroutine(RechargeStamina());
    }

    private IEnumerator RechargeStamina()
    {
        float duration = StatsManager.Instance.dashCooldown;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            staminaBarSlider.value = Mathf.Lerp(0f, 1f, t);
            yield return null;
        }

        staminaBarSlider.value = 1f;
    }


    public void ChangeEnemyCount()
    {
        enemyCount--;
        enemyCountText.text = $"Enemies left: {enemyCount}";
    }


    // AMMO BAR
    public void ChangeBulletCount(int amount)
    {
        for (int i = 0; i < bulletIcons.Count; i++)
        {
            bulletIcons[i].color = i < amount ? bulletFullColor : bulletEmptyColor;
        }

        if (amount <= 0)
        {
            bulletIcons[0].color = bulletEmptyColor; // Esta línea es necesaria para que "despinte" Bullet1 por algún motivo

            StartCoroutine(RefillBulletsGradually());
        }
    }

    private IEnumerator RefillBulletsGradually()
    {
        float delay = 0.832f;
        for (int i = 0; i < bulletIcons.Count; i++)
        {
            yield return new WaitForSeconds(delay);
            bulletIcons[i].color = bulletFullColor;
        }
    }

    // RELIC INDICATOR
    public void ChangeRelicInfo(string relic)
    {
        if (GameManager.Instance.currentRelicSlots >= 0)
            relicText.text = $"Relic: {relic}";
    }

    public void UpdateRelicRotation(int slotIndex)
    {
        if (isRotating) return;

        isRotating = true;

        float angle = 0f;

        switch (slotIndex)
        {
            case 0: angle = 0f; break;
            case 1: angle = -120f; break;
            case 2: angle = 120f; break;
        }

        StartCoroutine(RotateCircleSmoothly(angle));
        StartCoroutine(HighlightSelectedRelic(slotIndex));
    }

    private IEnumerator RotateCircleSmoothly(float targetAngle)
    {
        
        float duration = 0.4f;
        float elapsed = 0f;

        Quaternion startRotation = relicCircleParent.rotation;
        Quaternion endRotation = Quaternion.Euler(0, 0, targetAngle);

        while (elapsed < duration)
        {
            relicCircleParent.rotation = Quaternion.Slerp(startRotation, endRotation, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        relicCircleParent.rotation = endRotation;

        isRotating = false;
    }

    public void UpdateRelicIcons(int relicCount)
    {
        fireRelicSprite.SetActive(false);
        iceRelicSprite.SetActive(false);
        windRelicSprite.SetActive(false);

        //if (relicCount >= 1)
        //    fireRelicSprite.SetActive(true);
        //if (relicCount >= 2)
        //    iceRelicSprite.SetActive(true);
        //if (relicCount >= 3)
        //    windRelicSprite.SetActive(true);


        if (relicCount >= 0)
        {
            fireRelicSprite.SetActive(true);
        }
        if (relicCount >= 1)
        {
            iceRelicSprite.SetActive(true);
        }
        if (relicCount >= 2)
        {
            windRelicSprite.SetActive(true);
        }
    }

    private IEnumerator HighlightSelectedRelic(int selectedIndex)
    {
        yield return new WaitForEndOfFrame();

        for (int i = 0; i < relicIcons.Count; i++)
        {
            RectTransform icon = relicIcons[i];
            Image img = icon.GetComponent<Image>();

            if (i == selectedIndex)
            {
                icon.localScale = defaultScales[i];
                img.color = new Color(1f, 1f, 1f, 1f);
            }
            else
            {
                icon.localScale = defaultScales[i] * 0.75f;
                img.color = new Color(1f, 1f, 1f, 0.75f);
            }
        }
    }

    public void UpdateRelicVisuals(int selectedIndex)
    {
        for (int i = 0; i < relicIcons.Count; i++)
        {
            Image iconImage = relicIcons[i].GetComponent<Image>();

            // if (iconImage != null)
            // {
            //     Color color = iconImage.color;
            //     color.a = (i == selectedIndex) ? 1f : 0.4f;
            //     iconImage.color = color;
            // }

            if (i == selectedIndex)
            {
                iconImage.color = Color.white;
            }
            else
            {
                iconImage.color = Color.gray;
            }
        }
    }

    public void EnablePauseMenu()
    {
        GamepadMenuSupport.Instance.inMenu = true;
        EventSystem.current.SetSelectedGameObject(resumeButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = resumeButton.gameObject;

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
        GamepadMenuSupport.Instance.inMenu = false;
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);
        GameManager.Instance.gamePaused = false;
        Time.timeScale = 1f;

        if (GameManager.Instance.onInventory)
            DisableInventoryMenu();
    }

    public void EnableGameOverMenu()
    {
        GamepadMenuSupport.Instance.inMenu = true;
        EventSystem.current.SetSelectedGameObject(retryButtonLose.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = retryButtonLose.gameObject;
        gameOverMenu.SetActive(true);
    }

    public void EnableVictoryMenu()
    {
        GamepadMenuSupport.Instance.inMenu = true;
        EventSystem.current.SetSelectedGameObject(retryButtonWin.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = retryButtonWin.gameObject;
        victoryMenu.SetActive(true);
    }

    public void Restart()
    {
        GameManager.Instance.ResetProperties();
        SceneLoader.Instance.LoadCurrentScene();
    }

    public void EnableSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(videoButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = videoButton.gameObject;
        settingsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        EnableVideoArea();
    }

    public void DisableSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(settingsButton.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = resumeButton.gameObject;
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
        GamepadMenuSupport.Instance.inMenu = true;
        EventSystem.current.SetSelectedGameObject(firstRelic.gameObject);
        GamepadMenuSupport.Instance.lastSelectedObject = firstRelic.gameObject;
        inventoryMenu.SetActive(true);
    }

    public void DisableInventoryMenu()
    {
        Time.timeScale = 1f;
        GamepadMenuSupport.Instance.inMenu = false;
        inventoryMenu.SetActive(false);
    }

    public void ActiveShootCooldown(float time)
    {
        StartCoroutine(SetCooldown(time, shootCooldown));
    }

    public void ActiveRelicCooldown(float time)
    {
        StartCoroutine(SetCooldown(time, relicCooldown));
    }

    public void ActiveConfussionCooldown(float time)
    {
        StartCoroutine(SetCooldown(time, confussionIcon));
    }

    IEnumerator SetCooldown(float timer, GameObject cooldownIcon)
    {
        cooldownIcon.SetActive(true);
        cooldownIcon.GetComponent<Image>().fillAmount = 1f;

        float timeElapsed = 0f;

        while (timeElapsed < timer)
        {
            timeElapsed += Time.deltaTime;

            cooldownIcon.GetComponent<Image>().fillAmount = 1f - (timeElapsed / timer);
            yield return null;
        }

        cooldownIcon.GetComponent<Image>().fillAmount = 0f;
        cooldownIcon.SetActive(false);
    }
}