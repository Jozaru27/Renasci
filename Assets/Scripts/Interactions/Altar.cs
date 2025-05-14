using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject relicObj;

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

            //if (GameManager.Instance.currentRelicSlots == 0)
            //{
            //    UIManager.Instance.ChangeRelicInfo("Fire");
            //    playerObj.GetComponent<Attack>().currentRelic = Attack.Relics.Fire;
            //}

            UIManager.Instance.UpdateRelicIcons(GameManager.Instance.currentRelicSlots);

            switch (GameManager.Instance.currentRelicSlots)
            {
                case 0:
                    UIManager.Instance.ChangeRelicInfo("Fire");
                    playerObj.GetComponent<Attack>().currentRelic = Attack.Relics.Fire;
                    playerObj.GetComponent<Attack>().relicSlot = 0;
                    break;
                case 1:
                    UIManager.Instance.ChangeRelicInfo("Ice");
                    playerObj.GetComponent<Attack>().currentRelic = Attack.Relics.Ice;
                    playerObj.GetComponent<Attack>().relicSlot = 1;
                    break;
                case 2:
                   UIManager.Instance.ChangeRelicInfo("Wind");
                    playerObj.GetComponent<Attack>().currentRelic = Attack.Relics.Wind;
                    playerObj.GetComponent<Attack>().relicSlot = 2;
                    break;
            }

            int currentSlot = playerObj.GetComponent<Attack>().relicSlot;
            UIManager.Instance.UpdateRelicRotation(currentSlot);

            GetComponent<AddRelicInventory>().PassInfoToInventory();
            Destroy(relicObj);
            firstTime = true;
        }
    }
}
