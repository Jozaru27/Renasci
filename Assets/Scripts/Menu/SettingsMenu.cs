using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public GameObject panelAudio;
    public GameObject panelVideo;

    // Start is called before the first frame update
    void Start()
    {
        ShowAudioSettings();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowAudioSettings()
    {
        panelAudio.SetActive(true);
        panelVideo.SetActive(false);
    }


    public void ShowVideoSettings()
    {
        panelAudio.SetActive(false);
        panelVideo.SetActive(true);
    }
}