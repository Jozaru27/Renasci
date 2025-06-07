using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Altar : MonoBehaviour, IInteractable
{
    [Header("Relic Info")]
    public string relicName;
    public string relicDescription;

    [Header("Active Relic Info")]
    public string infoName;
    [TextArea(4,6)] public string infoDescription;

    [SerializeField] GameObject relicObj;
    [SerializeField] RelicsInventoryScriptableObject relicInfo;

    bool firstTime;
    GameObject playerObj;
    GameObject interactUI;
    Image interactImage;
    TMP_Text interactText;

    private void Start()
    {
        playerObj = GameObject.Find("Player");
        interactUI = GameObject.Find("--INTERACT_UI--");
        interactImage = GameObject.Find("InteractionImage").GetComponent<Image>();
        interactText = GameObject.Find("InteractionText").GetComponent<TMP_Text>();
    }

    public void Hold()
    {
        if (!firstTime)
        {
            StopAllCoroutines();
            StartCoroutine(SpawnInteractUI());
        }
    }

    public void Unhold()
    {
        if (!firstTime)
        {
            StopAllCoroutines();
            StartCoroutine(DespawnInteractUI());
        }
    }

    IEnumerator SpawnInteractUI()
    {
        interactUI.transform.position = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);

        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a < 1)
        {
            fadeColor.a += 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
    }

    IEnumerator DespawnInteractUI()
    {
        Color fadeColor = Color.white;
        fadeColor.a = interactImage.color.a;
        interactImage.color = fadeColor;
        interactText.color = fadeColor;

        while (interactImage.color.a > 0)
        {
            fadeColor.a -= 2 * Time.unscaledDeltaTime;
            interactImage.color = fadeColor;
            interactText.color = fadeColor;

            yield return null;
        }
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
            //InfoPanel.Instance.AddRelic(relicName, relicDescription, relicInfo, 1f);
            InfoPanel.Instance.AddTextWithImage(relicName, relicDescription, relicInfo.image, 1f);

            if (!GameManager.Instance.relicInfoObtained)
            {
                InfoPanel.Instance.AddText(infoName, infoDescription, 1f);
                GameManager.Instance.relicInfoObtained = true;
            }

            Destroy(relicObj);
            firstTime = true;
            StartCoroutine(DespawnInteractUI());
        }
    }

    public void ChangeName(string newName)
    {
        relicName = newName;
    }

    public void ChangeDescription(string newDescription)
    {
        relicDescription = newDescription;
    }

    public void ChangeInfoName(string newName)
    {
        infoName = newName;
    }

    public void ChangeInfoDescription(string newDescription)
    {
        infoDescription = newDescription;
    }
}
