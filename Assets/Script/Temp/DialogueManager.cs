using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}