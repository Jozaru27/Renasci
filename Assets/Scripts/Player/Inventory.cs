using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        if (context.started && !GameManager.Instance.gameOver && !GameManager.Instance.gameWin && !GameManager.Instance.inInfo && !GameManager.Instance.gamePaused)
        {
            if (!GameManager.Instance.onInventory)
            {
                UIManager.Instance.EnableInventoryMenu();
                GameManager.Instance.onInventory = true;
            }
            else
            {
                UIManager.Instance.DisableInventoryMenu();
                InventoryMenu.Instance.DisableInventory();
                GameManager.Instance.onInventory = false;
            }
        }
    }
}
