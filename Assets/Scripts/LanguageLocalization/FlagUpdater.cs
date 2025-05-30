using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

public class FlagUpdater : MonoBehaviour
{
    public LocalizedAsset<Sprite> localizedFlag;
    public Image targetImage;

    void OnEnable()
    {
        localizedFlag.AssetChanged += UpdateImage;
    }

    void OnDisable()
    {
        localizedFlag.AssetChanged -= UpdateImage;
    }

    void UpdateImage(Sprite newSprite)
    {
        if (targetImage != null && newSprite != null)
        {
            targetImage.sprite = newSprite;
        }
    }
}
