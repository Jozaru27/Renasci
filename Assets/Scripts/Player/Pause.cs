using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pause : MonoBehaviour
{
    bool paused;

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gameOver && !GameManager.Instance.gameWin && !GameManager.Instance.inInfo && GameManager.Instance.gamePausable)
        {
            if (GameManager.Instance.onInventory)
            {
                UIManager.Instance.DisableInventoryMenu();
                GameManager.Instance.onInventory = false;
            }
            else
            {
                if (!paused)
                {
                    UIManager.Instance.EnablePauseMenu();
                    paused = true;
                }
                else
                {
                    UIManager.Instance.DisablePauseMenu();
                    paused = false;
                }
            }
        }
    }
}
