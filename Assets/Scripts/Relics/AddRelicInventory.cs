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
            InventoryMenu.Instance.UpdateStats();
            Destroy(this.gameObject);
        }
    }
}
