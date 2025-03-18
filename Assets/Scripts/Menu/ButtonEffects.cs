using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonEffects : MonoBehaviour
{
    public List<Button> buttonsList;
    public Color glowColor = new Color(1f, 1f, 0.5f, 1f);
    private Color originalColor;
    private UnityEngine.UI.Image panelImage;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Button button in buttonsList)
        {
            AddEventTriggers(button);
        }
    } 
    

    private void AddEventTriggers(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        // Event OnPointerEnter
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnPointerEnter(button); });
        trigger.triggers.Add(entryEnter);

        // Event OnPointerExit
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnPointerExit(button); });
        trigger.triggers.Add(entryExit);

        // Event OnPointerClick
        EventTrigger.Entry entryClick = new EventTrigger.Entry();
        entryClick.eventID = EventTriggerType.PointerClick;
        entryClick.callback.AddListener((data) => { OnPointerClick(button); });
        trigger.triggers.Add(entryClick);
    }


    public void OnPointerEnter(Button button)
    {
        panelImage = button.GetComponentInChildren<UnityEngine.UI.Image>();

        if (panelImage != null)
        {
            originalColor = panelImage.color;
            panelImage.color = glowColor; // Aplica el glow
        }
    }

    public void OnPointerExit(Button button)
    {
        if (panelImage != null)
        {
            panelImage.color = originalColor; // Restaura el color original
        }
    }

    public void OnPointerClick(Button button)
    {
        if (panelImage != null)
        {
            panelImage.color = originalColor; // Restaura el color original al hacer clic
        }
    }
}
