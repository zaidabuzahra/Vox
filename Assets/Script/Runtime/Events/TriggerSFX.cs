using UnityEngine;

public class TriggerSFX : TriggerPlayerCollisionEvents
{
    [SerializeField] private AudioClip clip;
    [SerializeField] private AudioSource audioSource;
    public void PlaySFX()
    {
        audioSource.PlayOneShot(clip);
    }
}