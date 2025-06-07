using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VibrationManager : MonoBehaviour
{
    [SerializeField] PlayerInput input;

    bool justEnteredInKeyboard;
    bool justEnteredInGamepad;
    bool canRumble;

    Gamepad pad;

    public static VibrationManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (input.currentControlScheme == "Gamepad" && !justEnteredInGamepad)
        {
            canRumble = true;
            justEnteredInKeyboard = false;
            justEnteredInGamepad = true;
            pad = Gamepad.current;
            pad.ResetHaptics();
        }
        else if (input.currentControlScheme == "Keyboard" && !justEnteredInKeyboard)
        {
            canRumble = false;

            justEnteredInKeyboard = true;
            justEnteredInGamepad = false;
        }
    }

    public void RumbleGamepad(float lowFrequency, float highFrequency, float rumbleDuration)
    {
        if (canRumble && pad != null)
        {
            pad.ResetHaptics();
            pad.SetMotorSpeeds(lowFrequency, highFrequency);

            StopAllCoroutines();
            StartCoroutine(StopRumbling(rumbleDuration));
        }
    }

    IEnumerator StopRumbling(float duration)
    {
        yield return new WaitForSecondsRealtime(duration);

        pad.ResetHaptics();
    }
}
