using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRelicInventory : MonoBehaviour
{
    [SerializeField] RelicsInventoryScriptableObject relicInfo;

    public void PassInfoToInventory()
    {
        InventoryMenu.Instance.AddToInventory(relicInfo);

        if (relicInfo.relicType.ToString() == "Passive")
        {
            switch (relicInfo.relicName)
            {
                case "Crimson Fury":
                    if (!RelicInfoManager.Instance.crimsonFury)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.crimsonFury = true;
                    }

                    break;
                case "Dance Of The Shadows":
                    if (!RelicInfoManager.Instance.danceOfTheShadows)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.danceOfTheShadows = true;
                    }

                    break;
                case "Eye Of The Falcon":
                    if (!RelicInfoManager.Instance.eyeOfTheFalcon)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.eyeOfTheFalcon = true;
                    }

                    break;
                case "Hand Of The Gunner":
                    if (!RelicInfoManager.Instance.handOfTheGunner)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.handOfTheGunner = true;
                    }

                    break;
                case "Ilusory Track":
                    if (!RelicInfoManager.Instance.ilusoryTrack)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.ilusoryTrack = true;
                    }

                    break;
                case "Roar Of The Thunder":
                    if (!RelicInfoManager.Instance.roarOfTheThunder)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.roarOfTheThunder = true;
                    }

                    break;
                case "Sword Of The Fallen":
                    if (!RelicInfoManager.Instance.swordOfTheFallen)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.swordOfTheFallen = true;
                    }

                    break;
                case "Titanium Heart":

                    if (!RelicInfoManager.Instance.titaniumHeart)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.titaniumHeart = true;
                    }

                    break;
                case "Tree Of Eternity":
                    if (!RelicInfoManager.Instance.treeOfEternity)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.treeOfEternity = true;
                    }

                    break;
                case "Wings Of The Wind":
                    if (!RelicInfoManager.Instance.wingsOfTheWind)
                    {
                        InfoPanel.Instance.ImageTextInfo(relicInfo.description + "\n\n" + relicInfo.effect + relicInfo.value + relicInfo.valueQuantity, relicInfo.image, 2.5f);
                        RelicInfoManager.Instance.wingsOfTheWind = true;
                    }

                    break;
            }

            InventoryMenu.Instance.UpdateStats();
            Destroy(this.gameObject);
        }
    }
}
