using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BoilActivator : ChargeObject
{
    [SerializeField] private Image heatGaugeSlider;
    [SerializeField] private Image progressBar;
    [SerializeField] private ParticleSystem boilEffect;
    [SerializeField] private PlantJuice plantJuice;
    [SerializeField, Range(0, 20)] private float underCookedLimit, overCookedLimit;
    [SerializeField] private float progressRate = 1f;
    public bool isFilled;
    public UnityEvent onCharged;

    private void Start()
    {
        canDecharge = true;
        maxCharge = 15f;
        currentCharge = 0f;
        isCharged = false;
    }

    public void Fill()
    {
        isFilled = true;
        currentCharge = 5;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
        heatGaugeSlider.fillAmount = (currentCharge / maxCharge);
        if (isCharged)
        {
            if (boilEffect) boilEffect.Play();
            int progress = 1;
            progressBar.color = Color.green;
            if (currentCharge <= underCookedLimit)
            {
                progress = 0;
                progressBar.color = Color.white;
                if (boilEffect) boilEffect.Stop();
            }
            else if (currentCharge >= overCookedLimit)
            {
                progress = -1;
                progressBar.color = Color.red;
            }
            progressBar.fillAmount += progressRate * progress * Time.deltaTime;
            if (progressBar.fillAmount >= 1)
            {
                plantJuice.Success();
            }
            else if (progressBar.fillAmount <= 0)
            {
                plantJuice.Fail();
                onCharged.Invoke();
                Uncharge();
            }
        }
    }

    public override void Charge(float charge)
    {
        if (!isFilled) return;
        isCharged = true;
        currentCharge += charge;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);
    }

    public override void Uncharge()
    {
        if (progressBar.fillAmount > 0) return;
        base.Uncharge();
    }
}