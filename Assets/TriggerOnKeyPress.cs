using UnityEngine;
using UnityEngine.Events;

public class TriggerOnKeyPress : MonoBehaviour
{
    public UnityEvent onKeyPressQ;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R))
        {
            onKeyPressQ?.Invoke();
        }
    }
}
