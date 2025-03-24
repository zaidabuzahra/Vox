using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction<Vector2> OnInputMoveUpdate = delegate { };
    public UnityAction<Vector2> OnInputeLookUpdate = delegate { };
    public UnityAction OnInputInteractPressed = delegate { };
}
