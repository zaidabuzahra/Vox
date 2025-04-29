using Runtime.Player.Gun;
using UnityEngine;

[CreateAssetMenu(fileName = "UVLightUtility", menuName = "ScriptableObjects/Utilities/UVLightUtility")]
public class UVLightUtility : IUtility
{
    // Utility properties
    public string utilityName;
    public string utilityDescription;
    public Sprite utilityIcon;
    // Utility methods
    public override void UseUtility()
    {
        Debug.Log("Using UV Light Utility");
    }
    public override void Equip()
    {
        Debug.Log("Equipping UV Light Utility");
    }
    public override void Unequip()
    {
        Debug.Log("Unequipping UV Light Utility");
    }

    public override void StopUtility()
    {
        throw new System.NotImplementedException();
    }
}