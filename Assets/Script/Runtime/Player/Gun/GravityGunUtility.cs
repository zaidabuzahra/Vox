using Runtime.Player.Gun;
using UnityEngine;

public class GravityGunUtility : IUtility
{
    // Utility properties
    public LayerMask layerMask;
    public GameObject heldObject;
    public Rigidbody heldObjectRb;
    public float pickUpDistance = 5f;
    public float pickUpForce = 150f;

    private Camera _cam;
    private float currentHoldDistance = 2f;
    public float minHoldDistance = 1f;
    public float maxHoldDistance = 5f;
    public float scrollSpeed = 2f;
    public float followSpeed = 10f;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (heldObject != null)
        {
            // Update hold distance with scroll
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            currentHoldDistance = Mathf.Clamp(currentHoldDistance + scroll * scrollSpeed, minHoldDistance, maxHoldDistance);

            MoveObject();
        }
        else
        {
            Vector3 rayOrigin = new(0.5f, 0.5f, 0f);
            Ray ray = _cam.ViewportPointToRay(rayOrigin);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickUpDistance, layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * pickUpDistance, Color.red, 1f);
            }
        }
    }

    public override void UseUtility()
    {
        if (heldObject == null)
        {
            Vector3 rayOrigin = new(0.5f, 0.5f, 0f);
            Ray ray = _cam.ViewportPointToRay(rayOrigin);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, pickUpDistance, layerMask))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green, 1f);
                PickUpObject(hit.transform.gameObject);
            }
            else
            {
                Debug.DrawLine(ray.origin, ray.origin + ray.direction * pickUpDistance, Color.red, 1f);
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

        if (pickupObject.TryGetComponent<Rigidbody>(out heldObjectRb))
        {
            //heldObjectRb.useGravity = false;
            //heldObjectRb.isKinematic = false;
            heldObjectRb.linearDamping = 10;
            heldObjectRb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObject = pickupObject;
            heldObject.transform.SetParent(null); // No parenting for physical movement
            currentHoldDistance = Mathf.Clamp(Vector3.Distance(_cam.transform.position, heldObject.transform.position), minHoldDistance, maxHoldDistance);
            animator.SetBool("PickUp", true);
        }
    }

    private void DropObject()
    {
        isActive = false;

        if (heldObjectRb != null)
        {
            //heldObjectRb.useGravity = true;
            //heldObjectRb.isKinematic = false;
            heldObjectRb.linearVelocity = Vector3.zero;
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
        Vector3 targetPosition = _cam.transform.position + _cam.transform.forward * currentHoldDistance;
        Vector3 direction = targetPosition - heldObject.transform.position;
        heldObjectRb.linearVelocity = direction * followSpeed;
    }

    public override void Equip()
    {
        gameObject.SetActive(true);
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
