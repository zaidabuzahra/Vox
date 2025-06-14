using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Dissolve : MonoBehaviour
{
    private float _val = 0;
    public Material dissolveMaterial;
    public void StartDissolve()
    {
        // Start the dissolve effect
        dissolveMaterial.SetFloat("_DissolveAmount", _val);
        StartCoroutine(DissolveEffect());
    }
    private IEnumerator DissolveEffect()
    {
        DOVirtual.Float(0, 1, 2, x => _val = x);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    private void Update()
    {
        dissolveMaterial.SetFloat("_DissolveAmount", _val);
    }
}