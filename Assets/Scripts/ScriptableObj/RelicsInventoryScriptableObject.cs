using UnityEngine;

[CreateAssetMenu(fileName = "RelicInventory", menuName = "ScriptableObjects/RelicInventory")]
public class RelicsInventoryScriptableObject : ScriptableObject
{
    public Sprite image;
    public string relicName;
    [TextArea (2, 6)] public string description;
    [TextArea (2, 6)] public string effect;
    public string value;
    public float valueQuantity;

    public enum Relics
    {
        Passive,
        Active
    }
    public Relics relicType;
}
