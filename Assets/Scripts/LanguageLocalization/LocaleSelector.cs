using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Localization.Settings;

public class LocaleSelector : MonoBehaviour
{
    private bool active = false;

    public TMP_Dropdown languageDropdown;

    void Start()
    {
        var currentLocale = LocalizationSettings.SelectedLocale;
        int index = LocalizationSettings.AvailableLocales.Locales.FindIndex(locale => locale == currentLocale);
        languageDropdown.value = index;
        languageDropdown.RefreshShownValue();
    }
        
    public void ChangeLocale(int localeID)
    {
        if (active == true)
            return;
        StartCoroutine(SetLocale(localeID));
    }

    IEnumerator SetLocale(int _localeID)
    {
        active = true;
        yield return LocalizationSettings.InitializationOperation;
        LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[_localeID];

        active = false;
    }
}
