using InventorySystem;
using UnityEngine;

/**
 * Adds an item to the inventory and then removes it from the scene.
 */
public class InspectionToInventory_Action : ActionType
{
    [SerializeField] private InventoryItem _inventoryItem;
    public override void Activate()
    {
        Inventory.AddItem(_inventoryItem);
        
        Destroy(gameObject);
    }
}
