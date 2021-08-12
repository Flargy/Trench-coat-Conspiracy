using System.Collections;
using System.Collections.Generic;
using Interaction;
using UnityEngine;
using UnityEngine.UI;


namespace Hacking
{
    /**
     * The manager for the hacking mini game.
     */
    public class HackingManager : MonoBehaviour
    {
        public float endDelay = 1f;
        public List<GameObject> upperConnections = new List<GameObject>();
        public List<GameObject> lowerConnections = new List<GameObject>();
        public GameObject canvasHolder;

        public List<Button> _buttons = new List<Button>();
        [SerializeField] private List<HackingNode> _nodes;
        [SerializeField] private List<ActionType> _affectedObject;
        [SerializeField] private Audio_Action _startSound;

        private List<HackingConnections> _connections = new List<HackingConnections>();
        private bool _completed = false;

        /**
         * Enum for connections of nodes.
         */
        public enum port
        {
            Upper,
            Lower
        }

        /**
         * Sets the starting connections.
         */
        void Start()
        {
            HackingNode.Check += CheckSolution;
            _connections.Add(new HackingConnections(_nodes[4], port.Upper));
            _connections.Add(new HackingConnections(_nodes[5], port.Lower));
        }

        /**
         * Changes the wire to fit with the next one dependant on if the direction changed.
         */
        private void DrawNewWire(HackingConnections hack)
        {
            if (hack.port == port.Upper)
            {
                for (int j = 0; j < upperConnections.Count; j++)
                {
                    upperConnections[j].SetActive(false);
                }
                int i = _nodes.IndexOf(hack.node);
                upperConnections[i].SetActive(true);
            }
            else
            {
                for (int j = 0; j < lowerConnections.Count; j++)
                {
                    lowerConnections[j].SetActive(false);
                }
                int i = _nodes.IndexOf(hack.node);
                lowerConnections[i].SetActive(true);
            }
        }
        
        /**
         * Resets the Selected nodes.
         */
        public void ResetSelection()
        {
            foreach (HackingNode node in _nodes)
            {
                node.ReturnConnection();
            }
        }
        
        /**
         * Removes a connection to a specific node.
         */
        public void RemoveConnection(HackingNode node)
        {
            foreach (HackingConnections hack in _connections)
            {
                if (hack.node == node)
                {
                    hack.node = null;
                }
            }
        }
        
        /**
         * Resets the mini game.
         */
        public void ResetConnections()
        {
            foreach (HackingConnections hack in _connections)
            {
                hack.node = null;
            }

            _connections[0].node = _nodes[4];
            _connections[1].node = _nodes[5];

            for (int i = 0; i < upperConnections.Count; i++)
            {
                upperConnections[i].SetActive(false);
                lowerConnections[i].SetActive(false);
            }
            upperConnections[4].SetActive(true);
            lowerConnections[5].SetActive(true);

        }

        public void SetConnection(HackingNode node)
        {
            foreach (HackingConnections hack in _connections)
            {
                if (hack.node == null)
                {
                    hack.node = node;
                    DrawNewWire(hack);
                }
            }
        }

        /**
         * Checks if the player has made the correct connections. This is activated after each wire movement.
         */
        private void CheckSolution()
        {
            
            if (_nodes[0].powered && _nodes[1].powered)
            {
                FinishHacking();
            }
            else if (_nodes[2].powered && _nodes[3].powered)
            {
                FinishHacking();
            }
        }
        
        /**
         * Starts the mini game.
         */
        public void StartHacking()
        {
            if (_completed)
                return;
            MouseInput.Activate(false);
            canvasHolder.SetActive(true);
            foreach (Button button in _buttons)
            {
                button.interactable = true;
            }

            if (_startSound != null)
            {
                _startSound.Activate();
            }
        }
        
        /**
         * Stops the mini game. Rests all tiles when exiting.
         */
        public void StopHacking()
        {
            for (int i = 0; i < upperConnections.Count; i++)
            {
                upperConnections[i].SetActive(false);
                lowerConnections[i].SetActive(false);
            }
            upperConnections[4].SetActive(true);
            lowerConnections[5].SetActive(true);

            for (int i = 0; i < _nodes.Count; i++)
            {
                _nodes[i].powered = false;
            }

            _nodes[4].powered = true;
            _nodes[5].powered = true;

            foreach (Button button in _buttons)
            {
                button.interactable = false;
            }
            ResetConnections();
            canvasHolder.SetActive(false);
            MouseInput.Activate(true);
            if (_completed && _affectedObject != null)
            {
                foreach (ActionType act in _affectedObject)
                {
                    act.Activate();
                }
            }
        }
        
        /**
         * Activates a short delay and then closes the mini game upon success.
         */
        private void FinishHacking()
        {
            _completed = true;
            foreach (Button button in _buttons)
            {
                button.interactable = false;
            }
            StartCoroutine(EndDelay());
        }

        private IEnumerator EndDelay()
        {
            yield return new WaitForSeconds(endDelay);
            StopHacking();
        }
    }
    
    /**
     * Custom data class for current nodes and their connections.
     */
    public class HackingConnections
    {
        public HackingNode node { get; set; }
        public HackingManager.port port { get; set; }

        public HackingConnections(HackingNode node, HackingManager.port port)
        {
            this.node = node;
            this.port = port;
        }
        
        
        
    }
}