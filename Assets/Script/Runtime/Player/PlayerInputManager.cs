using UnityEngine;
using UnityEngine.InputSystem;

//Responsible for handling player input and sending it to the appropriate system. (e.g. ui input, player movement input, gun switching input, etc.)
public class PlayerInputManager : MonoBehaviour
{
    public void OnMoveUpdate(InputAction.CallbackContext context)
    {
        InputSignals.Instance.OnInputMoveUpdate?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnLookUpdate(InputAction.CallbackContext context)
    {
        InputSignals.Instance.OnInputeLookUpdate?.Invoke(context.ReadValue<Vector2>());
    }
    public void OnInteractPress(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnInputInteractPressed?.Invoke(); }
    }
}