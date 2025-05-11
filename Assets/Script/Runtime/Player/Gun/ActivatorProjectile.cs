using UnityEngine;

public class ActivatorProjectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    private float _charge;
    public void ShootProjectile(Vector3 direction, float charge)
    {
        transform.parent = null;
        _charge = charge;
        if (TryGetComponent<Rigidbody>(out var rb))
        {
            rb.AddForce(direction * _speed, ForceMode.Impulse);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<ChargeObject>())
        {
            other.gameObject.GetComponent<ChargeObject>().Charge(_charge);
        }
        Destroy(gameObject);
    }
}