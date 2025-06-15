using DG.Tweening;
using UnityEngine;

public class PowerOnGravityBall : MonoBehaviour
{
    public bool isPowered = true;
    [ColorUsage(hdr: true, showAlpha: true)]
    public Color poweredColor;
    public Material poweredMaterial;

    public void PowerOn()
    {
        if (isPowered) return;
        isPowered = true;
        poweredMaterial.DOColor(poweredColor, 2f);
        gameObject.layer = LayerMask.NameToLayer("Activate");
        gameObject.tag = "Gravity";
    }
    public void StopMovement()
    {
        gameObject.layer = LayerMask.NameToLayer("Default");
        gameObject.tag = "Untagged";
        InputSignals.Instance.OnUseUtilityCancelled?.Invoke();
    }
}
