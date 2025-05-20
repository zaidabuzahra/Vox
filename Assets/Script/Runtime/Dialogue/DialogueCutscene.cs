using Runtime;
using UnityEngine;

public class DialogueCutscene : MonoBehaviour
{
    public Dialogue dialogue;
    private void OnEnable()
    {
        DialogueManager.Instance.RequestPlayDialogue(dialogue);
    }
}
