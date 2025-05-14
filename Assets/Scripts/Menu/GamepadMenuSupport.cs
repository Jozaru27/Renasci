using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GamepadMenuSupport : MonoBehaviour
{
    public bool inMenu;
    public GameObject lastSelectedObject;

    bool firstTimeInGamepad;

    [SerializeField] PlayerInput input;

    public static GamepadMenuSupport Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (input.currentControlScheme == "Keyboard")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (input.currentControlScheme == "Gamepad")
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        if (inMenu)
        {
            if (input.currentControlScheme == "Keyboard")
            {
                EventSystem.current.SetSelectedGameObject(null);
                firstTimeInGamepad = false;
            }
            else if (input.currentControlScheme == "Gamepad" && !firstTimeInGamepad)
            {
                if (lastSelectedObject != null)
                {
                    EventSystem.current.SetSelectedGameObject(lastSelectedObject);

                    firstTimeInGamepad = true;
                }
            }
        }
    }
}
