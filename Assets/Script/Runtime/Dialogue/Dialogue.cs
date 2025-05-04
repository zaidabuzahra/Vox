using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/New")]
public class Dialogue : ScriptableObject
{
    [Header("Basic Info")]
    public string title;
    public DialogueActivationType activationType;
    public DialoguePriority priority;

    [Header("Content")]
    public AudioClip dialogueClip;
    [TextArea(3, 5)] public string[] captions;
    public float[] captionDelays;
    public float expectedDuration;
    
    [Header("Responses")]
    public bool canRespond;
    public bool canSkip;

    [HideInInspector] public float responseDelay;
    [HideInInspector] public int responseCount;
    [HideInInspector] public Dialogue[] responses = new Dialogue[3];

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Dialogue))]
    public class DialogueEditor : Editor
    {
        private bool showResponses;
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Dialogue dialogue = (Dialogue)target;
            if (dialogue.canRespond)
            {
                showResponses = EditorGUILayout.Foldout(showResponses, "Responses");
                if (showResponses)
                {
                    EditorGUI.indentLevel++;
                    dialogue.responseCount = EditorGUILayout.IntField("Response Count", dialogue.responseCount);
                    dialogue.responseDelay = EditorGUILayout.FloatField("Response Delay", dialogue.responseDelay);
                    if (dialogue.responses == null || dialogue.responses.Length != dialogue.responseCount)
                    {
                        System.Array.Resize(ref dialogue.responses, dialogue.responseCount);
                    }

                    for (int i = 0; i < dialogue.responseCount; i++)
                    {
                        dialogue.responses[i] = (Dialogue)EditorGUILayout.ObjectField($"Response {i + 1}", dialogue.responses[i], typeof(Dialogue), true);
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }
    }
#endif
    #endregion
}

public enum DialogueActivationType
{
    Trigger,
    SolvePuzzle,
    DialogueChoice
}
public enum DialoguePriority
{
    Low,
    High
}