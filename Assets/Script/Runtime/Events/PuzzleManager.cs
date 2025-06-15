using UnityEngine;
using UnityEngine.Events;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private int puzzleCount;
    public UnityEvent resolvedPuzzle;
    private int currentPuzzleIndex = 0;
    public void SolvePart()
    {
        currentPuzzleIndex++;
        if (currentPuzzleIndex == puzzleCount)
        {
            resolvedPuzzle.Invoke();
            Debug.Log("PuzzleSolved");
        }
    }
    public void UnsolvePart()
    {
        currentPuzzleIndex--;
        if (currentPuzzleIndex < 0)
        {
            currentPuzzleIndex = 0;
        }
    }
}