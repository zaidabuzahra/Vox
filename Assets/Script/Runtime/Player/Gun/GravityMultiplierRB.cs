using UnityEngine;

public class GravityMultiplierRB : MonoBehaviour
{
    public float gravityMultiplier = 2f;

    Rigidbody rb;

    void Awake() => rb = GetComponent<Rigidbody>();

    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
    }
}