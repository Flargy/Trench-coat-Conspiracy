using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class InventorySlot
    {
        private InventoryItem _inventoryItem;
        private Image         _icon;
        private Button        _button;

        public GameObject SlotPrefab { get; private set; }
        public Image Icon => _icon;
        public InventoryItem InventoryItem => _inventoryItem;

        public void Init(GameObject slotPrefab)
        {
            this.SlotPrefab = slotPrefab;
            _button = slotPrefab.GetComponentInChildren<Button>();
            _icon = slotPrefab.GetComponentInChildren<Image>();
        }

        public void AddItem(InventoryItem newInventoryItem)
        {
            _inventoryItem = newInventoryItem;

            _icon.sprite = newInventoryItem.displayIcon;
            _icon.enabled = true;
            _button.interactable = true;
        }

        public void ClearSlot()
        {
            _inventoryItem = null;
            _icon.sprite = null;
            _icon.enabled = false;
            _button.interactable = false;
        }
    }
}