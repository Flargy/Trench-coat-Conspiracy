using UnityEditor;
using UnityEngine;

namespace Interaction
{

    [CustomEditor(typeof(InteractionInspection))]
    public class InteractionInspectionEditor : Editor
    {
        private InteractionInspection _interactionInspection;
        private bool _isReady = false;
        private GameObject _selected;

        private new SerializedObject _serializedObject;

        private SerializedProperty _showcaseMesh;
        private SerializedProperty _clueNormal;
        private SerializedProperty _startRotation ;
        private SerializedProperty _rayPoint;
        
        private void OnEnable()
        {
            _isReady = false;

            _selected = Selection.activeGameObject;

            if (!_selected) return;
            if (!_interactionInspection)
            {
                _interactionInspection = _selected.GetComponent<InteractionInspection>();
            }
            
            _serializedObject = new SerializedObject(_interactionInspection);

            GetProperties();
            EditorUtility.SetDirty(target);
            _isReady = true;
        }
        
        public override void OnInspectorGUI()
        {
            if (!_isReady) return;
            
            _serializedObject.Update();

            ShowUI();
            
            Undo.RecordObject(target, "Component");

            _serializedObject.ApplyModifiedProperties();
            
        }
        
        private void GetProperties()
        {
            if (!_selected) return;

            _showcaseMesh = _serializedObject.FindProperty("showcaseMesh");
            _clueNormal = serializedObject.FindProperty("clueNormal");
            _startRotation = serializedObject.FindProperty("startRotation");
            _rayPoint = serializedObject.FindProperty("rayPoint");
        }

        private void ShowUI()
        {
            ShowDescription();
            MeshUI();
            ClueUI();
            ButtonUI();
            TextUI();
        }
        
        private void ShowDescription()
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            GUILayout.Label("Interaction information", new GUIStyle(EditorStyles.boldLabel)
            {
                alignment = TextAnchor.MiddleLeft,
                wordWrap = true,
                fontSize = 11,
            });
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void MeshUI()
        {
            ExplanationBox("Mesh to render");
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PropertyField(_showcaseMesh);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        
        private void ClueUI()
        {
            ExplanationBox("Clue normal direction");
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            EditorGUILayout.PropertyField(_clueNormal);
            GUI.enabled = true;
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        
        private void ButtonUI()
        {
            ExplanationBox("Set values");
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button(new GUIContent("Set values", "Sets starting rotation and clue normal")))
            {
                _interactionInspection.GenerateData();
            }
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }

        private void TextUI()
        {
            ExplanationBox("Dialogue upon successful inspection");
            EditorGUILayout.BeginHorizontal();
            _interactionInspection.inspectionDialogue = EditorGUILayout.TextArea(_interactionInspection.inspectionDialogue);
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space();
        }
        
        void ExplanationBox(string inputText)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.Label(inputText, new GUIStyle(EditorStyles.helpBox)
                {
                    alignment = TextAnchor.MiddleLeft,
                    wordWrap = true,
                    fontSize = 12
                });
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}