using UnityEngine;

public class AnimationSwitchOff : MonoBehaviour
{
    [SerializeField] GameObject turnOffObject;
    public void SwitchOff()
    {
        turnOffObject.SetActive(false);
    }
}
