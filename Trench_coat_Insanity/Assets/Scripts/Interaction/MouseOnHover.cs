using Shaders;
using UnityEngine;

namespace Interaction
{
    public class MouseOnHover
    {
        private Camera        _cam;
        private LayerMask     _layerMaskInteractable;
        private OutlineShader _outlineShader;
        public void Init(MouseInput mouseInput)
        {
            _cam = mouseInput._cam;
            _layerMaskInteractable = mouseInput._layerMaskInteractable;
        }

        public void Tick()
        {
            RaycastHit hit;
            if (Physics.Raycast(_cam.ScreenPointToRay(Input.mousePosition), out hit,
                Mathf.Infinity, _layerMaskInteractable))
            {
                if (hit.collider.TryGetComponent(out OutlineShader shader))
                {
                    _outlineShader = shader;
                    _outlineShader.SetOutline();
                }
                else
                {
                    if (!_outlineShader) return;
                    _outlineShader.SetOutline(false);
                    _outlineShader = null;
                }
            }
            else
            {
                if (!_outlineShader) return;
                _outlineShader.SetOutline(false);
                _outlineShader = null;
            }
        }
    }
}