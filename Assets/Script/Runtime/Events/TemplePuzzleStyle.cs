using Unity.VisualScripting;
using UnityEngine;

public class TemplePuzzleStyle : MonoBehaviour
{
    public PuzzleManager puzzleManager;
    public Transform restPosition;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PowerOnGravityBall>() && other.GetComponent<PowerOnGravityBall>().isPowered)
        {
            puzzleManager.SolvePart();
            other.GetComponent<PowerOnGravityBall>().StopMovement();
            other.transform.position = restPosition.position;
            Destroy(this);
        }
    }
}