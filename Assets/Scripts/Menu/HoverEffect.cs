using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class HoverEffect : MonoBehaviour
{
    public List<GameObject> uiElementsList;
    public List<GameObject> ignorePointerClickReset;
    private GameObject currentUIElement;
    public Color glowColor = new Color(1f, 1f, 0.5f, 1f);
    private Dictionary<GameObject, Color> originalColors = new Dictionary<GameObject, Color>();

    MenuSoundsManager soundsManager;
    // Start is called before the first frame update
    void Start()
    {
        soundsManager=FindObjectOfType<MenuSoundsManager>();
        foreach (GameObject uiElement in uiElementsList)
        {
            AddEventTriggers(uiElement);
        }
    } 
    

    private void AddEventTriggers(GameObject uiElement)
    {
        EventTrigger trigger = uiElement.gameObject.AddComponent<EventTrigger>();

        if (trigger == null)
        {
            trigger = uiElement.AddComponent<EventTrigger>();
        }

        Image panelImage = uiElement.GetComponentInChildren<Image>(); 
        
        if (panelImage != null && !originalColors.ContainsKey(uiElement))
        {
            originalColors[uiElement] = panelImage.color;
        }

        // Event OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(uiElement); });
        trigger.triggers.Add(entryEnter);

        // Event OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(uiElement); });
        trigger.triggers.Add(entryExit);

        // Event OnPointerClick
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnPointerClick(uiElement); });
        trigger.triggers.Add(entryClick);
    }


    public void OnPointerEnter(GameObject uiElement)
    {
        Button btn = uiElement.GetComponent<Button>();
        if (btn != null && !btn.interactable)
            return;

        InventoryMenu inventoryMenu = FindObjectOfType<InventoryMenu>();
        if (uiElement.name.Contains("ArrowButton_01") && !inventoryMenu.CanGoLeft()) return;
        if (uiElement.name.Contains("ArrowButton_02") && !inventoryMenu.CanGoRight()) return;

        Image panelImage = uiElement.GetComponentInChildren<Image>();
        if (panelImage != null)
        {
            soundsManager.PlayHoverSound();
            panelImage.color = glowColor; 
        }
    }

    public void OnPointerExit(GameObject uiElement)
    {
        Button btn = uiElement.GetComponent<Button>();
        if (btn != null && !btn.interactable)
            return;

        if (originalColors.ContainsKey(uiElement))
        {
            Image panelImage = uiElement.GetComponentInChildren<Image>();
            if (panelImage != null)
            {
                panelImage.color = originalColors[uiElement]; 
            }
        }

        if (currentUIElement == uiElement)
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            currentUIElement = null;
        }
    }


    public void OnPointerClick(GameObject uiElement)
    {
        Button btn = uiElement.GetComponent<Button>();
        if (btn != null && !btn.interactable)
            return;

        InventoryMenu inventoryMenu = FindObjectOfType<InventoryMenu>();
        if (uiElement.name.Contains("ArrowButton_01") && !inventoryMenu.CanGoLeft()) return;
        if (uiElement.name.Contains("ArrowButton_02") && !inventoryMenu.CanGoRight()) return;

        OnPointerExit(uiElement);

        StartCoroutine(RestoreHoverNextFrame(uiElement));
    }

    private IEnumerator RestoreHoverNextFrame(GameObject uiElement)
    {
        yield return null;
        
        if (IsPointerOver(uiElement))
        {
            OnPointerEnter(uiElement);
        }
    }

    private bool IsPointerOver(GameObject obj)
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue()
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject == obj || result.gameObject.transform.IsChildOf(obj.transform))
            {
                return true;
            }
        }

        return false;
    }


    public void ResetAllHoverEffects()
    {
        foreach (GameObject uiElement in uiElementsList)
        {
            if (originalColors.ContainsKey(uiElement))
            {
                Image panelImage = uiElement.GetComponentInChildren<Image>();
                if (panelImage != null)
                {
                    panelImage.color = originalColors[uiElement];
                }
            }
        }
    }

}
