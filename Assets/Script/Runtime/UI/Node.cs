using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor;

public class Node : MonoBehaviour
{
    [CustomEditor(typeof(Node))]
    public class ColliderCreatorEditor : Editor
    {

        // some declaration missing??

        override public void OnInspectorGUI()
        {
            Node colliderCreator = (Node)target;
            if (GUILayout.Button("End"))
            {
                colliderCreator.Rotate(); // how do i call this?
            }
            DrawDefaultInspector();
        }
    }
    public enum Direction { Up, Right, Down, Left }

    [Header("Connections")]
    public Direction[] ports = new Direction[2];
    public bool isSource = false;

    [Header("Visuals")]
    public Image nodeImage;
    public Color poweredColor = Color.yellow;
    public Color unpoweredColor = Color.white;

    [Header("Events")]
    public UnityEvent<bool> onPowerChanged; // bool = isPowered

    public Node[] neighbors = new Node[4];
    private bool _isPowered;

    public bool isPowered
    {
        get => _isPowered;
        private set
        {
            _isPowered = value;
            nodeImage.color = value ? poweredColor : unpoweredColor;
            onPowerChanged.Invoke(value);
        }
    }

    private void Awake()
    {
        if (isSource) SetPowered(true);
    }

    private void Start()
    {
        UpdatePower();
    }
    public void SetNeighbors(Node up, Node right, Node down, Node left)
    {
        neighbors[(int)Direction.Up] = up;
        neighbors[(int)Direction.Right] = right;
        neighbors[(int)Direction.Down] = down;
        neighbors[(int)Direction.Left] = left;
    }

    // Call this from Button's OnClick
    public void Rotate()
    {
        transform.Rotate(0, 0, -90);
        for (int i = 0; i < ports.Length; i++)
        {
            ports[i] = (Direction)(((int)ports[i] + 1) % 4);
        }
        UpdatePower();
    }

    public void SetPowered(bool power)
    {
        if (isSource) return; // Sources stay always on
        isPowered = power;
    }

    public void UpdatePower()
    {
        if (isSource)
        {
            PropagatePower();
            return;
        }

        foreach (var dir in ports)
        {
            Node neighbor = neighbors[(int)dir];
            if (neighbor == null || !neighbor.isPowered) continue;

            Direction opposite = (Direction)(((int)dir + 2) % 4);
            if (neighbor.HasPort(opposite))
            {
                isPowered = true;
                PropagatePower();
                return;
            }
        }
        isPowered = false;
    }

    private void PropagatePower()
    {
        foreach (var dir in ports)
        {
            Node neighbor = neighbors[(int)dir];
            if (neighbor == null || neighbor.isPowered) continue;

            Direction opposite = (Direction)(((int)dir + 2) % 4);
            if (neighbor.HasPort(opposite))
            {
                neighbor.isPowered = true;
                neighbor.PropagatePower();
            }
        }
    }

    private bool HasPort(Direction dir)
    {
        foreach (var p in ports) if (p == dir) return true;
        return false;
    }
}