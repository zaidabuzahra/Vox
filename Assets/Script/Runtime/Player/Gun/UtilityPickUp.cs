using UnityEngine;
using UnityEngine.Events;

namespace Runtime.Player.Gun
{
    class UtilityPickUp : MonoBehaviour
    {
        public bool isCutscene = true;
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
            Debug.Log("Utility Picked Up");
            gunManager = FindFirstObjectByType<GunManager>();
            if (gunManager != null)
            {
                Debug.Log("Utility Added to GunManager");
                gunManager.AddUtility(utility);
                Destroy(gameObject);
            }
        }
        private void OnEnable()
        {
            if (isCutscene)
            {
                PickUpUtility();
                Destroy(this);
            }
        }
    }
}
