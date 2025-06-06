using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
using UnityEngine.InputSystem;
public class ControlImageHandler : MonoBehaviour
{
    public LocalizedAsset<Sprite> keyboardControlsLocalized;
    public LocalizedAsset<Sprite> gamepadControlsLocalized;

    [SerializeField] private Image targetImage; 

    private void Start()
    {
        UpdateControlImage();
        LocalizationSettings.SelectedLocaleChanged += _ => UpdateControlImage();
    }

    private void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame)
        {
            SetControlType(isGamepad: false);
        }
        else if (Gamepad.current != null && IsAnyGamepadButtonPressed())
        {
            SetControlType(isGamepad: true);
        }
    }

    private bool IsAnyGamepadButtonPressed()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null) return false;

        return gamepad.buttonSouth.wasPressedThisFrame ||
            gamepad.buttonNorth.wasPressedThisFrame ||
            gamepad.buttonEast.wasPressedThisFrame ||
            gamepad.buttonWest.wasPressedThisFrame ||
            gamepad.leftShoulder.wasPressedThisFrame ||
            gamepad.rightShoulder.wasPressedThisFrame ||
            gamepad.leftTrigger.wasPressedThisFrame ||
            gamepad.rightTrigger.wasPressedThisFrame ||
            gamepad.startButton.wasPressedThisFrame ||
            gamepad.selectButton.wasPressedThisFrame ||
            gamepad.leftStickButton.wasPressedThisFrame ||
            gamepad.rightStickButton.wasPressedThisFrame ||
            gamepad.dpad.up.wasPressedThisFrame ||
            gamepad.dpad.down.wasPressedThisFrame ||
            gamepad.dpad.left.wasPressedThisFrame ||
            gamepad.dpad.right.wasPressedThisFrame;
    }

    private void SetControlType(bool isGamepad)
    {
        if (isGamepad)
        {
            gamepadControlsLocalized.LoadAssetAsync().Completed += handle =>
            {
                targetImage.sprite = handle.Result;
            };
        }
        else
        {
            keyboardControlsLocalized.LoadAssetAsync().Completed += handle =>
            {
                targetImage.sprite = handle.Result;
            };
        }
    }

    private void UpdateControlImage()
    {
        bool isGamepad = Gamepad.current != null && Gamepad.current.wasUpdatedThisFrame;
        SetControlType(isGamepad);
    }
}
