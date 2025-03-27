using Runtime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    enum DialogueActivatioType
    {
        Trigger,
        SolvePuzzle,
        DialogueChoice
    }

    [SerializeField] private DialogueActivatioType activationType;

    public float duration;
    public AudioClip dialogueClip;
    public string[] captions;
    public float[] captionDelay;

    public bool canRespond;
    private bool showResponses;
    [HideInInspector] public float responseDelay;
    [HideInInspector] public int responseCount;
    [HideInInspector] public Dialogue[] responses;

    [HideInInspector] public GameObject puzzle;

    #region Editor
#if UNITY_EDITOR
    [CustomEditor(typeof(Dialogue))]
    public class DialogueEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Dialogue dialogue = (Dialogue)target;
            if (dialogue.activationType == DialogueActivatioType.SolvePuzzle)
            {
                dialogue.puzzle = EditorGUILayout.ObjectField("Puzzle : ", dialogue.puzzle, typeof(GameObject), true) as GameObject;
            }
            if (dialogue.canRespond)
            {
                dialogue.showResponses = EditorGUILayout.Foldout(dialogue.showResponses, "Responses");
                if (dialogue.showResponses)
                {
                    EditorGUI.indentLevel++;
                    dialogue.responseCount = EditorGUILayout.IntField("Response Count", dialogue.responseCount);
                    dialogue.responseDelay = EditorGUILayout.FloatField("Response Delay", dialogue.responseDelay);
                    dialogue.responses = new Dialogue[dialogue.responseCount];
                    for (int i = 0; i < dialogue.responses.Length; i++)
                    {
                        dialogue.responses[i] = (Dialogue)EditorGUILayout.ObjectField("Response " + i, dialogue.responses[i], typeof(Dialogue), true);
                    }
                    EditorGUI.indentLevel--;
                }
            }
        }
    }
#endif
    #endregion
    private void OnTriggerEnter(Collider other)
    {
        if (activationType == DialogueActivatioType.Trigger)
        {
            if (other.CompareTag("Player"))
            {
                DialogueManager.Instance.StartDialogue(this);
            }
        }
    }
    private void Start()
    {
        if (activationType == DialogueActivatioType.SolvePuzzle)
        {
            //puzzle.SetActive(false);
        }
    }
}