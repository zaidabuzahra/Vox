using System.Collections.Generic;
using UnityEngine;

public class LinkNodeActivator : MonoBehaviour
{

    private List<LinkNode> _nodes = new();

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
}
