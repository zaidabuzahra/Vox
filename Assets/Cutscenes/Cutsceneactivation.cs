using UnityEngine;
using UnityEngine.Playables;

public class Cutsceneactivation : MonoBehaviour
{
    public PlayableDirector timelineDirector;
    public MonoBehaviour playerController; 

    private bool hasPlayed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            if (playerController != null)
                playerController.enabled = false;

            timelineDirector.Play();
            StartCoroutine(WaitForTimeline());
        }
    }

    private System.Collections.IEnumerator WaitForTimeline()
    {
        yield return new WaitForSeconds((float)timelineDirector.duration);

        if (playerController != null)
            playerController.enabled = true;
    }
}
