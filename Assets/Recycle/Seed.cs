using UnityEngine;

public class Seed : MonoBehaviour
{
    public LauchSeedActivator lauchSeedActivator;
    public PuzzleManager puzzleManager;
    public void Shoot(Vector3 dir, float power)
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().AddForce(dir * power, ForceMode.Impulse);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent == transform.parent) return;
        GetComponent<Rigidbody>().isKinematic = true;

        if (other.CompareTag("SeedPlant"))
        {
            lauchSeedActivator.Remove();
            transform.SetParent(other.transform.parent);
            lauchSeedActivator = transform.parent.GetComponentInChildren<LauchSeedActivator>();
            lauchSeedActivator.Catch(this);
            transform.position = lauchSeedActivator.shootingDirection.position;
        }
        else if (other.CompareTag("Goal"))
        {
            //finish puzzle
            puzzleManager.SolvePart();
            lauchSeedActivator.Remove();
            Destroy(gameObject);

        }
        else
        {
            transform.position = lauchSeedActivator.shootingDirection.position;
        }
    }
}