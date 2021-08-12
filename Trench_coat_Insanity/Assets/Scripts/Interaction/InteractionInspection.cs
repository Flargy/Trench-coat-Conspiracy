using UnityEngine;

namespace Interaction
{
    public class InteractionInspection : MonoBehaviour, IInteractable
    {
        public Mesh showcaseMesh;
        public Vector3 clueNormal = Vector3.one;
        public Quaternion startRotation = Quaternion.identity;
        public Transform rayPoint;
        public string inspectionDialogue;
        private Material[] _material;
        private ActionType[] _actions;
        public void Interact()
        {
            Inspection.ItemInspection.StartInspection(showcaseMesh, clueNormal, startRotation, inspectionDialogue, _material, _actions);
        }

        private void Start()
        {
            _actions = GetComponents<ActionType>();
            gameObject.AddComponent<MeshCollider>();
            _material = GetComponent<MeshRenderer>().materials;
        }

        #region Editor Only

        #if UNITY_EDITOR
        private void SaveRotation()
        {
            startRotation = transform.rotation;
        }

        private void SetClueNormal(Vector3 normal)
        {
            clueNormal = normal;
        }

        private void SendRay()
        {
            RaycastHit hit;
            if (Physics.Raycast(rayPoint.position, transform.position - rayPoint.position, out hit, Mathf.Infinity))
            {
                SetClueNormal(hit.normal);
            }
        }

        private void SetShowcaseMesh()
        {
            showcaseMesh = GetComponent<MeshFilter>().sharedMesh;
        }

        public void GenerateData()
        {
            if (rayPoint == null)
            {
                rayPoint = new GameObject().transform;
                rayPoint.gameObject.name = "RayPoint";
                rayPoint.position = transform.position + Vector3.forward;
                rayPoint.parent = gameObject.transform;
            }

            if (TryGetComponent(out Collider col) == false)
            {
                gameObject.AddComponent<MeshCollider>();
            }
            SendRay();
            SaveRotation();
            SetShowcaseMesh();
        }
        
        #endif
        #endregion

    }
}