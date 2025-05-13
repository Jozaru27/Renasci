using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Script that changes the mouse design from cursor to pointer and back, whenever its hovering a button or whenever it stops.

public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorPointer;
    public List<GameObject> uiElementsList; 

    private GameObject currentUIElement;

    void Start()
    {
        foreach (GameObject uiElement in uiElementsList)
        {
            AddEventTriggers(uiElement);
        }
    }

    private void AddEventTriggers(GameObject uiElement)
    {
        EventTrigger trigger = uiElement.gameObject.AddComponent<EventTrigger>();

        // Event OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(uiElement); });
        trigger.triggers.Add(entryEnter);

        // Event OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryExit);

        // Evento OnPointerClick
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnPointerExit(); });
        trigger.triggers.Add(entryClick);
    }


    // Swaps the mouse cursor to the pointer design
    public void OnPointerEnter(GameObject uiElement)
    {
        Button btn = uiElement.GetComponent<Button>();
        if (btn != null && !btn.interactable)
        return;

        currentUIElement = uiElement;
        Cursor.SetCursor(cursorPointer, Vector2.zero, CursorMode.Auto);
    }


    // Swaps the mouse cursor back to its default design
    public void OnPointerExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    public void ResetCursor()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

}
