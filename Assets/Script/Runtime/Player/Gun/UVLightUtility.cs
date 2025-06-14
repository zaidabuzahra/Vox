using Runtime.Player.Gun;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UVLightUtility", menuName = "ScriptableObjects/Utilities/UVLightUtility")]
public class UVLightUtility : IUtility
{
    // Utility methods
    public GameObject uvLight;
    [SerializeField] private Material liquidMaterial;
    [SerializeField] private Transform uvLightRayPos;
    [SerializeField] private float pickUpDistance = 5f;
    Vector3 hitGO;
    private Camera _cam;

    private void Start()
    {
        _cam = Camera.main;
    }

    private void Update()
    {
        if (!isActive) return;
        Vector3 rayOrigin = new(0.5f, 0.5f, 0f);

        // actual Ray
        Ray ray = _cam.ViewportPointToRay(rayOrigin);

        if (Physics.Raycast(ray, out RaycastHit hit, pickUpDistance))
        {
            Collider[] colliders = Physics.OverlapSphere(hit.point, 0.1f);
            foreach (Collider c in colliders)
            {
                if (c.gameObject.CompareTag("Gravity") && isActive)
                {
                    c.gameObject.GetComponent<PuzzleManager>().SolvePart();
                    Debug.Log("Hit a UV scannable object");
                }
            }
            hitGO = hit.point;
        }
    }
    public override void UseUtility()
    {
        Debug.Log("Using UV Light Utility");
        isActive = true;
        animator.SetBool("PickUp", true);
        uvLight.SetActive(true);
    }
    public override void Equip()
    {
        gameObject.SetActive(true);
        animator.SetBool("Equip", true);
    }
    public override void Unequip()
    {
        Debug.Log("Unequipping UV Light Utility");
        StopUtility();
        animator.SetBool("Equip", false);
    }

    public override void StopUtility()
    {
        isActive = false;
        animator.SetBool("PickUp", false);
        uvLight.SetActive(false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(hitGO, 0.1f);
    }
}