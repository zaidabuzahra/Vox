using UnityEngine;

public class FocusedInteraction : MonoBehaviour
{
    public GameObject focusedObject;
    public void Open()
    {
        Cursor.lockState = CursorLockMode.Confined;
        //playersignals.stop
        Cursor.visible = true;
        focusedObject.SetActive(true);
    }
    public void Close()
    {
        //playersignals.start
        focusedObject.SetActive(false);
    }
}
