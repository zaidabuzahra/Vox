using UnityEngine;
using UnityEngine.UI;

public class LauchSeedActivator : ChargeObject
{
    public Transform shootingDirection;
    [SerializeField] private GameObject shootingSeed;
    [SerializeField] private float amplifier = 1f;
    [SerializeField] private Image chargeSlider;
    public Animator animator;
    private void Start()
    {
        canDecharge = false;
        currentCharge = 0f;
        animator.SetBool("Closed", shootingSeed);
    }
    public void Catch(Seed seed)
    {
        //play animation
        animator.SetTrigger("Catch");
        shootingSeed = seed.gameObject;
    }
    public override void Charge(float charge)
    {
        if (shootingSeed != null)
        {
            animator.SetTrigger("Shoot");
            Debug.Log("Shooting seed");
            shootingSeed.GetComponent<Seed>().Shoot(shootingDirection.forward, charge * amplifier);
            chargeSlider.fillAmount = charge / 10;
        }
    }
    public void Remove()
    {
        shootingSeed = null;
        animator.SetBool("Closed", false);
    }
}