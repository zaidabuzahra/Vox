using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void Click()
    {
        SceneManager.LoadSceneAsync("Waterfall", LoadSceneMode.Single);
    }
}
