using UnityEngine;

[CreateAssetMenu(menuName = "InventoryItem")]
public class InventoryItem : ScriptableObject
{
    public string        displayName;
    public Sprite        displayIcon;
    public string        displayInformation;
    public bool          isKey = false; // only for first playable
    public InventoryItem combinesWithItem;
    public InventoryItem combineResultItem;
}
