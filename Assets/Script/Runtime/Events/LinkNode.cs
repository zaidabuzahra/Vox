using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LinkNode : ChargeObject
{
    [SerializeField] private LinkNodeActivator linkNodeActivator;

    [SerializeField] private bool isFirstNode;
    [SerializeField] private bool isFinalNode;
    [SerializeField] private bool isConfiremer;

    [SerializeField] private LinkNode nextNode;
    [SerializeField] private LinkNode originalNode;

    [SerializeField] private Image chargeSlider;

    [SerializeField] private Material mat;

    public bool canCharge;
    private bool _confirmed;
    float _energy;
    float elapsedtime;
    public float desiredtime;
    public float materialMax;
    public float materialMin;

    private void Start()
    {
        canDecharge = true;
        maxCharge = 10f;
        if (isFinalNode || isConfiremer)
        {
            canDecharge = false;
        }
        if (isFirstNode)
        {
            canDecharge = false;
            originalNode.canCharge = true;
            currentCharge = maxCharge;
            isCharged = true;
            _confirmed = true;
            if (originalNode.mat != null)
            {
                originalNode.mat.SetFloat("_Energy", materialMax);
            }
        }
        if (nextNode != null && nextNode.mat != null)
        {
            nextNode.mat.SetFloat("_Energy", materialMin);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (chargeSlider != null)
        {
            chargeSlider.fillAmount = (currentCharge / maxCharge);
        }
        if (mat != null && canCharge)
        {
            elapsedtime += Time.deltaTime;
            float percentage = elapsedtime / desiredtime;
            _energy = Mathf.Lerp(materialMin, materialMax, percentage);
            mat.SetFloat("_Energy", _energy);

        }
        if (mat != null && !canCharge)
        {
            elapsedtime += Time.deltaTime;
            float percentage = elapsedtime / desiredtime;
            _energy = Mathf.Lerp(materialMax, materialMin, percentage);
            mat.SetFloat("_Energy", _energy);
        }
    }

    public override void Charge(float charge)
    {
        if (!canCharge) return;
        isCharged = true;

        Debug.LogWarning("Charged! " + name);
        linkNodeActivator.AddNode(this);

        currentCharge += charge;
        currentCharge = Mathf.Clamp(currentCharge, 0, maxCharge);


        if (nextNode != null)
        {
            nextNode.EnableNode();
        }

        if (originalNode)
        {
            originalNode.DisableNode();
            originalNode.Uncharge();
        }
    }

    public override void Uncharge()
    {
        if (_confirmed) return;

        Debug.LogWarning("Uncharged" + name);

        base.Uncharge();
        if (nextNode != null && nextNode.canCharge)
        {
            nextNode.Uncharge();
            nextNode.DisableNode();
        }
        if (originalNode != null)
        {
            originalNode.EnableNode();
        }
        linkNodeActivator.RemoveNode(this);
    }

    public void EnableNode()
    {
        Debug.LogWarning("Enabling: " + name);
        if (!canCharge) elapsedtime = 0;
        canCharge = true;
        if (mat != null)
        {
            _energy = 0;
        }
        if (isConfiremer)
        {
            linkNodeActivator.ConfimNodes();
        }
        if (isFinalNode)
        {
            linkNodeActivator.SourceReachedDestination();
        }
    }
    private void DisableNode()
    {
        Debug.LogWarning("Disabling: " + name);
        if (canCharge) elapsedtime = 0;
        canCharge = false;
    }
    public void ConfirmCharge()
    {
        Debug.LogWarning("Charge Confirmed");
        _confirmed = true;
        canDecharge = false;
        currentCharge = maxCharge;
    }
}