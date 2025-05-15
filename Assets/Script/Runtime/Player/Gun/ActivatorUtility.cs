using DG.Tweening;
using Runtime.Player.Gun;
using UnityEngine;

public class ActivatorUtility : IUtility
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject chargedShot;
    [SerializeField] private ActivatorProjectile projectilePrefab;
    [SerializeField, Range(0f, 0.1f)] private float scaleSpeed;
    [SerializeField, Range(1f, 10f)] private float chargeSpeed;

    private float _chargedEnergy;

    private void Start()
    {
        stopUtility = true;
    }

    private void Update()
    {
        if (isActive)
        {
            Charge();
        }
    }

    public override void Equip()
    {
        //throw new System.NotImplementedException();
        gameObject.SetActive(true);
        animator.SetBool("Equip", true);
    }

    public override void StopUtility()
    {
        isActive = false;
        animator.SetBool("Charge", false);
        chargedShot.transform.DOScale(Vector3.zero,0.1f);
        chargedShot.SetActive(false);
        ActivatorProjectile projectile = Instantiate(projectilePrefab, gunPoint.position, gunPoint.rotation);
        projectile.transform.DOScale(chargedShot.transform.localScale, 0.1f);
        projectile.ShootProjectile(gunPoint.forward, _chargedEnergy);
    }

    private void Charge()
    {
        chargedShot.transform.DOScale(chargedShot.transform.localScale + Vector3.one * scaleSpeed, 0.1f);
        _chargedEnergy += Time.deltaTime * chargeSpeed;
        animator.SetFloat("ChargingTimer", _chargedEnergy);
        Debug.Log(_chargedEnergy);
    }

    public override void Unequip()
    {
        animator.SetBool("Equip", false);
        StopUtility();
        //throw new System.NotImplementedException();
    }

    public override void UseUtility()
    {
        isActive = true;
        _chargedEnergy = 0;
        chargedShot.SetActive(true);
        animator.SetBool("Charge", true);
        Debug.Log("Using Activator Utility");
    }
}