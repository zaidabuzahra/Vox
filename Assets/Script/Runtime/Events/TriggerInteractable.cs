using UnityEngine;
using UnityEngine.Events;

public class TriggerInteractable : MonoBehaviour
{
    [SerializeField] private UnityEvent interactionEvent;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.ShowInteractBox();
            InputSignals.Instance.OnInputInteractPressed = Interact;
        }
    }

    private void Interact()
    {
        interactionEvent?.Invoke();
        UIManager.Instance.HideInteractBox();
        Destroy(this);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.HideInteractBox();
            InputSignals.Instance.OnInputInteractPressed -= Interact;
        }
    }
}
