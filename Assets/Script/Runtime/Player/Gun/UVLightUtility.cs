using Runtime.Player.Gun;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "UVLightUtility", menuName = "ScriptableObjects/Utilities/UVLightUtility")]
public class UVLightUtility : IUtility
{
    // Utility methods
    [SerializeField] public GameObject uvLight;
    [SerializeField] private Material liquidMaterial;

    private void Update()
    {
        liquidMaterial.SetFloat("_Energy", utilityCharge);
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
}