using UnityEngine;

public class TriggerObjectOn : MonoBehaviour
{
    public GameObject targetObject;
    public void TurnOnObject()
    {
        targetObject.SetActive(true);
    }
    public void TurnOffObject()
    {
        targetObject.SetActive(false);
    }
}
