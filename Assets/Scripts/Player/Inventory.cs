using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    bool inventory;

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gameOver && !GameManager.Instance.gameWin)
        {
            if (!GameManager.Instance.onInventory)
            {
                UIManager.Instance.EnableInventoryMenu();
                GameManager.Instance.onInventory = true;
            }
            else
            {
                UIManager.Instance.DisableInventoryMenu();
                GameManager.Instance.onInventory = false;
            }
        }
    }
}
