using UnityEngine;

namespace Runtime.Player.Gun
{
    public abstract class IUtility : MonoBehaviour
    {
        public string utilityName;
        public string utilityDescription;
        public Sprite utilityIcon;

        public Animator animator;
        public bool isActive;
        public bool stopUtility;

        public float utilityCharge;

        public abstract void UseUtility();
        public abstract void StopUtility();
        public abstract void Equip();
        public abstract void Unequip();
    }
}