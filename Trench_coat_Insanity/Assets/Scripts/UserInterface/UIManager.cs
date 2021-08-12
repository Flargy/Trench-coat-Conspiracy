using System;
using InventorySystem;
using TMPro;
using UnityEngine;

namespace UserInterface
{
    public class UIManager : MonoBehaviour
    {
        private UIMouseInteraction _uiMouseInteraction;
        private FadeController     _fadeController;

        internal InventoryUI InventoryUI;
        internal Vector2     MouseIconOffset;

        [SerializeField, Range(0, 100)]  private float mouseIconOffsetX;
        [SerializeField, Range(0, -100)] private float mouseIconOffsetY;

        public Transform       inventoryItemsParent;
        public GameObject      inventorySlotPrefab;
        public Transform       mouseIconTransform;
        public TextMeshProUGUI itemName;
        public TextMeshProUGUI itemInfo;
        public Animator        fadeAnimator;

        private void Awake()
        {
            InventoryUI = new InventoryUI();
            _uiMouseInteraction = new UIMouseInteraction();
            _fadeController = new FadeController();
        }

        private void Start()
        {
            GameObject[] slots = GetInventorySlots();
            MouseIconOffset = new Vector2(mouseIconOffsetX, mouseIconOffsetY);

            InventoryUI.Init(this, slots);
            _uiMouseInteraction.Init(this);
            _fadeController.Init(this);
        }

        private void Update()
        {
            _uiMouseInteraction.Tick();

            if (Input.GetKeyDown(KeyCode.Space))
                Inventory.UpdateUI();
        }

        private GameObject[] GetInventorySlots()
        {
            if (inventoryItemsParent == null)
            {
                throw new MissingReferenceException(
                    $"<b><color=red> {name} </color></b> is missing a Reference: <b><color=red> Inventory Items Parent </color></b> needs a Reference in the inspector!");
            }

            int          childCount = inventoryItemsParent.childCount;
            Transform    tf         = inventoryItemsParent.transform;
            GameObject[] slots      = new GameObject[childCount];

            for (int i = 0; i < childCount; i++)
            {
                GameObject slot = tf.GetChild(i).gameObject;
                slots[i] = slot;
            }

            return slots;
        }

        public void SetCurrentInventoryItem(int index) => Inventory.HoldItem(index);

        public void ShowMouseText(int index) =>
            _uiMouseInteraction.ShowText(InventoryUI.Slots[index].InventoryItem);

        public void HideMouseText() => _uiMouseInteraction.HideText();

        private void OnValidate()
        {
            if (inventoryItemsParent == null)
                Debug.LogWarning(
                    $"<b><color=yellow> {name} </color></b> is missing a Reference: <b><color=yellow> Inventory Items Parent </color></b> needs a Reference in the inspector!");

            if (inventorySlotPrefab == null)
                Debug.LogWarning(
                    $"<b><color=yellow> {name} </color></b> is missing a Reference: <b><color=yellow> Inventory Slot Prefab </color></b> needs a Reference in the inspector!");

            if (mouseIconTransform == null)
                Debug.LogWarning(
                    $"<b><color=yellow> {name} </color></b> is missing a Reference: <b><color=yellow> Mouse Icon Transform </color></b> needs a Reference in the inspector!");
        }

        public void OnDestroy()
        {
            _uiMouseInteraction.UnSubscribe();
            InventoryUI.UnSubscribe();
        }
    }
}