using UnityEngine;

public class SwitchSkybox : MonoBehaviour
{
    bool isIn;

    private void OnTriggerEnter(Collider other)
    {
        if (isIn)
        {
            RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Skybox;
            RenderSettings.ambientIntensity = 0.35f;
            isIn = false;
            return;
        }
        RenderSettings.defaultReflectionMode = UnityEngine.Rendering.DefaultReflectionMode.Custom;
        RenderSettings.ambientIntensity = 0;
        isIn = true;
    }
}
