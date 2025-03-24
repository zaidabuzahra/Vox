using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public AudioClip dialogueClip;
    public DialogueManager manager;
    bool _activated;
    private void OnTriggerEnter(Collider other)
    {
        if (!_activated)
        {
            manager.PlayClip(dialogueClip);
        }
        _activated = true;
    }
    public void Activate(AudioClip clip)
    {
        manager.PlayClip(clip);
    }
}
