using DG.Tweening;
using Runtime.Player.Gun;
using TMPro.EditorUtilities;
using UnityEngine;

public class ActivatorUtility : IUtility
{
    [SerializeField] private Transform gunPoint;
    [SerializeField] private GameObject chargedShot;
    [SerializeField] private ActivatorProjectile projectilePrefab;
    [SerializeField, Range(0f, 0.1f)] private float scaleSpeed;
    [SerializeField, Range(1f, 10f)] private float chargeSpeed;

    [SerializeField] private LineRenderer lineRenderer;

    private float _chargedEnergy;
    private Camera _cam;

    private void Start()
    {
        stopUtility = true;
        _cam = Camera.main;
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, gunPoint.position);
        Ray ray = new Ray(gunPoint.position, gunPoint.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            lineRenderer.enabled = true;
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.enabled = false;
        }
        if (isActive)
        {
            Charge();
        }
    }

    public override void Equip()
    {
        //throw new System.NotImplementedException();
        lineRenderer.enabled = true;
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
        lineRenderer.enabled = false;

        animator.SetBool("Equip", false);
        StopUtility();
        //throw new System.NotImplementedException();
    }

    public override void UseUtility()
    {
        lineRenderer.enabled = true;
        isActive = true;
        _chargedEnergy = 0;
        chargedShot.SetActive(true);
        animator.SetBool("Charge", true);
        Debug.Log("Using Activator Utility");
    }
}