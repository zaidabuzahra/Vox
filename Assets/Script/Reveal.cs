//Shady
using UnityEngine;

[ExecuteInEditMode]
public class Reveal : MonoBehaviour
{
    [SerializeField] Material Mat;
    [SerializeField] Light SpotLight;
	
	void Update ()
    {
        Mat.SetVector("_LightPos",  SpotLight.transform.position);
        Mat.SetVector("_LightDir", -SpotLight.transform.forward );
        Mat.SetFloat ("_Angle", SpotLight.spotAngle);
    }//Update() end
}//class end