using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem
{
    public class Inventory : MonoBehaviour
    {
        #region SingletonData

        private static Inventory _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        #endregion

        public delegate void ItemAdded();

        public delegate void HoldingItem(int i);

        public static event ItemAdded UpdateItems;
        public static event HoldingItem UpdateHoldingItem;

        private InventoryItem _currentlyHeldItem;
        private bool          _isHoldingItem = false;

        [SerializeField] private List<InventoryItem> _inventoryItems = new List<InventoryItem>();
        [SerializeField] private int                 _maxItems       = 6;
        [SerializeField] private bool                _hasKey         = false; // first playable only

        public static int MaxItems => _instance._maxItems;
        public static InventoryItem CurrentlyHeldItem => _instance._currentlyHeldItem;
        public static List<InventoryItem> InventoryItems => _instance._inventoryItems;

        public static void UpdateUI()
        {
            UpdateItems?.Invoke();
        }

        public static void AddItem(InventoryItem item)
        {
            _instance._inventoryItems.Add(item);

            if (item.isKey) // first playable only
            {
                _instance._hasKey = true;
            }

            UpdateUI();
        }

        public static void DiscardItem(InventoryItem item)
        {
            ResetItemHeld();
            RemoveItemFromList(item);
        }

        public static void ReleaseHeldItem()
        {
            HoldItem(-1);
        }

        public static bool CanProgress() // first playable only
        {
            return _instance._hasKey;
        }

        public static void HoldItem(int indexOfIncomingItem)
        {
            print($"first.is holding item: {_instance._isHoldingItem}");
            if (indexOfIncomingItem < 0)
            {
                ResetItemHeld();
                return;
            }

            InventoryItem incomingItem = _instance._inventoryItems[indexOfIncomingItem];

            if (_instance._isHoldingItem)
            {
                CombineItems(incomingItem);
                ResetItemHeld();
                print($"if.is holding item: {_instance._isHoldingItem}");
            }
            else if (incomingItem is InspectableInventoryItem)
            {
                InspectableInventoryItem item = (InspectableInventoryItem)incomingItem;
                ActionType act = null;
                Mesh itemMesh = item.itemPrefab.GetComponent<MeshFilter>().sharedMesh;
                Material[] itemMats = item.itemPrefab.GetComponent<MeshRenderer>().sharedMaterials;
                if(item.itemPrefab.TryGetComponent(out ActionType action))
                {
                    act = action;
                }
                Inspection.ItemInspection.StartInspection(itemMesh, item.clueNormal, Quaternion.identity, "", itemMats, act);
            }
            else
            {
                _instance._currentlyHeldItem = incomingItem;
                print($"else.is holding item: {_instance._isHoldingItem}");
                _instance._isHoldingItem = true;
                UpdateHoldingItem?.Invoke(indexOfIncomingItem);
            }
        }

        public static bool CheckInventoryForItem(InventoryItem keyItem)
        {
            return _instance._inventoryItems.Contains(keyItem);
        }

        public static InventoryItem GetHeldItem()
        {
            return _instance._currentlyHeldItem;
        }

        private static void CombineItems(InventoryItem item)
        {
            if (CompareItems(item))
            {
                print("It's a match!");
                RemoveItemFromList(item);
                RemoveItemFromList(_instance._currentlyHeldItem);
                AddItem(_instance._currentlyHeldItem.combineResultItem);

                ResetItemHeld();
            }
            else
            {
                print("Those don't go together!");
            }
        }

        private static void RemoveItemFromList(InventoryItem item)
        {
            _instance._inventoryItems.Remove(item);
            UpdateUI();
        }

        private static void RemoveItemFromList(int index)
        {
            _instance._inventoryItems.RemoveAt(index);
            UpdateUI();
        }

        private static bool CompareItems(InventoryItem item) =>
            _instance._currentlyHeldItem == item.combinesWithItem;

        private static void ResetItemHeld()
        {
            _instance._currentlyHeldItem = null;
            _instance._isHoldingItem = false;
            UpdateHoldingItem?.Invoke(-1);
        }
    }
}