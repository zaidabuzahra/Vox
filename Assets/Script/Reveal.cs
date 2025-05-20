//Shady
using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;

    private void Start()
    {
        UVLightUtility utility = FindFirstObjectByType<UVLightUtility>();
        if (utility != null)
        {
            SpotLight = utility.uvLight.GetComponent<Light>();
        }
        Debug.Log(InputSignals.Instance);
        if (SpotLight == null && InputSignals.Instance != null)
        {
            Debug.LogWarning("ENTERED" + gameObject.name);
            InputSignals.Instance.PickUp += PickUp;
        }
    }

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

    private void PickUp(GameObject uv)
    {
        Debug.LogWarning("ENTERED FUNCTION: " + gameObject.name);

        this.SpotLight = uv.GetComponent<UVLightUtility>().uvLight.GetComponent<Light>();
    }
}