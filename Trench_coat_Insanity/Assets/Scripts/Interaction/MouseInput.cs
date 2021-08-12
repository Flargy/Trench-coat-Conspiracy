using FollowCamera;
using InventorySystem;
using UnityEngine;
using Player;
using UnityEngine.EventSystems;

namespace Interaction
{
    public class MouseInput : MonoBehaviour
    {
        private static MouseInput _instance;

        private MouseOnHover _mouseOnHover;
        private Player01     _p1;

        private bool _active      = true;
        private bool _holdingItem = false;

        private LayerMask _layerMaskWalkable;
        private int blockingLayerId;
        
        internal Camera       _cam;
        internal LayerMask _layerMaskInteractable;
        
        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

            _instance._mouseOnHover = new MouseOnHover();
            blockingLayerId = LayerMask.GetMask("BlockingLayer");
            _layerMaskInteractable += blockingLayerId;
            _layerMaskInteractable += LayerMask.GetMask("Interactable");
            _layerMaskWalkable = LayerMask.GetMask("Walkable");
        }

        private void Start()
        {
            _instance._cam = Camera.main;
            _instance._p1 = GetComponent<Player01>();
            Inventory.UpdateHoldingItem += IsHoldingItem;
            
            _instance._mouseOnHover.Init(this);
        }

        private void Update()
        {
            if (!_instance._active) return;
            _instance._mouseOnHover.Tick();
            
            if (!Input.GetMouseButtonDown(0)) return;
            RaycastHit hit;

            //Return if pointer is over UI.
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (_holdingItem)
            {
                if (Physics.Raycast(_instance._cam.ScreenPointToRay(Input.mousePosition), out hit,
                    Mathf.Infinity, _layerMaskInteractable))
                {
                    if (hit.collider.gameObject.layer == blockingLayerId)
                    {
                        Inventory.ReleaseHeldItem();
                        return;
                    }
                    if (!hit.collider.TryGetComponent(out IInteractable script)) return;
                    if(_instance._p1.MovementEnabled)
                        _instance._p1.MoveTo(hit.collider.transform.position, script);
                    else
                    {
                        script.Interact();
                        Inventory.ReleaseHeldItem();
                    }
                }
                else
                    Inventory.ReleaseHeldItem();
            }
            else
            {
                if (Physics.Raycast(_instance._cam.ScreenPointToRay(Input.mousePosition), out hit,
                    Mathf.Infinity, _layerMaskInteractable))
                {
                    if (hit.collider.gameObject.layer == blockingLayerId)
                    {
                        Inventory.ReleaseHeldItem();
                        return;
                    }
                    
                    hit.collider.TryGetComponent(out IInteractable script);
                    if (script as ClickMe != null)
                    {
                        script.Interact();
                        Inventory.ReleaseHeldItem();
                    }
                    else
                    {
                        if(_instance._p1.MovementEnabled)
                            _instance._p1.MoveTo(hit.collider.transform.position,script);
                        else
                        {
                            script.Interact();
                        }
                    }
                }
                else if (Physics.Raycast(_instance._cam.ScreenPointToRay(Input.mousePosition),
                    out hit, Mathf.Infinity, _layerMaskWalkable))
                {
                    _instance._p1.MoveTo(hit.point);
                }
            }

            // hold RMB to move
            // if (Input.GetMouseButton(1))
            // {
            // 	RaycastHit hit;
            //
            // 	if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity,
            // 		LayerMask.GetMask("Walkable")))
            // 	{
            // 		//Vector2 hitPoint = (Vector2) hit.point;
            // 		print("Sending hit: " + hit.point);
            // 		_p1.MoveTo(hit.point);
            // 	}
            // }
        }

        public static void Activate(bool newValue)
        {
            _instance._active = newValue;
        }

        public static void IsHoldingItem(int i)
        {
            _instance._holdingItem = i >= 0; //If index is below 0, then false.
        }
    }
}