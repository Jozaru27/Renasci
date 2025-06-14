using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
    [Header("Stats Section")]
    [SerializeField] TMP_Text statsText;

    [Header("Info Section")]
    [SerializeField] GameObject infoSection;
    [SerializeField] GameObject statsSection;
    [SerializeField] Image relicImage;
    [SerializeField] TMP_Text nameText;
    [SerializeField] TMP_Text infoText;

    [Header("RelicSlots")]
    [SerializeField] GameObject[] passiveRelicButtons;
    [SerializeField] GameObject[] activeRelicButtons;
    [SerializeField] TMP_Text[] relicTexts;

    [Header("Arrow Buttons")]
    [SerializeField] Button arrowButtonLeft;
    [SerializeField] Button arrowButtonRight;

    int passiveButtonsNum;
    int activeButtonsNum;
    int relicNum;
    List<int> relicQuantity = new List<int>();
    List<RelicsInventoryScriptableObject> passiveRelicsInfo = new List<RelicsInventoryScriptableObject>();
    List<RelicsInventoryScriptableObject> activeRelicsInfo = new List<RelicsInventoryScriptableObject>();
    [SerializeField] PlayerInput input;
    GameObject lastSelected;

    List<string> passiveRelicNames = new List<string>();
    List<string> passiveRelicDescriptions = new List<string>();
    List<string> activeRelicNames = new List<string>();
    List<string> activeRelicDescriptions = new List<string>();

    public static InventoryMenu Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        UpdateArrowButtons();
    }

    private void Update()
    {
        if (input.currentControlScheme == "Gamepad" && EventSystem.current.currentSelectedGameObject != lastSelected)
        {
            infoSection.SetActive(false);
            statsSection.SetActive(true);
            lastSelected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void AddToInventory(string relName, string relDescription, RelicsInventoryScriptableObject relicInfo)
    {
        int iterations = 0;
        bool relicInList = false;

        if (relicInfo.relicType.ToString() == "Passive")
        {
            if (passiveRelicsInfo.Count != 0)
                foreach (RelicsInventoryScriptableObject info in passiveRelicsInfo)
                {
                    if (info == relicInfo)
                        relicInList = true;

                    if (!relicInList)
                        iterations++;
                }

            if (!relicInList)
            {
                passiveRelicsInfo.Add(relicInfo);
                passiveRelicNames.Add(relName);
                passiveRelicDescriptions.Add(relDescription);

                if (passiveButtonsNum <= 2)
                {
                    passiveRelicButtons[passiveButtonsNum].GetComponent<Image>().sprite = relicInfo.image;
                    passiveRelicButtons[passiveButtonsNum].GetComponent<Image>().color = Color.white;
                    relicTexts[passiveButtonsNum].gameObject.SetActive(true);
                }

                passiveButtonsNum++;
                relicQuantity.Add(1);
            }
            else
            {
                relicQuantity[iterations]++;

                if (iterations >= relicNum && iterations <= relicNum + 2)
                    relicTexts[iterations - relicNum].text = $"x{relicQuantity[iterations]}";
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
                activeRelicNames.Add(relName);
                activeRelicDescriptions.Add(relDescription);

                activeRelicButtons[activeButtonsNum].GetComponent<Image>().sprite = relicInfo.image;
                activeRelicButtons[activeButtonsNum].GetComponent<Image>().color = Color.white;
                activeButtonsNum++;

                if (activeButtonsNum > 2)
                    activeButtonsNum = 2;
            }
        }

        UpdateArrowButtons();
    }

    public void PassiveButton(int button)
    {
        if (passiveRelicsInfo.Count >= (button + 1 + relicNum))
        {
            infoSection.SetActive(true);
            statsSection.SetActive(false);

            relicImage.sprite = passiveRelicsInfo[button + relicNum].image;
            //nameText.text = passiveRelicsInfo[button + relicNum].relicName;
            //infoText.text = passiveRelicsInfo[button + relicNum].description + "\n\n <size=25>" + passiveRelicsInfo[button + relicNum].effect + "<color=#00ff00ff>" + passiveRelicsInfo[button + relicNum].value + passiveRelicsInfo[button + relicNum].valueQuantity + "</color><color=#ffBf58ff> (" + passiveRelicsInfo[button + relicNum].value + (passiveRelicsInfo[button + relicNum].valueQuantity * relicQuantity[button + relicNum]).ToString() + ")</color></size>";
            nameText.text = passiveRelicNames[button + relicNum];
            infoText.text = passiveRelicDescriptions[button + relicNum] + " <color=#00ff00ff>" + passiveRelicsInfo[button + relicNum].value + passiveRelicsInfo[button + relicNum].valueQuantity + "</color><color=#ffBf58ff> (" + passiveRelicsInfo[button + relicNum].value + (passiveRelicsInfo[button + relicNum].valueQuantity * relicQuantity[button + relicNum]).ToString() + ")</color></size>";
        }
    }

    public void ActiveButton(int button)
    {
        if (activeRelicsInfo.Count >= (button + 1))
        {
            infoSection.SetActive(true);
            statsSection.SetActive(false);

            relicImage.sprite = activeRelicsInfo[button].image;
            //nameText.text = activeRelicsInfo[button].relicName;
            //infoText.text = activeRelicsInfo[button].description + "\n\n <size=25>" + activeRelicsInfo[button].effect + "<color=#00ff00ff>" + activeRelicsInfo[button].value + "</color></size>";
            nameText.text = activeRelicNames[button];
            infoText.text = activeRelicDescriptions[button];
        }
    }

    public void DisableInfo()
    {
        infoSection.SetActive(false);
        statsSection.SetActive(true);
    }

    public void GoRight()
    {
        relicNum++;

        if (relicNum > passiveRelicsInfo.Count - 3)
            relicNum--;

        if (passiveRelicsInfo.Count > 3)
            for (int i = 0; i < 3; i++)
            {
                passiveRelicButtons[i].GetComponent<Image>().sprite = passiveRelicsInfo[i + relicNum].image;
                relicTexts[i].text = $"x{relicQuantity[i + relicNum]}";
            }
            UpdateArrowInteractivity();
    }



    public void GoLeft()
    {
        relicNum--;

        if (relicNum < 0)
            relicNum = 0;

        if (passiveRelicsInfo.Count > 3)
            for (int i = 0; i < 3; i++)
            {
                passiveRelicButtons[i].GetComponent<Image>().sprite = passiveRelicsInfo[i + relicNum].image;
                relicTexts[i].text = $"x{relicQuantity[i + relicNum]}";
            }
            UpdateArrowInteractivity();
    }



    public void UpdateStats()
    {
        statsText.text = $"{StatsManager.Instance.maxLife:0.00} \n\n" +
            $"{StatsManager.Instance.life:0.00} \n\n" +
            $"{StatsManager.Instance.lifeRegeneration:0.00} \n\n" +
            $"{StatsManager.Instance.damage:0.00} \n\n" +
            $"{StatsManager.Instance.damageMultiplyer:0.00} \n\n" +
            $"{StatsManager.Instance.criticalChance:0.00} \n\n" +
            $"{StatsManager.Instance.movementSpeed:0.00} \n\n" +
            $"{StatsManager.Instance.attackSpeed:0.00} \n\n" +
            $"{StatsManager.Instance.shootCadence:0.00} \n\n" +
            $"{StatsManager.Instance.dashCooldown:0.00} \n\n" +
            $"{StatsManager.Instance.evasion:0.00} \n\n";
    }

    public void DisableInventory()
    {
        infoSection.SetActive(false);
        statsSection.SetActive(true);

        CursorChanger cursorChanger = FindObjectOfType<CursorChanger>();
        if (cursorChanger != null)
        {
            cursorChanger.ResetCursor();
        }

        HoverEffect hoverEffect = FindObjectOfType<HoverEffect>();
        if (hoverEffect != null)
        {
            hoverEffect.ResetAllHoverEffects();
        }
    }

    private void UpdateArrowButtons()
    {
        bool hasEnoughRelics = passiveRelicsInfo.Count >= 4;

        arrowButtonLeft.interactable = hasEnoughRelics;
        arrowButtonRight.interactable = hasEnoughRelics;

        SetButtonOpacity(arrowButtonLeft.gameObject, hasEnoughRelics ? 1f : 0.3f);
        SetButtonOpacity(arrowButtonRight.gameObject, hasEnoughRelics ? 1f : 0.3f);
    }

    private void SetButtonOpacity(GameObject buttonGO, float alpha)
    {
        Image[] images = buttonGO.GetComponentsInChildren<Image>(true);
        foreach (Image img in images)
        {
            Color c = img.color;
            c.a = alpha;
            img.color = c;
        }

        TMPro.TextMeshProUGUI[] texts = buttonGO.GetComponentsInChildren<TMPro.TextMeshProUGUI>(true);
        foreach (var text in texts)
        {
            Color c = text.color;
            c.a = alpha;
            text.color = c;
        }
    }

    public bool CanGoLeft()
    {
        return relicNum > 0;
    }

    public bool CanGoRight()
    {
        return relicNum < passiveRelicsInfo.Count - 3;
    }

    public void ToggleInfo(int button)
    {
        if (input.currentControlScheme == "Gamepad")
        {
            infoSection.SetActive(!infoSection.activeSelf);
            statsSection.SetActive(!statsSection.activeSelf);

            if (button < 3)
            {
                relicImage.sprite = passiveRelicsInfo[button + relicNum].image;
                //nameText.text = passiveRelicsInfo[button + relicNum].relicName;
                //infoText.text = passiveRelicsInfo[button + relicNum].description + "\n\n <size=25>" + passiveRelicsInfo[button + relicNum].effect + "<color=#00ff00ff>" + passiveRelicsInfo[button + relicNum].value + passiveRelicsInfo[button + relicNum].valueQuantity + "</color><color=#ffBf58ff> (" + passiveRelicsInfo[button + relicNum].value + (passiveRelicsInfo[button + relicNum].valueQuantity * relicQuantity[button + relicNum]).ToString() + ")</color></size>";
                nameText.text = passiveRelicNames[button + relicNum];
                infoText.text = passiveRelicDescriptions[button + relicNum] + " <color=#00ff00ff>" + passiveRelicsInfo[button + relicNum].value + passiveRelicsInfo[button + relicNum].valueQuantity + "</color><color=#ffBf58ff> (" + passiveRelicsInfo[button + relicNum].value + (passiveRelicsInfo[button + relicNum].valueQuantity * relicQuantity[button + relicNum]).ToString() + ")</color></size>";
            }
            else
            {
                button -= 3;
                relicImage.sprite = activeRelicsInfo[button].image;
                //nameText.text = activeRelicsInfo[button].relicName;
                //infoText.text = activeRelicsInfo[button].description + "\n\n <size=25>" + activeRelicsInfo[button].effect + "<color=#00ff00ff>" + activeRelicsInfo[button].value + "</color></size>";
                nameText.text = activeRelicNames[button];
                infoText.text = activeRelicDescriptions[button];
            }
        }
    }

    private void ForceExitHover(GameObject target)
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            pointerEnter = null
        };

        ExecuteEvents.Execute(target, pointerEventData, ExecuteEvents.pointerExitHandler);
    }


        private void UpdateArrowInteractivity()
    {
        bool canGoLeft = CanGoLeft();
        bool canGoRight = CanGoRight();

        arrowButtonLeft.interactable = canGoLeft;
        arrowButtonRight.interactable = canGoRight;

        SetButtonOpacity(arrowButtonLeft.gameObject, canGoLeft ? 1f : 0.3f);
        SetButtonOpacity(arrowButtonRight.gameObject, canGoRight ? 1f : 0.3f);

        if (!canGoLeft)
            ForceExitHover(arrowButtonLeft.gameObject);
        if (!canGoRight)
            ForceExitHover(arrowButtonRight.gameObject);
    }

}