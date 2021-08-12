using UnityEngine;

namespace Interaction
{

    public class InteractionBox : MonoBehaviour, IInteractable
    {
        public void Interact() // What will happen when you press the object goes here
        {
            Debug.Log("Box was interacted with");
        }
    }
}