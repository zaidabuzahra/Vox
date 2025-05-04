using UnityEngine;

namespace Runtime.Player.Gun
{
    public abstract class IUtility : MonoBehaviour
    {
        public string utilityName;
        public string utilityDescription;
        public Sprite utilityIcon;

        public bool isActive;
        public bool stopUtility;

        public abstract void UseUtility();
        public abstract void StopUtility();
        public abstract void Equip();
        public abstract void Unequip();
    }
}