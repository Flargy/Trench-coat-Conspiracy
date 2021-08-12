using UnityEngine;

/**
 * The base action which other actions inherits from.
 * Activated through IInteractable classes
 */
public abstract class ActionType : MonoBehaviour
{
    public abstract void Activate(); // The base function that gets activated
}
