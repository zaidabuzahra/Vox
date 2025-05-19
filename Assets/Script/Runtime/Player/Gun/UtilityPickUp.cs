using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player.Gun
{
    class UtilityPickUp : MonoBehaviour
    {
        [SerializeField] private GameObject utility;
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
            if (utility.GetComponent<UVLightUtility>())
            {
                InputSignals.Instance.PickUp?.Invoke(utility);
            }
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
