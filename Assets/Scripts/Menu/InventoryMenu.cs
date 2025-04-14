using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [Header("Info Section")]
    [SerializeField] GameObject infoSection;
    [SerializeField] Image relicImage;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;

    [Header("RelicSlots")]
    [SerializeField] GameObject[] passiveRelicButtons;
    [SerializeField] GameObject[] activeRelicButtons;

    int passiveButtonsNum;
    int activeButtonsNum;
    List<RelicsInventoryScriptableObject> passiveRelicsInfo = new List<RelicsInventoryScriptableObject>();
    List<RelicsInventoryScriptableObject> activeRelicsInfo = new List<RelicsInventoryScriptableObject>();

    public static InventoryMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddToInventory(RelicsInventoryScriptableObject relicInfo)
    {
        bool relicInList = false;

        if (relicInfo.relicType.ToString() == "Passive")
        {
            if (passiveRelicsInfo.Count != 0)
                foreach (RelicsInventoryScriptableObject info in passiveRelicsInfo)
                {
                    if (info == relicInfo)
                        relicInList = true;
                }

            if (!relicInList)
            {
                passiveRelicsInfo.Add(relicInfo);

                if (passiveButtonsNum <= 2)
                    passiveRelicButtons[passiveButtonsNum].GetComponent<Image>().sprite = relicInfo.image;

                passiveButtonsNum++;
            }
        }
        else
        {
            if (activeRelicsInfo.Count != 0)
                foreach (RelicsInventoryScriptableObject info in activeRelicsInfo)
                {
                    if (info == relicInfo)
                        relicInList = true;
                }

            if (!relicInList)
            {
                activeRelicsInfo.Add(relicInfo);
                activeRelicButtons[activeButtonsNum].GetComponent<Image>().sprite = relicInfo.image;
                activeButtonsNum++;

                if (activeButtonsNum > 2)
                    activeButtonsNum = 2;
            }
        }
    }

    public void PassiveButton(int button)
    {
        if (passiveRelicsInfo.Count >= (button + 1))
        {
            infoSection.SetActive(true);

            relicImage.sprite = passiveRelicsInfo[button].image;
            nameText.text = passiveRelicsInfo[button].relicName;
            infoText.text = passiveRelicsInfo[button].description + "\n\n <size=25>" + passiveRelicsInfo[button].effect + "<color=#00ff00ff>" + passiveRelicsInfo[button].value + "</color></size>";
        }
    }

    public void ActiveButton(int button)
    {
        if (activeRelicsInfo.Count >= (button + 1))
        {
            infoSection.SetActive(true);

            relicImage.sprite = activeRelicsInfo[button].image;
            nameText.text = activeRelicsInfo[button].relicName;
            infoText.text = activeRelicsInfo[button].description + "\n\n <size=25>" + activeRelicsInfo[button].effect + "<color=#00ff00ff>" + activeRelicsInfo[button].value + "</color></size>";
        }
    }

    public void DisableInfo()
    {
        infoSection.SetActive(false);
    }
}
