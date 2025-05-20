using JetBrains.Annotations;
using UnityEngine;

public class ColorSwitchActivator : ChargeObject
{
    public Material mat;
    public PuzzleManager puzzleManager;
    public ColorSwitchState colorGoal;
    public ColorSwitchState currentColorState;

    private bool _solved;

    private void Start()
    {
        switch(currentColorState)
        {
            case ColorSwitchState.Red:
                mat.color = Color.red;
                currentColorState = ColorSwitchState.Red;
                break;
            case ColorSwitchState.Blue:
                mat.color = Color.blue;
                currentColorState = ColorSwitchState.Blue;
                break;
        }
        if (currentColorState == colorGoal)
        {
            puzzleManager.SolvePart();
            _solved = true;
        }
        canDecharge = false;
        maxCharge = 5;
    }

    public override void Charge(float charge)
    {
        if (charge >= maxCharge)
        {
            switch (currentColorState)
            {
                case ColorSwitchState.Red:
                    mat.SetColor("_Color", Color.blue);
                    currentColorState = ColorSwitchState.Blue;
                    break;
                case ColorSwitchState.Blue:
                    mat.SetColor("_Color", Color.red);
                    currentColorState = ColorSwitchState.Red;
                    break;
            }
            if (currentColorState == colorGoal)
            {
                puzzleManager.SolvePart();
                _solved = true;
            }
            else if (_solved)
            {
                puzzleManager.UnsolvePart();
                _solved = false;
            }
        }
    }
}

public enum ColorSwitchState
{
    Red,
    Blue
}