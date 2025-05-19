using UnityEngine;
using UnityEngine.Events;

public class InputSignals : MonoSingleton<InputSignals>
{
    public UnityAction<Vector2> OnInputMoveUpdate = delegate { };
    public UnityAction<Vector2> OnInputeLookUpdate = delegate { };

    public UnityAction OnInputJumpPressed = delegate { };

    public UnityAction<float> OnInputScrolling = delegate { };
    public UnityAction<uint> OnChooseNumber = delegate { };
    public UnityAction OnOpenDialogueOptions = delegate { };
    public UnityAction OnCloseDialogueOptions = delegate { };

    public UnityAction OnInputInteractPressed = delegate { };

    public UnityAction OnUseUtilityPressed = delegate { };
    public UnityAction OnUseUtilityCancelled = delegate { };
    public UnityAction<int> OnSwitchUtility = delegate { };
    
    
    public UnityAction<GameObject> PickUp = delegate { };
}
