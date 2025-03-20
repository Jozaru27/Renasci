using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VideoSettings : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    private Resolution[] resolutions;

    void Start()
    {
        // Load Full Screen Value from Player Refs
        fullscreenToggle.isOn = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // Fills Drop Down with resolution
        resolutions = Screen.resolutions;

        List<string> options = new List<string>();

        foreach (var resolution in resolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);
        }
        
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);

        // Establish actual resolution
        int currentResolutionIndex = GetCurrentResolutionIndex();
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(ChangeResolution);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }

    private int GetCurrentResolutionIndex()
    {
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                return i;
            }
        }
        return 0;
    }

    private void ChangeResolution(int index)
    {
        Resolution selectedResolution = resolutions[index];
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}