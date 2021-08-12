using System;
using InventorySystem;
using UnityEngine;

namespace Interaction
{
    
    public class InteractionPickup : MonoBehaviour, IInteractable/*, ICombinable*/
    {
        [SerializeField] private InventoryItem _item;

        private Audio_Action _action;

        public void Awake()
        {
            if(TryGetComponent(out Audio_Action comp))
                _action = comp;
        }

        public void Interact()
        {
            Inventory.AddItem(_item);
            if (_action != null)
            {
                _action.Activate();
            }
            Destroy(gameObject);
        }
        
        // public void Combine()
        // {
        //     if (Inventory.ItemsCanCombine(item))
        //     {
        //         Inventory.CombineItems(item);
        //         Destroy(gameObject);
        //     }
        //     else
        //     {
        //         print("Can't combine those items!");
        //     }
        // }
    }
}