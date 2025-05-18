using UnityEngine;

public class Puzzle : MonoBehaviour
{
    public bool solved;
    public void Solve()
    {
        solved = true;
    }
    public void Unsolve()
    {
        solved = false;
    }
}