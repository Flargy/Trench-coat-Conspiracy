using UnityEngine;
using InventorySystem;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class InventoryUI
    {
        public InventorySlot[] Slots { get; private set; }

        public void Init(UIManager uiManager, GameObject[] inventorySlots) //Called in Start (UIManager)
        {
            InitInventoryUI(inventorySlots.Length, inventorySlots);
            UpdateUI();
            Inventory.UpdateItems += UpdateUI;
        }

        private void InitInventoryUI(int inventorySize, GameObject[] slotPrefabs)
        {
            Slots = new InventorySlot[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                InventorySlot slot = new InventorySlot();
                slot.Init(slotPrefabs[i]);
                Slots[i] = slot;
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                if (i < Inventory.InventoryItems.Count)
                {
                    Slots[i].AddItem(Inventory.InventoryItems[i]);
                }
                else
                {
                    Slots[i].ClearSlot();
                }
            }
        }

        public void UnSubscribe()
        {
            Inventory.UpdateItems -= UpdateUI;
        }

        
    }
}