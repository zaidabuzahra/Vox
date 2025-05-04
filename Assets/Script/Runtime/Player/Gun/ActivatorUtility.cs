using Runtime.Player.Gun;
using UnityEngine;

public class ActivatorUtility : IUtility
{
    public Transform gunPoint;
    public LineRenderer lineRenderer;
    private Camera _cam;
    public GameObject debugSphere;

    private void Start()
    {
        stopUtility = true;
        _cam = Camera.main;
    }

    private void Update()
    {
        if (isActive)
        {
            lineRenderer.SetPosition(0, gunPoint.position);
            Vector3 rayOrigin = new (0.5f, 0.5f, 0f);

            // actual Ray
            Ray ray = _cam.ViewportPointToRay(rayOrigin);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // our Ray intersected a collider
                debugSphere.transform.position = hit.point;
                Debug.Log("Hit: " + hit.collider.name);
                lineRenderer.SetPosition(1, hit.point);
                if (hit.collider.CompareTag("ChargeObject"))
                {
                    hit.collider.gameObject.GetComponent<ChargeObject>().Charge();
                }
            }
            else
            {
                lineRenderer.SetPosition(1, gunPoint.position + (_cam.transform.forward * 50));
            }
        }
    }

    public override void Equip()
    {
        //throw new System.NotImplementedException();
    }

    public override void StopUtility()
    {
        isActive = false;
        lineRenderer.enabled = false;
    }

    public override void Unequip()
    {
        StopUtility();
        //throw new System.NotImplementedException();
    }

    public override void UseUtility()
    {
        isActive = true;
        lineRenderer.enabled = true;
        Debug.Log("Using Activator Utility");
    }
}
