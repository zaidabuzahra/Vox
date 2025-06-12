using UnityEngine;

public class ButtonPulse : MonoBehaviour
{
    public float pulseSpeed = 1.2f;
    public float pulseStrength = 0.05f;
    private Vector3 startScale;

    void Start()
    {
        startScale = transform.localScale;

    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * pulseSpeed) * pulseStrength;
        transform.localScale = startScale + Vector3.one * pulse;
    }
}
