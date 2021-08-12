using System.Collections.Generic;
using UnityEngine;

namespace Interaction
{
    public class InteractionDoorKey : MonoBehaviour, IInteractable
    {
        [SerializeField]         private List<ActionType>        _actions;
        [SerializeField]         private Collider      boxCollider;
        [Space] [SerializeField] private InventoryItem keyItem;
        public void Interact()
        {
            if (InventorySystem.Inventory.CheckInventoryForItem(keyItem))
            {
                foreach (ActionType action in _actions)
                {
                    action.Activate();
                }
                boxCollider.enabled = false;
            }
        }
    }
}