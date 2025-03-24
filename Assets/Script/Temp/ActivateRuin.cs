using UnityEngine;

public class ActivateRuin : MonoBehaviour
{
    public Animator animator;
    public void PlayAnimation()
    {
        animator.SetTrigger("Play");
    }
}
