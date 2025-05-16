using UnityEngine;

public class SwitchSkybox : MonoBehaviour
{
    bool isIn;
    float elapsedTime;
    float ambientVal;

    private void Update()
    {
        if (isIn)
        {
            elapsedTime += Time.deltaTime;
            ambientVal = Mathf.Lerp(1, 0, elapsedTime / 1);

            RenderSettings.ambientIntensity = ambientVal;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
        isIn = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
        //RenderSettings.ambientIntensity = 0.35f;
    }
}
