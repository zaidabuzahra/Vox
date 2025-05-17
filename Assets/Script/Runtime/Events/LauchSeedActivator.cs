using UnityEngine;
using UnityEngine.UI;

public class LauchSeedActivator : ChargeObject
{
    [SerializeField] private Transform shootingDirection;
    [SerializeField] private GameObject shootingSeed;
    [SerializeField] private float amplifier = 1f;
    [SerializeField] private Image chargeSlider;

    private void Start()
    {
        canDecharge = false;
        currentCharge = 0f;
    }
    public void Catch(Seed seed)
    {
        //play animation
        shootingSeed = seed.gameObject;
    }
    public override void Charge(float charge)
    {
        if (shootingSeed != null)
        {
            Debug.Log("Shooting seed");
            shootingSeed.GetComponent<Seed>().Shoot(shootingDirection.forward, charge * amplifier);
            chargeSlider.fillAmount = charge / 10;
        }
    }
}