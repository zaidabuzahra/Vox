using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LinkNodeActivator : MonoBehaviour
{
    [SerializeField] int finalNodeCount;

    private int _nodeCount;
    private List<LinkNode> _nodes = new();

    [SerializeField] private UnityEvent onPuzzleSolved;

    public void AddNode(LinkNode node)
    {
        if (!_nodes.Contains(node))
        {
            _nodes.Add(node);
        }
    }
    public void RemoveNode(LinkNode node)
    {
        if (_nodes.Contains(node))
        {
            _nodes.Remove(node);
        }
    }
    public void ConfimNodes()
    {
        foreach (var node in _nodes)
        {
            node.ConfirmCharge();
        }
    }
    public void SourceReachedDestination()
    {
        _nodeCount++;
        if (_nodeCount == finalNodeCount)
        {
            Debug.Log("All nodes reached destination");
            onPuzzleSolved?.Invoke();
        }
    }
}