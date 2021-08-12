using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

    
public class LockNode : MonoBehaviour
{
    public bool traversable = true;
    public bool isPoint = false;
    public Sprite straightLine;
    public Sprite curvedLine;
    public bool isStart = false;

    private Sprite _baseSprite;
    private LockPickManager.Directions lastMovement = LockPickManager.Directions.East;
    private Dictionary<LockNode ,LockPickManager.Directions> _adjacentDictionary = new Dictionary<LockNode, LockPickManager.Directions>();
    private bool _traversableStartingValue = true;
    [SerializeField] private Image _myImage;
    [SerializeField] private RectTransform _myRect;
    [SerializeField] private LockPickManager _manager;

    [SerializeField] private List<LockNode> _keys = new List<LockNode>();
    [SerializeField] private List<LockPickManager.Directions> _values = new List<LockPickManager.Directions>();
    
    
    private void Awake()
    {
        for (int i = 0; i < _keys.Count; i++)
        {
            _adjacentDictionary.Add(_keys[i], _values[i]);
        }
        _traversableStartingValue = traversable;
        _myImage = GetComponent<Image>();
        _baseSprite = _myImage.sprite;
        _myRect = GetComponent<RectTransform>();
    }


    public void TestMovement(LockNode node)
    {
        if (!_adjacentDictionary.ContainsKey(node) || !node.IsTraversable()) return;
        _manager.MoveToNode(node, _adjacentDictionary[node]);
        RefreshSprite(_adjacentDictionary[node]);
        node.BecomeTraversed();
    }

    private void RefreshSprite(LockPickManager.Directions dir)
    {
        if (lastMovement == dir)
        {
            return;
        }
        _myImage.sprite = curvedLine;

        switch (lastMovement)
        {
            case LockPickManager.Directions.North:
                _myRect.rotation = dir == LockPickManager.Directions.West ? new Quaternion(0,0,-0.7071f,0.7071f) : new Quaternion(0,0,0,1f);
                break;
            case LockPickManager.Directions.South:
                _myRect.rotation = dir == LockPickManager.Directions.West ? new Quaternion(0,0,1,0) : new Quaternion(0,0,0.7071f,0.7071f);
                break;
            case LockPickManager.Directions.East:
                _myRect.rotation = dir == LockPickManager.Directions.North ? new Quaternion(0,0,1,0) : new Quaternion(0,0,-0.7071f,0.7071f);
                break;
            case LockPickManager.Directions.West:
                _myRect.rotation = dir == LockPickManager.Directions.North ? new Quaternion(0,0,0.7071f,0.7071f) : new Quaternion(0,0,0,1);
                break;
        }

    }

    public void PreviousMovement(LockPickManager.Directions dir)
    {
        lastMovement = dir;
        _myImage.sprite = straightLine;
        if (dir == LockPickManager.Directions.East || dir == LockPickManager.Directions.West)
        {
            _myRect.rotation = new Quaternion(0,0,0.7071f,0.7071f);
        }
    }

    private void BecomeTraversed() // Changes the value so that the node wont be traversable
    {
        traversable = false;
        _myImage.color = Color.white;
        if (isPoint)
        {
            _manager.CollectPoint();
        }
    }

    private bool IsTraversable() // Returns whether the node is traversable or not
    {
        return traversable;
    }
    
    public void OnButtonPress()
    {
        _manager.RequestMovement(this);
    }
    
    public void ResetStatus()
    {
        traversable = _traversableStartingValue;
        if (isStart)
        {
            _myImage.sprite = straightLine;
            _myRect.rotation = new Quaternion(0,0,0.7071f,0.7071f);
            return;
        }

        lastMovement = LockPickManager.Directions.East;
        _myRect.rotation = Quaternion.identity;
        _myImage.sprite = _baseSprite;
        _myImage.color = new Color(255,255,255,0.1f);

    }

    //Things that only need to exist in editor

    #region Editor Parts
    #if UNITY_EDITOR
    

    public void SetReference(LockPickManager manager)
    {
        _manager = manager;
    }
    public void SetAsStart()
    {
        traversable = false;
        _manager.SetStartTile(this);
        GetComponent<Image>().sprite = straightLine;
        GetComponent<RectTransform>().rotation = new Quaternion(0,0,0.7071f, 0.7071f);
        isStart = true;
    }

    public void RemoveAsStart()
    {
        isStart = false;
        traversable = true;
        GetComponent<Image>().sprite = null;
        GetComponent<RectTransform>().rotation = new Quaternion(0,0,0,0);
    }
   

    public void SetDictionaryNeighbor(LockNode node, LockPickManager.Directions dir)
    {
        _myImage = GetComponent<Image>();
        _myRect = GetComponent<RectTransform>();
        if (node == null)
        {
            return;
        }
        _keys.Add(node);
        _values.Add(dir);
        
    }

    public void ChangePointState()
    {
        isPoint = !isPoint;
        if (isPoint)
        {
            _manager.SetPointCount(1);
        }
        else
        {
            _manager.SetPointCount(-1);
        }
    }

    #endif
    #endregion

}
