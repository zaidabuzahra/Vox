using UnityEngine;

public class PlantJuice : ChargeObject
{
    [SerializeField] private BoilActivator boilActivator;

    private void Start()
    {
        maxCharge = 10f;
        isCharged = false;
    }

    public override void Charge(float charge)
    {
        if (charge >= maxCharge - 5) ;
        {
            isCharged = true;
            boilActivator.isFilled = true;
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
