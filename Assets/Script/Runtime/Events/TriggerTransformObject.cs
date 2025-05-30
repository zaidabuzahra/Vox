using DG.Tweening;
using UnityEngine;

public class TriggerRotateObject : TriggerPlayerCollisionEvents
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Vector3 changeVector;
    [SerializeField] private float rotationSpeed = 1f;
    public bool immediate;

    public void TriggerTeleportation()
    {
        targetObject.transform.position = changeVector;
    }

    public void TriggerRotation()
    {
        targetObject.transform.DOLocalRotate(targetObject.transform.localRotation.eulerAngles + changeVector, rotationSpeed);
    }
    public void TriggerMovement()
    {
        targetObject.transform.DOLocalMove(targetObject.transform.localPosition + changeVector, rotationSpeed);
    }
    public void TriggerScaling()
    {
        targetObject.transform.DOScale(targetObject.transform.localScale + changeVector, rotationSpeed);
    }
}
