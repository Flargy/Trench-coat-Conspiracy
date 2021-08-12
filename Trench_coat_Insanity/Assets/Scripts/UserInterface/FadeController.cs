using UnityEngine;

namespace UserInterface
{
    public class FadeController
    {
        private Animator _animator;

        public void Init(UIManager uiManager)
        {
            _animator = uiManager.fadeAnimator;
            _animator.gameObject.SetActive(true);

            if (LevelManager.Instance != null)
                LevelManager.Instance.InitSceneSwitch += TriggerFadeOut;
            else
                Debug.LogWarning("<b>UIManager.FadeController</b> wants a <b>LevelManager</b> in the scene!");
        }
        
        private void TriggerFadeOut()
        {
            _animator.SetTrigger("ShouldFadeOut");
        }
    }
}