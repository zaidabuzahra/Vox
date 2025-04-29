using UnityEngine;

namespace Runtime.Player.Gun
{
    class UtilityPickUp : MonoBehaviour
    {
        [SerializeField] private IUtility utility;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Utility Picked Up");
                GunManager gunManager = other.GetComponent<GunManager>();
                if (gunManager != null)
                {
                    Debug.Log("Utility Added to GunManager");
                    gunManager.AddUtility(utility);
                    Destroy(gameObject);
                }
            }
        }
    }
}
