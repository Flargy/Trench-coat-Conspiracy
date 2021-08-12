using System.Collections.Generic;
using Interaction;
using InventorySystem;
using UnityEngine;

public class InteractionItem : MonoBehaviour, IInteractable
{
    [SerializeField] private InventoryItem _desiredItem;
    [SerializeField] private List<ActionType> _actionsOnSuccess;

    private bool hasBeenUsed = false;

    public void Interact()
    {
        if (hasBeenUsed == false)
        {
            if (Inventory.GetHeldItem() == _desiredItem)
            {
                hasBeenUsed = true;   
                foreach (ActionType action in _actionsOnSuccess)
                {
                    action.Activate();
                }
            }

        }
    }
}
