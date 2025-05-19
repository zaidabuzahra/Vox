using UnityEngine;
using UnityEngine.Events;

public class TriggerAnimation : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private string triggerName;
    public bool state = true;
    public bool isTrigger = false;
    public void PlayAnimation()
    {
        if (isTrigger)
        {
            animator.SetTrigger(triggerName);
            return;
        }
        animator.SetBool(triggerName, state);
        //animator.SetTrigger(triggerName);
    }
}