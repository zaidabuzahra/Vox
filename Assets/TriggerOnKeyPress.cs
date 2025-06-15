using UnityEngine;
using UnityEngine.Events;

public class TriggerOnKeyPress : MonoBehaviour
{
    public bool Q, Shift, Click;
    public UnityEvent onKeyPressQ;
    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.R) ) && Q)
        {
            onKeyPressQ?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && Shift)
        {
            gameObject.SetActive(false);
        }
        if (Input.GetMouseButtonDown(0) && Click)
        {
            gameObject.SetActive(false);
        }
    }
}
