using DG.Tweening;
using UnityEngine;

public class TriggerRotateObject1 : MonoBehaviour
{
    [SerializeField] private GameObject targetObject;
    [SerializeField] private Vector3 changeVector;
    [SerializeField] private float rotationSpeed = 1f;

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
