using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LockPickManager))]
public class LockManagerEditor : Editor
{
    private LockPickManager _lockPickManager;
    
    private bool _isReady = false;
    private GameObject _selected;

    private new SerializedObject _serializedObject;

    private SerializedProperty _rows;
    private SerializedProperty _columns;
    private SerializedProperty _endDelayTime;
    private SerializedProperty _nodePrefab;
    private SerializedProperty _offset;
    private SerializedProperty _actions;
    private SerializedProperty _holder;
    private SerializedProperty _startSound;
    
    private void OnEnable()
    {
        _isReady = false;

        _selected = Selection.activeGameObject;

        if (!_selected) return;
        if (!_lockPickManager)
        {
            _lockPickManager = _selected.GetComponent<LockPickManager>();
        }
            
        _serializedObject = new SerializedObject(_lockPickManager);
        
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
        _rows = _serializedObject.FindProperty("rows");
        _columns = _serializedObject.FindProperty("columns");
        _nodePrefab = _serializedObject.FindProperty("nodePrefab");
        _offset = _serializedObject.FindProperty("offset");
        _actions = _serializedObject.FindProperty("actions");
        _holder = _serializedObject.FindProperty("holder");
        _startSound = serializedObject.FindProperty("startSound");
        _endDelayTime = serializedObject.FindProperty("endDelayTime");

    }
    
    private void ShowUI()
    {
        NumberUI();
        OffsetUI();
        PrefabUI();
        HolderUI();
        ActionsUI();
        ButtonUI();
    }

    private void NumberUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_rows);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_columns);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_endDelayTime);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void OffsetUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_offset);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void PrefabUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(_nodePrefab);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void HolderUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(_holder);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void ActionsUI()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.PropertyField(_actions);
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.ObjectField(_startSound, typeof(ActionType));
        _lockPickManager.startSound = _startSound.objectReferenceValue as ActionType;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
    }

    private void ButtonUI()
    {
        ExplanationBox("Rebuilds the tiles");
        if (GUILayout.Button(new GUIContent("Build tiles", "Removes old tiles and rebuilds them")))
        {
            _lockPickManager.BuildTiles();
        }
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
