using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interaction
{
    [RequireComponent(typeof(Collider))]
    public class InteractionDoor : MonoBehaviour, IInteractable
    {
        private bool switchScene = false;
        
        [SerializeField] private Transform leadsToLocation;

        public InventoryItem keyItem;
        public int           levelToLoad = 1;
        public bool          isLocked         = true;

        private void Awake() => switchScene = (leadsToLocation == null);

        public void Interact()
        {
            if (InventorySystem.Inventory.CurrentlyHeldItem == keyItem)
            {
                UnlockDoor();
                InventorySystem.Inventory.DiscardItem(keyItem);
                print("The door was unlocked!");
            }
            else
            {
                if (isLocked)
                {
                    print("The door is locked!");
                    return;
                    // if (InventorySystem.Inventory.CanProgress() == false) // first playable only
                    // {
                    //     return;
                    // }
                }
            
                if (switchScene)
                    //Debug.Log("load new scene...");
                    LevelManager.Instance.LoadSceneFromIndex(levelToLoad -1);
                else
                    Debug.Log("travel to location...");
            }
        }

        public void ChangeIndexToNextScene()
        {
            levelToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        }

        private void LockDoor()
        {
            isLocked = true;
        }

        private void UnlockDoor()
        {
            isLocked = false;
        }

        private void OnDrawGizmosSelected()
        {
            if (leadsToLocation == null) return;

            Vector3 location = leadsToLocation.position;
            Vector3 cubeSize = new Vector3(0.5f, 1f, 0.5f);
            Vector3 center   = new Vector3(0, cubeSize.y * 0.5f, 0);
            
            Gizmos.DrawWireCube(location + center, cubeSize);
            Gizmos.DrawLine(transform.position, location + center);
        }

        private void OnValidate()
        {
            if (levelToLoad <= 0) levelToLoad = 1;
        }
    }
}
