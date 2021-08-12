using UnityEngine;

namespace Shaders
{
    [RequireComponent(typeof(MeshRenderer))]
    public class OutlineShader : MonoBehaviour
    {
        private          Renderer _renderer;
        private          Material _originalMat;
        private readonly int      _outlineThickness = Shader.PropertyToID("_OutlineThickness");
        private readonly int      _fresnelExponent  = Shader.PropertyToID("_FresnelExponent");
        private readonly int      _fresnelOpacity   = Shader.PropertyToID("_FresnelOpacity");
        private readonly int      _texture          = Shader.PropertyToID("_MainTex");

        [SerializeField] private Material outlineShader;

        [Range(0, 4f)]   public float fresnelExponent  = 4f;
        [Range(0, 1f)]   public float fresnelOpacity   = 1f;
        [Range(0, 0.2f)] public float outlineThickness = 0.1f;

        private MaterialPropertyBlock _propBlock;
        public MaterialPropertyBlock PropBlock
        {
            get
            {
                if (_propBlock == null)
                    _propBlock = new MaterialPropertyBlock();
                return _propBlock;
            }
        }

        private void OnEnable()
        {
            Init();
        }
        
        public void SetOutline(bool visible = true)
        {
            if (visible)
            {
                if (_renderer.sharedMaterial != outlineShader)
                    _renderer.sharedMaterial = outlineShader;
                GetSetPropBlock();
            }
            else
            {
                if (_renderer.sharedMaterial != _originalMat)
                    _renderer.sharedMaterial = _originalMat;
            }
        }

        private void Init()
        {
            _renderer = this.GetComponent<Renderer>();
            _originalMat = _renderer.sharedMaterial;

            PropBlock.SetTexture(_texture, _originalMat.mainTexture);
            _renderer.SetPropertyBlock(PropBlock);
        }

        private void GetSetPropBlock()
        {
            PropBlock.SetFloat(_fresnelExponent, fresnelExponent);
            PropBlock.SetFloat(_fresnelOpacity, fresnelOpacity);
            PropBlock.SetFloat(_outlineThickness, outlineThickness);

            _renderer.SetPropertyBlock(PropBlock);
        }
    }
}