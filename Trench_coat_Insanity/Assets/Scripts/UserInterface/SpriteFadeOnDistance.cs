using UnityEngine;

namespace UserInterface
{
    public class SpriteFadeOnDistance : MonoBehaviour
    {
        private Transform _transform;

        [SerializeField]                       private SpriteRenderer spriteRenderer;
        [SerializeField]                       private Collider       collider;
        [SerializeField]                       private Transform      cameraTransform;
        [SerializeField]                       private Transform      playerTransform;
        [Space] [SerializeField, Range(0, 10)] private float          fullVisibilityDistance = 1.5f;
        [SerializeField, Range(0, 10)]         private float          noVisibilityDistance   = 3f;
        [SerializeField, Range(0, 10)]         private float          cameraFullVisDist   = 10f;
        [SerializeField, Range(0, 10)]         private float          cameraNoVisDist   = 3.5f;

        private void Awake()
        {
            _transform = this.transform;
            if (!spriteRenderer)
                spriteRenderer = GetComponent<SpriteRenderer>();
            if (!collider)
                collider = GetComponent<Collider>();
        }

        private void Update()
        {
            Vector3 pos = _transform.position;
            float distanceToPlayer = Vector3.Distance(pos, playerTransform.position);
            float distanceToCamera = Vector3.Distance(pos, cameraTransform.position);
            
            float alpha = Mathf.InverseLerp(
                noVisibilityDistance, fullVisibilityDistance, distanceToPlayer);
            float maxAlpha = Mathf.InverseLerp(
                cameraNoVisDist,cameraFullVisDist , distanceToCamera);

            alpha = Mathf.Clamp(alpha, 0, maxAlpha);
            
            Color spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;

            collider.enabled = alpha > 0.01f;
        }

        private void OnValidate()
        {
            if (noVisibilityDistance < fullVisibilityDistance)
                noVisibilityDistance = fullVisibilityDistance;
            if (cameraFullVisDist < cameraNoVisDist)
                cameraFullVisDist = cameraNoVisDist;
        }
    }
}