using UnityEngine;

[CreateAssetMenu(menuName = "InspectableItem")]
public class InspectableInventoryItem : InventoryItem
{
    public GameObject itemPrefab;
    public Vector3 clueNormal;
}
