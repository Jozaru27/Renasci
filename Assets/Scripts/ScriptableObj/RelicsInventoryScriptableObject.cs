using UnityEngine;

[CreateAssetMenu(fileName = "RelicInventory", menuName = "ScriptableObjects/RelicInventory")]
public class RelicsInventoryScriptableObject : ScriptableObject
{
    public Sprite image;
    public string relicName;
    public string description;
    public string effect;
}
