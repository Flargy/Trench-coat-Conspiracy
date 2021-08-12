using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockPickManager : MonoBehaviour
{
    public enum Directions
    {
        North,
        South,
        West,
        East
    }
    
    public int rows = 5;
    public int columns = 5;
    public float endDelayTime = 1f;
    public GameObject nodePrefab;
    public Vector2 offset;
    public List<ActionType> actions;
    public ActionType startSound;
    public GameObject holder;


    private LockNode[,] nodes; 
    [SerializeField] private int _points;
    private int _collectedPoints;
    private Rect _rTransform;
    private float _rWidth;
    private float _rHeight;
    private bool _challengeCompleted = false;
    [SerializeField] private LockNode _currentNode;
    [SerializeField] private LockNode _startNode;
    [SerializeField] private List<LockNode> _allNodes = new List<LockNode>();
    

    public void MoveToNode(LockNode newNode, Directions cameFrom)
    {
        _currentNode = newNode;
        _currentNode.PreviousMovement(cameFrom);
    }

    public  void RequestMovement(LockNode requestingNode)
    {
        if (_challengeCompleted == true)
            return;
        _currentNode.TestMovement(requestingNode);
    }

    public void ResetGrid()
    {
        foreach (LockNode node in _allNodes)
        {
            node.ResetStatus();
        }

        _collectedPoints = 0;
        _currentNode = _startNode;
    }
    
    public  void CollectPoint()
    {
        if (_challengeCompleted)
            return;
        _collectedPoints += 1;
        if (_collectedPoints >= _points)
        {
            _challengeCompleted = true;
            StartCoroutine(EndDelay());
        }
    }

    private void FinishChallenge()
    {
        foreach (ActionType act in actions)
        {
            act.Activate();
        }
        EndChallenge();
    }

    public void EndChallenge()
    {
        holder.SetActive(false);
        ResetGrid();
        
    }

    public void StartChallenge()
    {
        if (_challengeCompleted)
            return;
        if (startSound != null)
        {
            startSound.Activate();
        }
        holder.SetActive(true);
        foreach (LockNode node in _allNodes)
        {
            node.ResetStatus();
        }
    }

    private IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(endDelayTime);
        FinishChallenge();
    }

    #region Editor Parts
    #if UNITY_EDITOR
    
    //things that only need to exist in the editor
    
    public void BuildTiles()
    {

        foreach (LockNode node in _allNodes)
        {
            DestroyImmediate(node.gameObject);
        }
        _allNodes = new List<LockNode>();
        nodes = new LockNode[rows,columns];
        _rTransform = nodePrefab.GetComponent<RectTransform>().rect;
        _rWidth = _rTransform.width;
        _rHeight = _rTransform.height;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                SpawnTile(i, j);
            }
        }

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                AssignDictionaryNeighbors(i,j);
            }
        }

        _currentNode = nodes[0, 0];
        _currentNode.GetComponent<Image>().color = Color.white;
        _currentNode.SetAsStart();
        
    }

    private void AssignDictionaryNeighbors(int i, int j)
    {
        if(i - 1 > -1)
            nodes[i,j].SetDictionaryNeighbor(nodes[i - 1, j], Directions.West);
        if(i + 1 < rows)
            nodes[i,j].SetDictionaryNeighbor(nodes[i + 1, j], Directions.East);
        if(j - 1 > - 1)
            nodes[i,j].SetDictionaryNeighbor(nodes[i, j - 1], Directions.South);
        if(j + 1 < columns)
            nodes[i,j].SetDictionaryNeighbor(nodes[i, j + 1], Directions.North);
    }

    public void SetPointCount(int value)
    {
        if (_points < 0)
            _points = 0;
        _points += value;
    }
    

    private void SpawnTile(int x, int y)
    {
        GameObject obj = Instantiate(nodePrefab, holder.transform);
        obj.transform.position = transform.TransformPoint(new Vector3(x * _rWidth + offset.x, y * _rHeight + offset.y, 0));
        LockNode lockNode = obj.GetComponent<LockNode>();
        nodes[x, y] = lockNode;
        lockNode.SetReference(this);
        obj.name = "X: " + x + " Y: " + y;
        obj.GetComponent<Image>().color = new Color(255,255,255,0.1f);
        _allNodes.Add(lockNode);

    }
    

    public void SetStartTile(LockNode node)
    {
        _startNode = node;
        if (_currentNode == node)
        {
            return;
        }
        _currentNode.RemoveAsStart();
        node.isStart = false;
        _currentNode.GetComponent<Image>().color = new Color(255,255,255,0.1f);
        _currentNode = node;
        _currentNode.GetComponent<Image>().color = new Color(255,255,255,1f);

    }

    #endif
    #endregion
}
