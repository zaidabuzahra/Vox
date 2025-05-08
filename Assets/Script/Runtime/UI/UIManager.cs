using UnityEngine;
using UnityEngine.Events;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private GameObject interactBox;

    public void ShowInteractBox()
    {
        interactBox.SetActive(true);
    }
    public void HideInteractBox()
    {
        interactBox.SetActive(false);
    }
}