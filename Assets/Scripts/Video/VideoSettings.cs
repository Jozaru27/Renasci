using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private List<Vector2Int> allowedResolutions = new List<Vector2Int>
    {
        new Vector2Int(1920, 1080),
        new Vector2Int(1600, 900),
        new Vector2Int(1366, 768),
        new Vector2Int(1280, 720)
    };

    void Start()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < allowedResolutions.Count; i++)
        {
            Vector2Int res = allowedResolutions[i];
            string resString = res.x + "x" + res.y;
            options.Add(resString);

            if (Screen.width == res.x && Screen.height == res.y)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        PlayerPrefs.SetInt("Fullscreen", isFullscreen ? 1 : 0);

        if (isFullscreen)
        {
            Screen.SetResolution(Screen.width, Screen.height, true);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }

        Debug.Log("Fullscreen mode set to: " + Screen.fullScreenMode);
    }

    private void ChangeResolution(int index)
    {
        Vector2Int selectedRes = allowedResolutions[index];
        Screen.SetResolution(selectedRes.x, selectedRes.y, Screen.fullScreen);
        
        if (Screen.fullScreen)
        {
            Screen.SetResolution(selectedRes.x, selectedRes.y, true);
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            StartCoroutine(RefreshFullscreen());
        }
        else
            Screen.fullScreenMode = FullScreenMode.Windowed;

        Debug.Log($"Changed resolution to: {selectedRes.x}x{selectedRes.y}, fullscreen mode: {Screen.fullScreenMode}");
    }

    private IEnumerator RefreshFullscreen()
    {
        Screen.fullScreen = false;
        yield return null; 
        Screen.fullScreen = true;
    }
}