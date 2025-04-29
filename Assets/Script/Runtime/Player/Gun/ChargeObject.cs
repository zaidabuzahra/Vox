using UnityEngine;
using UnityEngine.Events;

public class ChargeObject : MonoBehaviour
{
    public bool canDischarge;

    public float neededCharge;
    public float currentCharge;

    public float dischargeRate;

    public UnityEvent events;

    private bool _isCharging;

    private void Update()
    {
        if (canDischarge && !_isCharging)
        {
            currentCharge -= Time.deltaTime * dischargeRate;
            if (currentCharge <= 0)
            {
                currentCharge = 0;
            }
        }
    }
    public void Charge()
    {
        _isCharging = true;

        currentCharge += Time.deltaTime * dischargeRate;
        if (currentCharge >= neededCharge)
        {
            currentCharge = neededCharge;
            events.Invoke();
        }
    }
}
