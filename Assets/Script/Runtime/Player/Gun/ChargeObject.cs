using UnityEngine;
using UnityEngine.Events;

public abstract class ChargeObject : MonoBehaviour
{
    protected bool canDecharge;

    protected float currentCharge;
    protected float maxCharge;
    [SerializeField] protected float dechargeRate;

    public bool isCharged;

    private void Update()
    {
        OnUpdate();
    }
    public virtual void OnUpdate()
    {
        if (canDecharge && isCharged)
        {
            currentCharge -= Time.deltaTime * dechargeRate;
            if (currentCharge <= 0)
            {
                Uncharge();
            }
        }
    }
    public virtual void Uncharge()
    {
        isCharged = false;
        currentCharge = 0;
    }
    public abstract void Charge(float charge);
}
