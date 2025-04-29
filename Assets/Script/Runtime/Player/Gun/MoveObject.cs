using DG.Tweening;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public void Move()
    {
        transform.DOMove(transform.position + Vector3.up * 50, 10f);
    }
}
