using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ProBuilder.MeshOperations;

public class PlantJuice : ChargeObject
{
    [SerializeField] private BoilActivator boilActivator;

    public UnityEvent onCharged;
    public UnityEvent notCharged;

    private void Start()
    {
        maxCharge = 10f;
        isCharged = false;
    }

    public override void Charge(float charge)
    {
        if (charge >= maxCharge - 5)
        {
            isCharged = true;
            boilActivator.Fill();
            onCharged?.Invoke();
        }
        else
        {
            notCharged?.Invoke();
        }
    }

    public void Fail()
    {
        isCharged = false;
        boilActivator.isFilled = false;
    }
    public void Success()
    {
        //play another animation
    }
}
