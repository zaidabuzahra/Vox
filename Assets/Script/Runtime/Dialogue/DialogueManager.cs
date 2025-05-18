using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        public Dialogue currentDialogue;

        [SerializeField] private UIDialogueController uiDialogueController;

        private float _dialogueTimer;
        private AudioSource _audioSource;
        private Coroutine _currentCoroutine;
        //private readonly Queue<Dialogue> _dialogueQueue = new();

        private bool _dialogueActive;

        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }
        private void Update()
        {
            if (currentDialogue != null)
            {
                _dialogueTimer -= Time.deltaTime;
                if (_dialogueTimer <= 0 && currentDialogue.canSkip)
                {
                    RequestPlayDialogue(currentDialogue.responses[currentDialogue.responses.Length - 1]);
                    Debug.Log("WOWOOWOWOWOW");
                    //currentDialogue.canSkip = true;
                }
            }
        }
        public void RequestPlayDialogue(Dialogue dialogue)
        {
            Debug.Log("Requesting...");
            if (dialogue.priority == DialoguePriority.High)
            {
                //_dialogueQueue.Clear();
                AbortDialogue();
            }
            else if (dialogue.priority == DialoguePriority.Low)
            {
                if (_dialogueActive && dialogue.activationType != DialogueActivationType.DialogueChoice)
                {
                    //_dialogueQueue.Enqueue(dialogue);
                    return;
                }
            }
            PlayDialogue(dialogue);
        }

        private void PlayDialogue(Dialogue dialogue)
        {
            Debug.Log("Playing...");

            _dialogueTimer = dialogue.expectedDuration;
            uiDialogueController.UIEndOptions();
            currentDialogue = dialogue;
            if (dialogue.dialogueClip != null) 
            { 
                _audioSource.clip = dialogue.dialogueClip;
                _audioSource.Play(); 
            }
            _dialogueActive = true;

            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);
            _currentCoroutine = StartCoroutine(ShowCaption(0));
        }

        private IEnumerator ShowCaption(int i)
        {
            //Caption manipulation maybe should be done in a seperate UI manager
            uiDialogueController.captionText.text = currentDialogue.captions[i];
            yield return new WaitForSeconds(currentDialogue.captionDelays[i]);
            
            if (i < currentDialogue.captions.Length - 1)
            {
                StartCoroutine(ShowCaption(i + 1));
            }
            else
            { 
                Debug.Log("Dialogue ended");
                uiDialogueController.captionText.text = "";
                _dialogueActive = false;
                if (currentDialogue.canRespond)
                {
                    uiDialogueController.UIInitiateOptions(currentDialogue.responses);
                }
                else
                {
                    PlayDialogueQueue();
                }
                //Caption manipulation should be done in a seperate UI manager
            }
        }

        private void PlayDialogueQueue()
        {
            //if (_dialogueQueue.Count > 0) RequestPlayDialogue(_dialogueQueue.Dequeue());
        }

        //This is called if an error occurs and the dialogue is not completed in the expected time
        private void AbortDialogue()
        {
            _audioSource.Stop();
            uiDialogueController.UIEndOptions();
        }

        //Signals to UI that requires dialogue to be inactive
        public bool IsDialogueActive()
        {
            return _dialogueActive;
        }
    }
}