using UnityEngine;

public class FocusedInteraction : MonoBehaviour
{
    public GameObject focusedObject;
    public void Open()
    {
        Debug.Log("OPEN");
        Cursor.lockState = CursorLockMode.Confined;
        //playersignals.stop
        Cursor.visible = true;
        focusedObject.SetActive(true);
    }
    public void Close()
    {
        //playersignals.start
        Cursor.visible = false;
        focusedObject.SetActive(false);
    }
}
