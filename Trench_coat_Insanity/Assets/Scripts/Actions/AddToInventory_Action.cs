using InventorySystem;
using UnityEngine;

/**
 * Adds an item to the inventory and then hides it in the scene.
 */
public class AddToInventory_Action : ActionType
{
    [SerializeField] private InventoryItem _itemToAdd;
    [SerializeField] private GameObject _objectToHide;

    public override void Activate()
    {
        Inventory.AddItem(_itemToAdd);
        _objectToHide.SetActive(false);
    }
}
