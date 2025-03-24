using UnityEngine;

public class GunUtility : MonoBehaviour
{
    public bool active;
    public GameObject gun;
    public LayerMask mask;
    private void Update()
    {
        if (active)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, mask)){
                if (hit.collider.gameObject.CompareTag("Activate"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        hit.collider.gameObject.GetComponent<ActivateRuin>().PlayAnimation();
                    }
                }
            }
        }
    }
}
