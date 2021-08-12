using UnityEngine;

namespace Interaction
{
    public class InteractionChallenge3 : MonoBehaviour, IInteractable
    {
        private bool       _puzzleSolved = false;
        private Challenge3 _challenge3;
        [SerializeField] private ActionType startSound;
        [SerializeField] private GameObject challenge3Prefab;
        [SerializeField] private Transform  canvasTransform;
        [SerializeField] private ActionType[]   _actions;

        public void Interact()
        {
            if (_puzzleSolved) return;
            MouseInput.Activate(false);
            _challenge3 = Instantiate(challenge3Prefab, canvasTransform)
                .GetComponent<Challenge3>();
            _challenge3.Init(this);
            if (startSound != null)
            {
                startSound.Activate();
            }
        }

        public void CompleteChallenge()
        {
            CallActions();
            MouseInput.Activate(true);
            _puzzleSolved = true;
        }

        private void CallActions()
        {
            foreach (ActionType action in _actions)
            {
                action.Activate();
            }
        }
    }
}