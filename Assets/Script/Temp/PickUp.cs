using UnityEngine;

public class PickUp : MonoBehaviour
{
    public GameObject weapon;
    public GunUtility player;
    private void OnTriggerEnter(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.active = true;
            player.gun.SetActive(true);
            weapon.SetActive(false);
        }
    }
}
