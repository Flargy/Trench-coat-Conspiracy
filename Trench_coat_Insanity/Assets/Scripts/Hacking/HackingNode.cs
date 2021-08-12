using UnityEngine;

namespace Hacking
{
    /**
     * The data for each node in the hacking system.
     */
    public class HackingNode : MonoBehaviour
    {
        public delegate void CheckSolution();
        public delegate void MoveWires();
        public static event CheckSolution Check;
        public static event MoveWires Move;
        
        public bool powered = false;
        public HackingManager manager;

        private bool _selected = false;
        private bool _movingWire = false;

        private void Start()
        {
            Move += MovingWire;
        }
        
        /**
         * The function that is called when the node is pressed.
         * Handles functionality depending on if it is powered or not.
         */
        public void OnButtonPress()
        {
            if (powered && _movingWire == false)
            {
                OnMoveEvent();
                powered = false;
                _selected = true;
                manager.RemoveConnection(this);
            }
            else if (powered == false && _movingWire)
            {
                powered = true;
                OnMoveEvent();
                manager.SetConnection(this);
            }

            if (powered)
            {
                Check?.Invoke();
            }
        }
    
        /**
         * Resets the connection.
         */
        public void ReturnConnection()
        {
            if (_selected)
            {
                powered = true;
                manager.SetConnection(this);
                SelectionPurge();
                
            }
        }
        
        /**
         * Deselects the object.
         */
        private void SelectionPurge()
        {
            _selected = false;
            OnMoveEvent();
        }
        
        /**
         * Fires an event to affect every node.
         */
        private void OnMoveEvent()
        {
            Move?.Invoke();
            
        }
        
        /**
         * Updates the availability of every node.
         */
        private void MovingWire()
        {
            _movingWire = !_movingWire;
            _selected = false;
        }
    }
}