using UnityEngine;

namespace Runtime.Player.Gun
{
    class UtilityPickUp : MonoBehaviour
    {
        [SerializeField] private IUtility utility;
        private GunManager gunManager;
        private void Start()
        {
            gunManager = FindFirstObjectByType<GunManager>();
            if (gunManager == null)
            {
                Debug.LogError("GunManager not found in the scene.");
            }
        }
        public void PickUpUtility()
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
