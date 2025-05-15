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

    [SerializeField] private UnityEvent onPuzzleSolved;

    public bool canCharge;
    private bool _confirmed;
    float _energy;
    float elapsedtime;
    public float desiredtime;
    bool idk;
    bool idktwo;

    private void Start()
    {
        canDecharge = true;
        maxCharge = 10f;
        if (isFinalNode)
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
                originalNode.mat.SetFloat("_Energy", 100);
            }
        }
        if (nextNode != null && nextNode.mat != null)
        {
            nextNode.mat.SetFloat("_Energy", 0);
        }
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (chargeSlider != null)
        {
            chargeSlider.fillAmount = (currentCharge / maxCharge);
        }
        if (mat != null && idk)
        {
            elapsedtime += Time.deltaTime;
            float percentage = elapsedtime / desiredtime;
            _energy = Mathf.Lerp(0, 100, percentage);
            mat.SetFloat("_Energy", _energy);

        }
        if (_energy >= 100)
        {
            idk = false;
            elapsedtime = 0;
        }
        if (mat != null && idktwo)
        {
            elapsedtime += Time.deltaTime;
            float percentage = elapsedtime / desiredtime;
            _energy = Mathf.Lerp(100, 0, percentage);
            mat.SetFloat("_Energy", _energy);
        }
        if (_energy <= 0)
        {
            idktwo = false;
            elapsedtime = 0;
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
        canCharge = true;
        if (mat != null)
        {
            _energy = 0;
            idk = true;
        }
        if (isConfiremer)
        {
            linkNodeActivator.ConfimNodes();
        }
        if (isFinalNode)
        {
            onPuzzleSolved.Invoke();
        }
    }
    private void DisableNode()
    {
        Debug.LogWarning("Disabling: " + name);

        canCharge = false;
        if (mat != null)
        {
            idktwo = true;
        }
    }
    public void ConfirmCharge()
    {
        Debug.LogWarning("Charge Confirmed");
        _confirmed = true;
        canDecharge = false;
        currentCharge = maxCharge;
    }
}