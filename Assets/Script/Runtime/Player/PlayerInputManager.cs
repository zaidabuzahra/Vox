using UnityEngine;
using UnityEngine.Events;
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
    public void OnScrolling(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnInputScrolling?.Invoke(context.ReadValue<float>()); }
    }
    public void OnOnePressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnChooseNumber?.Invoke(1); }
    }
    public void OnTwoPressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnChooseNumber?.Invoke(2); }
    }
    public void OnThreePressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnChooseNumber?.Invoke(3); }
    }
    public void OnOpenDialogueBox(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnOpenDialogueOptions?.Invoke(); }
        else if (context.canceled) { InputSignals.Instance.OnCloseDialogueOptions?.Invoke(); }
    }
    public void OnUseUtilityPressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnUseUtilityPressed?.Invoke(); }
        if (context.canceled) { InputSignals.Instance.OnUseUtilityCancelled?.Invoke(); }
    }
    public void OnSwitchUtilityRight(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnSwitchUtility?.Invoke(1); }
    }
    public void OnSwitchUtilityLeft(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnSwitchUtility?.Invoke(-1); }
    }
    public void OnJumpPressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnInputJumpPressed?.Invoke(); }
    }
    public void OnInputSprintPressed(InputAction.CallbackContext context)
    {
        if (context.performed) { InputSignals.Instance.OnInputSprintPressed?.Invoke(); }
        if (context.canceled) { InputSignals.Instance.OnInputSprintCancelled?.Invoke(); }
    }
}