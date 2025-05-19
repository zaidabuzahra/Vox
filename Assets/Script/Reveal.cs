//Shady
using UnityEngine;

public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;

    void Update()
    {
        if (Mat == null) return;
        if (!SpotLight || !SpotLight.gameObject.activeInHierarchy)
        {
            Mat.SetVector("_LightPos", Vector3.zero);
            Mat.SetVector("_LightDir", Vector3.zero);
            Mat.SetFloat("_Angle", 0);
            return;
        }
        Mat.SetVector("_LightPos", SpotLight.transform.position);
        Mat.SetVector("_LightDir", -SpotLight.transform.forward);
        Mat.SetFloat("_Angle", SpotLight.spotAngle); 
    }

    private void PickUp(GameObject uv) => SpotLight = uv.GetComponent<UVLightUtility>().gameObject.GetComponentInChildren<Light>();
    private void OnEnable()
    {
        UVLightUtility utility = FindFirstObjectByType<UVLightUtility>();
        if (utility != null)
        {
            SpotLight = utility.gameObject.GetComponentInChildren<Light>();
        }
        if (SpotLight == null)
        {
            InputSignals.Instance.PickUp += PickUp;
        }
    }
}