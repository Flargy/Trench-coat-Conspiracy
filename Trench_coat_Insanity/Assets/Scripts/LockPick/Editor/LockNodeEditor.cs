using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(LockNode))]
public class LockNodeEditor : Editor
{
    private LockNode _lockNode;
    
    private bool _isReady = false;
    private GameObject _selected;

    private new SerializedObject _serializedObject;

    private SerializedProperty _traversable;
    private SerializedProperty _isPoint;
    private SerializedProperty _straightLine;
    private SerializedProperty _curvedLine;
    
    private void OnEnable()
    {
        _isReady = false;

        _selected = Selection.activeGameObject;

        if (!_selected) return;
        if (!_lockNode)
        {
            _lockNode = _selected.GetComponent<LockNode>();
        }
            
        _serializedObject = new SerializedObject(_lockNode);
        
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
        _traversable = _serializedObject.FindProperty("traversable");
        _isPoint = _serializedObject.FindProperty("isPoint");
        _straightLine = _serializedObject.FindProperty("straightLine");
        _curvedLine = _serializedObject.FindProperty("curvedLine");

    }
    
    private void ShowUI()
    {
        BoolUI();
        ButtonUI();
        ImageUI();
    }

    private void BoolUI()
    {
       ExplanationBox("Bool values");
       EditorGUILayout.BeginHorizontal();
       _traversable.boolValue = EditorGUILayout.Toggle("is traversable",_lockNode.traversable);
       _lockNode.traversable = _traversable.boolValue;
       EditorGUILayout.EndHorizontal();
       EditorGUILayout.Space();
       EditorGUILayout.BeginHorizontal();
       GUI.enabled = false;
       _isPoint.boolValue = EditorGUILayout.Toggle("is a point", _lockNode.isPoint);
       GUI.enabled = true;
       EditorGUILayout.EndHorizontal();
       EditorGUILayout.Space();
    }

    private void ImageUI()
    {
        ExplanationBox("Images");
        EditorGUILayout.ObjectField(_straightLine, typeof(Sprite));
        EditorGUILayout.Space();
        EditorGUILayout.ObjectField(_curvedLine, typeof(Sprite));
        EditorGUILayout.Space();
    }

    private void ButtonUI()
    {
        ExplanationBox("Selects if the node will be a point or not");
        if (GUILayout.Button(new GUIContent("Swap point value", "Selects if the node will be a point")))
        {
            _lockNode.ChangePointState();
        }
        EditorGUILayout.Space();
        ExplanationBox("Selects the node as starting point");
        if (GUILayout.Button(new GUIContent("Set as start", "Sets the node as start position")))
        {
            _lockNode.SetAsStart();
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
