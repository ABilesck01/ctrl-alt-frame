using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMinigame : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem vfx;
    [Header("Sign feedbacks")]
    [SerializeField] private GameObject sequenceUp;
    [SerializeField] private GameObject sequenceDown;
    [SerializeField] private GameObject sequenceLeft;
    [SerializeField] private GameObject sequenceRight;

    private bool isOnMinigame;
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private void Start()
    {
        MiniGameController.instance.OnStartMinimage.AddListener(StartMinigame);
    }

    private void StartMinigame(Character arg0)
    {
        if (!isOnMinigame)
        {
            isOnMinigame = true;
            vfx.Play();
        }
    }

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
            ShowSequence(Sequence.up);
            MiniGameController.instance.AddActionToSequence(Sequence.up);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if(down)
        {
            down = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.down);
            MiniGameController.instance.AddActionToSequence(Sequence.down);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if (left)
        {
            left = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.left);
            MiniGameController.instance.AddActionToSequence(Sequence.left);
            Invoke(nameof(FinishMinigame), 1.5f);
        }

        if(right)
        {
            right = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.right);
            MiniGameController.instance.AddActionToSequence(Sequence.right);
            Invoke(nameof(FinishMinigame), 1.5f);
        }
    }

    private void FinishMinigame()
    {
        MiniGameController.instance.CheckMiniGame();
        isOnMinigame = false;
        vfx.Stop();
    }

    public void ShowSequence(Sequence s)
    {
        GameObject sign = sequenceUp;

        switch (s)
        {
            case Sequence.up:
                sign = sequenceUp;
                break;
            case Sequence.down:
                sign = sequenceDown;
                break;
            case Sequence.left:
                sign = sequenceLeft;
                break;
            case Sequence.right:
                sign = sequenceRight;
                break;
        }

        GameObject signInstance = Instantiate(sign, transform.position, Quaternion.identity);
        Destroy(signInstance, 1f);
    }
}
