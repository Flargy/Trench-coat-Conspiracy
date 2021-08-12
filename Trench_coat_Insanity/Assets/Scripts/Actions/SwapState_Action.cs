using System.Collections.Generic;
using UnityEngine;

/**
 * Swaps the active state of a list of objects
 */
public class SwapState_Action : ActionType
{
    [SerializeField] private List<GameObject> _affectedObjects;

    private bool _hasBeenActivated = false;

    public override void Activate()
    {
        if (_hasBeenActivated == false)
        {
            _hasBeenActivated = true;
            foreach (GameObject obj in _affectedObjects)
            {
                obj.SetActive(!obj.activeSelf);
            }
        }
    }
}
