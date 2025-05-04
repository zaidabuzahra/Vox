using Runtime.Player.Gun;
using TMPro.EditorUtilities;
using UnityEngine;

public class GravityGunUtility : IUtility
{
    // Utility properties
    // Utility methods
    public Animator animator;
    public Transform holdArea;
    public GameObject heldObject;
    public Rigidbody heldObjectRb;
    public float pickUpDistance = 5f;
    public float pickUpForce = 150f;

    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (heldObject != null)
        {
            MoveObject();
        }
    }

    public override void UseUtility()
    {
        if (heldObject == null)
        {
            Vector3 rayOrigin = new(0.5f, 0.5f, 0f);

            // actual Ray
            Ray ray = _cam.ViewportPointToRay(rayOrigin);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, pickUpDistance))
            {
                PickUpObject(hit.collider.gameObject);
            }
        }
        else
        {
            DropObject();
        }
    }
    private void PickUpObject(GameObject pickupObject)
    {
        Debug.Log(pickupObject.name);
        isActive = true;
        if (pickupObject.GetComponent<Rigidbody>() != null)
        {
            heldObjectRb = pickupObject.GetComponent<Rigidbody>();
            heldObjectRb.useGravity = false;
            heldObjectRb.linearDamping = 10;
            heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObject = pickupObject;
            heldObject.transform.SetParent(holdArea);
            animator.SetBool("PickUp", true);
        }
    }

    private void DropObject()
    {
        isActive = false;
        if (heldObjectRb != null)
        {
            heldObjectRb.useGravity = true;
            heldObjectRb.linearDamping = 0;
            heldObjectRb.constraints = RigidbodyConstraints.None;
            heldObjectRb = null;
        }
        if (heldObject != null)
        {
            heldObject.transform.SetParent(null);
            heldObject = null;
        }
        animator.SetBool("PickUp", false);
    }

    private void MoveObject()
    {
        if (Vector3.Distance(heldObject.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 direction = holdArea.position - heldObject.transform.position;
            heldObjectRb.AddForce(direction * pickUpForce);
        }
    }

    public override void Equip()
    {
        animator.SetBool("Equip", true);
    }
    public override void Unequip()
    {
        DropObject();
        animator.SetBool("Equip", false);
    }

    public override void StopUtility()
    {
        DropObject();
    }
}