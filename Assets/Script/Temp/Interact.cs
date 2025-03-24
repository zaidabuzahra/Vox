using TMPro;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public TextMeshProUGUI UItext;
    public AudioClip clip;
    public DialogueTrigger trigger;
    public string text;
    bool _active;
    private void OnTriggerEnter(Collider other)
    {
        if (_active) return;
        UItext.text = text;
        UItext.gameObject.SetActive(true);
    }
    private void OnTriggerStay(Collider other)
    {
        if (_active) return;
        if (Input.GetKeyDown(KeyCode.E))
        {
            trigger.Activate(clip);
            _active = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(!_active) return;
        UItext.gameObject.SetActive(false);
    }
}
