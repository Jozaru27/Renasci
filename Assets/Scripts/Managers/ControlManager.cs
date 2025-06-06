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

    [HideInInspector] public Sprite fullCurrentControls;

    public static ControlManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fullCurrentControls = fullKeyboard;
    }

    private void Update()
    {
        if (playerAction.currentControlScheme == "Keyboard")
            fullCurrentControls = fullKeyboard;
        else
            fullCurrentControls = fullGamepad;

        if (affectInfoImage)
            infoImage.sprite = fullCurrentControls;
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
