using Runtime;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    private void OnTriggerEnter(Collider other)
    {
        if (dialogue.activationType != DialogueActivationType.Trigger) return;
        if (other.CompareTag("Player"))
        {
            DialogueManager.Instance.RequestPlayDialogue(dialogue);
        }
    }
}
