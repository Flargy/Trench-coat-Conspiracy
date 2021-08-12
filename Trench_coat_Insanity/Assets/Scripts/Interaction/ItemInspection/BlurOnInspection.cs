using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Inspection
{
    public class BlurOnInspection : MonoBehaviour
    {
        [SerializeField] private PostProcessVolume _postProcessVolume;
        private DepthOfField _depthOfField;
        void Start()
        {
            _postProcessVolume.profile.TryGetSettings(out _depthOfField);
            ItemInspection.Observe += ChangeBlur;
        }

        public void ChangeBlur(float blurValue)
        {
            _depthOfField.focusDistance.Override(blurValue);
        }
    }
}