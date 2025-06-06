using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlManager : MonoBehaviour
{
    [Header("Player Input")]
    [SerializeField] PlayerInput playerAction;

    [Header("Full Controls Sprites")]
    public Sprite fullKeyboard;
    public Sprite fullGamepad;

    [Header("UI Element")]
    [SerializeField] Image infoImage;
    public bool affectInfoImage;

    [Header("Controls Elements")]
    [SerializeField] GameObject keyboardControlPanel;
    [SerializeField] GameObject gamepadControlPanel;

    [Header("Specific Controls")]
    [SerializeField] GameObject changeRelicGamepad;
    [SerializeField] GameObject changeRelicKeyboard;
    [SerializeField] GameObject useRelicGamepad;
    [SerializeField] GameObject useRelicKeyboard;

    [HideInInspector] public Sprite fullCurrentControls;

    public static ControlManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fullCurrentControls = fullKeyboard;

        changeRelicGamepad.SetActive(false);
        changeRelicKeyboard.SetActive(false);
        useRelicGamepad.SetActive(false);
        useRelicKeyboard.SetActive(false);
    }

    private void Update()
    {
        if (playerAction.currentControlScheme == "Keyboard")
        {
            fullCurrentControls = fullKeyboard;
            keyboardControlPanel.SetActive(true);
            gamepadControlPanel.SetActive(false);
        }
        else
        {
            fullCurrentControls = fullGamepad;
            keyboardControlPanel.SetActive(false);
            gamepadControlPanel.SetActive(true);
        } 

        if (affectInfoImage)
            infoImage.sprite = fullCurrentControls;

        if (GameManager.Instance.currentRelicSlots == 0)
        {
            useRelicGamepad.SetActive(true);
            useRelicKeyboard.SetActive(true);
        }
        else if (GameManager.Instance.currentRelicSlots == 1)
        {
            changeRelicGamepad.SetActive(true);
            changeRelicKeyboard.SetActive(true);
        }
    }

    public void ChangeGamepad(Sprite newGamepad)
    {
        fullGamepad = newGamepad;
    }

    public void ChangeKeyboard(Sprite newKeyboard)
    {
        fullKeyboard = newKeyboard;
    }
}
