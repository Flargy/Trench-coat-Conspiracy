using Interaction;
using UnityEngine;

public class InteractionLockPick : MonoBehaviour, IInteractable
{
    [SerializeField] private LockPickManager _manager;
    public void Interact()
    {
        _manager.StartChallenge();
    }
}
