using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateNewRelic : MonoBehaviour, IInteractable
{
    bool firstTime;
    GameObject playerObj;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
    }

    public void Interact()
    {
        if (!firstTime)
        {
            GameManager.Instance.currentRelicSlots++;

            if (GameManager.Instance.currentRelicSlots == 0)
            {
                UIManager.Instance.ChangeRelicInfo("Fire");
                playerObj.GetComponent<Attack>().currentRelic = Attack.Relics.Fire;
            }
                
            firstTime = true;
        }
    }
}
