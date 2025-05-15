using UnityEngine;

public class Seed : MonoBehaviour
{
    Vector3 _originalPos;
    public void Shoot(Vector3 dir, float power)
    {
        _originalPos = transform.position;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(dir * power, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        if (other.TryGetComponent<LauchSeedActivator>(out LauchSeedActivator ot))
        {
            transform.SetParent(other.transform);
            ot.Catch(this);
        }
        else if (other.CompareTag("Goal"))
        {
            //finish puzzle

        }
        else
        {
            transform.position = _originalPos;
        }
    }
}