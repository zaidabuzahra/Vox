using UnityEngine;
using UnityEngine.Events;

public class TriggerPlayerCollisionEvents : MonoBehaviour
{
    [SerializeField] private UnityEvent eventTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            eventTrigger?.Invoke();
            Destroy(this);
        }
    }
}