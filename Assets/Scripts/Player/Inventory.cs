using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip openInventory;
    public AudioClip closeInventory;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {

        if (context.started && !GameManager.Instance.gameOver && !GameManager.Instance.gameWin && !GameManager.Instance.inInfo && !GameManager.Instance.gamePaused && GameManager.Instance.gamePausable)
        {
            if (!GameManager.Instance.onInventory)
            {
                UIManager.Instance.EnableInventoryMenu();
                GameManager.Instance.onInventory = true;
                audioSource.PlayOneShot(openInventory, 5f);
            }
            else
            {
                UIManager.Instance.DisableInventoryMenu();
                InventoryMenu.Instance.DisableInventory();
                GameManager.Instance.onInventory = false;
                audioSource.PlayOneShot(openInventory, 5f);
            }
        }
    }
}
