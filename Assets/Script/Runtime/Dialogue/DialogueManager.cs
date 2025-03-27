using System.Collections;
using TMPro;
using UnityEngine;

namespace Runtime
{
    [RequireComponent(typeof(AudioSource))]
    public class DialogueManager : MonoSingleton<DialogueManager>
    {
        private bool _dialogueActive;
        private AudioSource _audioSource;

        public TextMeshProUGUI captionText;
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void StartDialogue(Dialogue dialogue)
        {
            Debug.Log("Starting dialogue");
            _dialogueActive = true;
            StopAllCoroutines();
            StartCoroutine(ShowCaption(dialogue, 0));
            //Check if there is responses to be carried out
            _audioSource.clip = dialogue.dialogueClip;
            _audioSource.Play();
        }

        private IEnumerator ShowCaption(Dialogue dialogue, int i)
        {
            Debug.Log("i am waiting");
            //Debug.Log("Caption: " + dialogue.captions[i] + " | ");
            //Caption manipulation should be done in a seperate UI manager
            captionText.text = dialogue.captions[i];
            yield return new WaitForSeconds(dialogue.captionDelay[i]);
            Debug.Log("i am done waiting");
            if (i + 1 < dialogue.captions.Length)
            {
                StartCoroutine(ShowCaption(dialogue, i + 1));
            }
            else
            { 
                Debug.Log("Dialogue ended");
                //Caption manipulation should be done in a seperate UI manager
                captionText.text = "";
                _dialogueActive = false;
            }
        }

        public bool IsDialogueActive()
        {
            return _dialogueActive;
        }
    }
}