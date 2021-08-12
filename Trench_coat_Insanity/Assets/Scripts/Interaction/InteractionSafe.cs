using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UserInterface;

namespace Interaction
{
    public class InteractionSafe : MonoBehaviour, IInteractable
    {
        private SafeInteractionUI _safeInteractionUI = null;

        [SerializeField] private bool                isLocked = true;
        [SerializeField] private string              passcode;
        [SerializeField] private List<InventoryItem> safeInventory;
        [SerializeField] private GameObject          safePrefab;
        [SerializeField] private Transform           uiCanvasTransform;
        [SerializeField] private ActionType          voiceLineForPasscode;
        [SerializeField] private ActionType[]        actionsOnSafeEmpty;

        public List<InventoryItem> SafeInventory => safeInventory;
        public bool IsLocked => isLocked;

        public void Interact()
        {
            if (ReferenceEquals(_safeInteractionUI, null))
            {
                safePrefab = Instantiate(safePrefab, uiCanvasTransform);
                _safeInteractionUI = safePrefab.GetComponent<SafeInteractionUI>();
                _safeInteractionUI.Init(this);
            }
            else
            {
                ShowUI(true);
            }
        }

        // todo: write the game win logic in this script instead of in <SafeInteractionUI>.
        public bool ComparePasscode(string incomingPasscode)
        {
            bool check = incomingPasscode == passcode;
            if(check && voiceLineForPasscode)
                voiceLineForPasscode.Activate();
            return check;
        }

        public void AddItemToInventory(int inventoryIndex)
        {
            InventorySystem.Inventory.AddItem(safeInventory[inventoryIndex]);
            safeInventory.RemoveAt(inventoryIndex);

            if (safeInventory.Count > 0) return;
            foreach (ActionType action in actionsOnSafeEmpty)
            {
                action.Activate();
            }
        }

        public void ShowUI(bool shouldShow)
        {
            safePrefab.SetActive(shouldShow);
        }

        #region Editor functions
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (!OnlyDigits(passcode))
                passcode = Regex.Replace(passcode, "[^0-9]", "");
            if (passcode.Length > 6)
                passcode = passcode.Substring(0, 6);
            if (safeInventory.Count > 9)
                safeInventory.RemoveRange(9, safeInventory.Count - 9);
            if (!voiceLineForPasscode)
                Debug.LogWarning("<b>InteractionSafe: Voice Line (Action)</b> is missing a reference!");
                
        }

        private bool OnlyDigits(string str)
        {
            foreach (char c in str)
            {
                if (c >= 0 || c <= 9) return false;
            }

            return true;
        }
#endif
        #endregion
    }
}