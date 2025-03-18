using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public GameObject panel;
    public Color glowColor = new Color(1f, 1f, 0.5f, 1f);
    private Color originalColor;
    private UnityEngine.UI.Image panelImage;

    // Start is called before the first frame update
    void Start()
    {
        panelImage = panel.GetComponent<UnityEngine.UI.Image>();
        if (panelImage != null)
        originalColor = panelImage.color; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (panelImage != null)
            panelImage.color = glowColor; 

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (panelImage != null)
            panelImage.color = originalColor; 

        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); 
    }
}
