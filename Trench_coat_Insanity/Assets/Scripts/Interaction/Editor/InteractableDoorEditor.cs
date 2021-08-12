// using UnityEditor;
// using UnityEngine;
//
//
// namespace Interaction
// {
//     [CustomEditor(typeof(InteractionDoor))]
//     public class InteractableDoorEditor : Editor
//     {
//         private InteractionDoor _interactionDoor;
//         private bool _isReady = false;
//         private GameObject _selected;
//
//         private new SerializedObject _serializedObject;
//
//         private SerializedProperty _sceneIndexToLoad;
//         private SerializedProperty _isLocked;
//
//         private void OnEnable()
//         {
//             _isReady = false;
//
//             _selected = Selection.activeGameObject;
//
//             if (!_selected) return;
//             if (!_interactionDoor)
//             {
//                 _interactionDoor = _selected.GetComponent<InteractionDoor>();
//             }
//             
//             _serializedObject = new SerializedObject(_interactionDoor);
//
//             GetProperties();
//             EditorUtility.SetDirty(target);
//             _isReady = true;
//         }
//         
//         public override void OnInspectorGUI()
//         {
//             if (!_isReady) return;
//             
//             _serializedObject.Update();
//
//             ShowUI();
//             
//             Undo.RecordObject(target, "Component");
//
//             _serializedObject.ApplyModifiedProperties();
//             
//         }
//
//
//         private void GetProperties()
//         {
//             if (!_selected) return;
//
//             _sceneIndexToLoad = _serializedObject.FindProperty("sceneIndexToLoad");
//             _isLocked = serializedObject.FindProperty("isLocked");
//         }
//         
//         private void ShowUI()
//         {
//             ShowDescription();
//             SceneButtonUI();
//             LockUI();
//
//         }
//
//         private void ShowDescription()
//         {
//             EditorGUILayout.Space();
//             EditorGUILayout.BeginHorizontal();
//             GUILayout.Label("Door functionality", new GUIStyle(EditorStyles.boldLabel)
//             {
//                 alignment = TextAnchor.MiddleLeft,
//                 wordWrap = true,
//                 fontSize = 11,
//             });
//             EditorGUILayout.EndHorizontal();
//             EditorGUILayout.Space();
//         }
//
//         private void SceneButtonUI()
//         {
//             ExplanationBox("Controls for scene loading");
//             EditorGUILayout.BeginHorizontal();
//             EditorGUILayout.PropertyField(_sceneIndexToLoad);
//             if (GUILayout.Button(new GUIContent("Next", "Selects next scene")))
//             {
//                 _interactionDoor.ChangeIndexToNextScene();
//             }
//             EditorGUILayout.EndHorizontal();
//             EditorGUILayout.Space();
//         }
//
//         private void LockUI()
//         {
//             ExplanationBox("Locks and opens door");
//             EditorGUILayout.BeginHorizontal();
//             _isLocked.boolValue = EditorGUILayout.Toggle("Is locked", _interactionDoor.isLocked);
//             _interactionDoor.isLocked = _isLocked.boolValue;
//             EditorGUILayout.EndHorizontal();
//             EditorGUILayout.Space();
//         }
//         
//         
//         void ExplanationBox(string inputText)
//         {
//             EditorGUILayout.BeginHorizontal();
//             {
//                 GUILayout.Label(inputText, new GUIStyle(EditorStyles.helpBox)
//                 {
//                     alignment = TextAnchor.MiddleLeft,
//                     wordWrap = true,
//                     fontSize = 12
//                 });
//             }
//             EditorGUILayout.EndHorizontal();
//         }
//     }
// }