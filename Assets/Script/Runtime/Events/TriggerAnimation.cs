using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;
    public void PlayAnimation()
    {
        animator.SetBool(triggerName, true);
        //animator.SetTrigger(triggerName);
    }
}