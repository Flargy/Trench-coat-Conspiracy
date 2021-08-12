using Interaction;
using UnityEngine;
using UnityEngine.UI;

namespace UserInterface
{
    public class SafeInteractionUI : MonoBehaviour
    {
        private int             _digitToChange = 0;
        private InteractionSafe _interactionSafe;

        [SerializeField] private GameObject keypadTf;
        [SerializeField] private GameObject inventoryUITf;
        
        [SerializeField] private Text[]     keypadDigits;

        [SerializeField] private Button[] inventoryButtons;
        [SerializeField] private Image[]  inventoryIcons;

        public void Init(InteractionSafe interactionSafe)
        {
            _interactionSafe = interactionSafe;

            UpdateUI();
            
            if (interactionSafe.IsLocked)
                ShowKeypad();
            else
                ShowInventory();
        }

        public void AddDigit(int digit)
        {
            if (_digitToChange >= keypadDigits.Length) return;
            keypadDigits[_digitToChange].text = digit.ToString();
            _digitToChange++;
        }

        public void ResetPasscode()
        {
            foreach (Text digit in keypadDigits)
            {
                digit.text = "*";
            }

            _digitToChange = 0;
        }

        public void TryPasscode()
        {
            string str = "";

            foreach (Text text in keypadDigits)
            {
                str += text.text;
            }

            if (_interactionSafe.ComparePasscode(str))
                ShowInventory();
            else
                ResetPasscode();
        }

        public void AddItemToInventory(int inventoryIndex)
        {
            _interactionSafe.AddItemToInventory(inventoryIndex);
            UpdateUI();
        }

        public void Close()
        {
            _interactionSafe.ShowUI(false);
        }

        private void UpdateUI()
        {
            for (int i = 0; i < inventoryButtons.Length; i++)
            {
                bool inventoryItemExists = i < _interactionSafe.SafeInventory.Count;

                inventoryIcons[i].sprite = inventoryItemExists
                    ? _interactionSafe.SafeInventory[i].displayIcon
                    : null;
                inventoryIcons[i].enabled = inventoryItemExists;
                inventoryButtons[i].interactable = inventoryItemExists;
            }
        }

        private void ShowKeypad()
        {
            keypadTf.SetActive(true);
            inventoryUITf.SetActive(false);
        }

        private void ShowInventory()
        {
            keypadTf.SetActive(false);
            inventoryUITf.SetActive(true);
        }
    }
}