using UnityEngine;
using UnityEngine.Rendering;

namespace Shaders
{
    public class OutlinePP : MonoBehaviour
    {
        private Camera    _cam;
        private Renderer  _outlinedObject = null;
        private LayerMask _layerMask;
        
        public Material applyOutline;
        public Material writeObject;

        private void Awake()
        {
            _cam = Camera.main;
            _layerMask += LayerMask.GetMask("Interactable");
            _layerMask += LayerMask.GetMask("BlockingLayer");
        }

        private void Update()
        {
            Ray        ray = _cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, _layerMask))
                _outlinedObject = hit.collider.TryGetComponent(out Renderer rend) ? rend : null;
            else
                _outlinedObject = null;
        }

        private void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            // setup
            var commands        = new CommandBuffer();
            int selectionBuffer = Shader.PropertyToID("_SelectionBuffer");
            commands.GetTemporaryRT(selectionBuffer, src.descriptor);

            //render selection buffer
            commands.SetRenderTarget(selectionBuffer);
            commands.ClearRenderTarget(true, true, Color.clear);
            if (_outlinedObject != null)
                commands.DrawRenderer(_outlinedObject, writeObject);

            //apply everything and clean up in commandbuffer
            commands.Blit(src, dest, applyOutline);
            commands.ReleaseTemporaryRT(selectionBuffer);

            //execute and clean up commandbuffer itself
            Graphics.ExecuteCommandBuffer(commands);
            commands.Dispose();

            RenderTexture.active = dest;
        }
    }
}