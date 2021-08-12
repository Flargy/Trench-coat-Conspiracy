using Interaction;
using UnityEngine;

namespace Hacking
{
    /**
     * The interactable function that activates the hacking mini game.
     */
    public class InteractionHacking : MonoBehaviour, IInteractable
    {
        public HackingManager manager;
        public void Interact()
        {
            manager.StartHacking();
        }
    }
}