using UnityEngine;
using UnityEngine.Events;

public class DelayCall : MonoBehaviour
{
    public UnityEvent delayedEvent;
    public float delay = 1f;

    public void InvokeDelayedEvent()
    {
        StartCoroutine(InvokeAfterDelay());
    }
    private System.Collections.IEnumerator InvokeAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        delayedEvent?.Invoke();
    }
}
