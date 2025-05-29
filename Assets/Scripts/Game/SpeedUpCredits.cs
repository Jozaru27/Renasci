using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpeedUpCredits : MonoBehaviour
{
    public void OnSpeedUp(InputAction.CallbackContext context)
    {
        if (context.started)
            Time.timeScale = 2;

        if (context.canceled)
            Time.timeScale = 1;
    }
}
