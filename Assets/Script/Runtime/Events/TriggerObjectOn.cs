using UnityEngine;
using UnityEngine.Events;
using UnityEngine.WSA;

public class TriggerObjectOn : TriggerPlayerCollisionEvents
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
