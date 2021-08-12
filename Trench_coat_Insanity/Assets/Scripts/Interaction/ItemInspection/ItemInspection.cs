using System.Collections;
using Interaction;
using UnityEngine;

namespace Inspection
{
    public class ItemInspection : MonoBehaviour
    {

        private static ItemInspection _instance;

        public delegate void InteractionEvent(float blur);

        public static event InteractionEvent Observe;

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }

        }

        [Range(0.05f, 0.4f)] public float rotationSpeed = 0.2f;
        public Camera uiCamera;
        public Transform rotationObject;
        public GameObject imageBackground;
        public MeshFilter objectMesh;
        public float maximumOffset = 1f;
        public float maximumNegativeOffset = -3f;
        public float zoomStrength = 0.2f;
        public float observationTime = 0.4f;
        public static bool InteractionActive = false;

        [SerializeField] private float blurStrength = 0.1f;
        [SerializeField] private float noBlueStrength = 20f;
        
        private Vector3 _previousMousePosition;
        private bool _active = false;
        private float _offset = 0;
        private Vector3 _originalPosition;

        private Vector3 _clueNormal;
        private Quaternion _startRotation;
        private string _interactionDialogue = "";
        private Vector3 _cameraNormal;
        private bool _clueFound = false;
        private float _observedTime;
        private ActionType[] _actions;
        private MeshRenderer _renderer;

        private void Start()
        {
            _cameraNormal = uiCamera.transform.forward;
            _originalPosition = rotationObject.position;
            _renderer = rotationObject.GetComponent<MeshRenderer>();
        }

        void Update()
        {
            if (!_instance._active) return;

            MouseMovement();
            ZoomControl();

            if (_instance._clueFound == false)
            {
                DetectClue();
            }

        }

        private void MouseMovement()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _instance._previousMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 delta = Input.mousePosition - _instance._previousMousePosition;
                _instance._previousMousePosition = Input.mousePosition;

                Vector3 axis = Quaternion.AngleAxis(-90f, Vector3.forward) * delta;
                _instance.rotationObject.rotation =
                    Quaternion.AngleAxis(delta.magnitude * _instance.rotationSpeed, axis) *
                    _instance.rotationObject.rotation;
            }
        }

        private void ZoomControl()
        {
            float zoomValue = Input.mouseScrollDelta.y;

            if (zoomValue != 0)
            {
                _instance._offset += zoomValue * _instance.zoomStrength;
                _instance._offset = Mathf.Clamp(_instance._offset, _instance.maximumNegativeOffset,
                    _instance.maximumOffset);
                _instance.rotationObject.position = _instance._originalPosition + new Vector3(0, 0, _instance._offset);
            }
        }

        private void DetectClue()
        {
            if (Vector3.Dot(_instance.rotationObject.rotation * _instance._clueNormal, _instance._cameraNormal) < -0.9f)
            {
                _instance._observedTime += Time.deltaTime;
                if (_instance._observedTime >= _instance.observationTime)
                {
                    _instance._clueFound = true;

                    foreach (ActionType act in _instance._actions)
                    {
                        act.Activate();
                    }
                }
            }
            else
            {
                _instance._observedTime = 0f;
            }
        }

        private static void OnObserve(float blurValue)
        {
            Observe?.Invoke(blurValue);
        }

        public static void StartInspection(Mesh inspectionMesh, Vector3 clue, Quaternion rot, string dialogue,
            Material[] mats, ActionType[] act = null)
        {
            _instance._actions = act;
            _instance.objectMesh.mesh = inspectionMesh;
            _instance._clueNormal = clue;
            _instance._startRotation = rot;
            _instance._interactionDialogue = dialogue;
            _instance._clueFound = false;
            _instance._renderer.materials = mats;
            _instance.StartCoroutine(RotationDelay());
            _instance.imageBackground.SetActive(true);
            _instance.rotationObject.gameObject.SetActive(true);
            MouseInput.Activate(false);
            OnObserve(_instance.blurStrength);
            InteractionActive = true;
        }

        public static void StartInspection(Mesh inspectionMesh, Vector3 clue, Quaternion rot, string dialogue,
            Material[] mats, ActionType act = null)
        {
            _instance._actions = new ActionType[1];
            _instance._actions[0] = act;
            _instance.objectMesh.mesh = inspectionMesh;
            _instance._clueNormal = clue;
            _instance._startRotation = rot;
            _instance._interactionDialogue = dialogue;
            _instance._clueFound = false;
            _instance._renderer.materials = mats;
            _instance.StartCoroutine(RotationDelay());
            _instance.imageBackground.SetActive(true);
            _instance.rotationObject.gameObject.SetActive(true);
            MouseInput.Activate(false);
            OnObserve(_instance.blurStrength);
            InteractionActive = true;
        }

        public static void EndInspection()
        {
            _instance._active = false;
            _instance.imageBackground.SetActive(false);
            _instance.rotationObject.rotation = Quaternion.identity;
            _instance.rotationObject.gameObject.SetActive(false);
            MouseInput.Activate(true);
            OnObserve(_instance.noBlueStrength);
            InteractionActive = false;
        }

        private static IEnumerator RotationDelay()
        {
            yield return new WaitForSeconds(0.3f);
            _instance._active = true;
        }

    }
}