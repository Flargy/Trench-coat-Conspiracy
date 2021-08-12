using InventorySystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace UserInterface
{
    public class UIMouseInteraction
    {
        private InventoryUI     _inventoryUI;
        private Transform       _mouseIconTransform;
        private Image           _mouseIconGO;
        private Image           _mouseIcon;
        private Vector2         _iconOffset;
        private TextMeshProUGUI _itemName;
        private TextMeshProUGUI _itemInfo;
        private bool            _isShowingIcon = false;
        private bool            _isShowingText = false;

        public void Init(UIManager uiManager) //Called in Start (UIManager)
        {
            _inventoryUI = uiManager.InventoryUI;
            _mouseIconTransform = uiManager.mouseIconTransform;
            _iconOffset = uiManager.MouseIconOffset;
            _itemName = uiManager.itemName;
            _itemInfo = uiManager.itemInfo;

            _mouseIconGO = _mouseIconTransform.GetComponent<Image>();
            _mouseIcon = _mouseIconTransform.GetChild(0).GetComponent<Image>();

            Inventory.UpdateHoldingItem += HoldItem;

            HideIcon();
            HideText();
        }

        public void Tick() //Called in Update (UIManager)
        {
            if (!_isShowingIcon && !_isShowingText) return;
            Vector2 mouseScreenPos = Input.mousePosition;
            _mouseIconTransform.position = mouseScreenPos + _iconOffset;
        }

        public void ShowText(InventoryItem item)
        {
            _itemName.SetText(item.displayName);
            _itemInfo.SetText(item.displayInformation);

            _itemName.enabled = true;
            _itemInfo.enabled = true;

            _isShowingText = true;
        }

        public void HideText()
        {
            _itemName.enabled = false;
            _itemInfo.enabled = false;

            _isShowingText = false;
        }

        private void HoldItem(int i)
        {
            if (i >= 0)
                ShowIcon(_inventoryUI.Slots[i]);
            else
                HideIcon();
        }

        private void ShowIcon(InventorySlot slot)
        {
            _mouseIcon.sprite = slot.Icon.sprite;

            _isShowingIcon = true;
            _mouseIcon.enabled = true;
            _mouseIconGO.enabled = true;
        }

        private void HideIcon()
        {
            _isShowingIcon = false;
            _mouseIcon.enabled = false;
            _mouseIconGO.enabled = false;
        }
        
        public void UnSubscribe()
        {
            Inventory.UpdateHoldingItem -= HoldItem;
        }
    }
}