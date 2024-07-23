using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMinigame : MonoBehaviour
{
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    public void GetUpInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            up = true;
        }
    }

    public void GetDownInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            down = true;
        }
    }
    public void GetLeftInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            left = true;
        }
    }
    public void GetRightInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            right = true;
        }
    }

    private void Update()
    {
        if (!MiniGameController.instance.hasMinigame)
            return;

        if (up)
        {
            up = false;
            CancelInvoke(nameof(FinishMinigame));
            MiniGameController.instance.AddActionToSequence(Sequence.up);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if(down)
        {
            down = false;
            CancelInvoke(nameof(FinishMinigame));
            MiniGameController.instance.AddActionToSequence(Sequence.down);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if (left)
        {
            left = false;
            CancelInvoke(nameof(FinishMinigame));
            MiniGameController.instance.AddActionToSequence(Sequence.left);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if(right)
        {
            right = false;
            CancelInvoke(nameof(FinishMinigame));
            MiniGameController.instance.AddActionToSequence(Sequence.right);
            Invoke(nameof(FinishMinigame), 1.5f);
        }
    }

    private void FinishMinigame()
    {
        MiniGameController.instance.CheckMiniGame();
    }
}
