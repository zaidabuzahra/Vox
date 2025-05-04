using UnityEngine;

namespace Runtime.Player.Gun
{
    class UtilityPickUp : MonoBehaviour
    {
        [SerializeField] private IUtility utility;
        [SerializeField] private GunManager gunManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Utility Picked Up");
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
