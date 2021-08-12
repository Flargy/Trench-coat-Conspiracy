using UnityEditor;
using UnityEngine;

/**
 * The editor which is used to set the rotation degrees of Rotation_Action
 */
[CustomEditor(typeof(Rotate_Action))]
public class Rotate_ActionEditor : Editor
{
    private Rotate_Action _rotateAction;
    private bool _isReady = false;
    private GameObject _selected;

    private new SerializedObject _serializedObject;

    private SerializedProperty _rotationTime;
    
    private void OnEnable()
    {
        _isReady = false;

        _selected = Selection.activeGameObject;

        if (!_selected) return;
        if (!_rotateAction)
        {
            _rotateAction = _selected.GetComponent<Rotate_Action>();
        }
            
        _serializedObject = new SerializedObject(_rotateAction);
        
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
        _rotationTime = _serializedObject.FindProperty("rotationTime");
    }

    private void ShowUI()
    {
        RotationTimeUI();
        ButtonUI();
    }

    private void RotationTimeUI()
    {
        ExplanationBox("Rotation time");
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_rotationTime);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }
    
    /**
     * Creates the buttons which sends quaternion values to Rotation_Action
     */
    private void ButtonUI()
    {
        ExplanationBox("RotationButtons");
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("90 degrees clockwise", "Sets starting rotation to 90 degrees")))
        {
            _rotateAction.SetRotation(new Quaternion(0f, 0.7071f, 0f, 0.7071f));
        }
        if (GUILayout.Button(new GUIContent("90 degrees counter clockwise", "Sets starting rotation to 270 degrees")))
        {
            _rotateAction.SetRotation(new Quaternion(0f, -0.7071f, 0f, 0.7071f));
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button(new GUIContent("180 degrees clockwise", "Sets starting rotation to 180 degrees clockwise")))
        {
            _rotateAction.SetRotation(new Quaternion(0f, 1f, 0f, 0f));
        }
        if (GUILayout.Button(new GUIContent("180 degrees counter clockwise", "Sets starting rotation to 180 degrees counter clockwise")))
        {
            _rotateAction.SetRotation(new Quaternion(0f, -1f, 0f, 0f));
        }
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
