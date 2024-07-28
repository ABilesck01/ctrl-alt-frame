using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMinigame : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private GameObject vfx;
    [Header("Sign feedbacks")]
    [SerializeField] private string idle;
    [SerializeField] private string sequence_up;
    [SerializeField] private string sequence_down;
    [SerializeField] private string sequence_left;
    [SerializeField] private string sequence_right;

    private PlayerAnimation playerAnimation;
    private PlayerMovement playerMovement;

    private bool isOnMinigame;
    private bool minigameInputDelay;
    private bool up;
    private bool down;
    private bool left;
    private bool right;

    private void Awake()
    {
        playerAnimation = GetComponent<PlayerAnimation>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        MiniGameController.instance.OnStartMinimage.AddListener(StartMinigame);
    }

    private void StartMinigame(Character arg0)
    {
        if (!isOnMinigame)
        {
            isOnMinigame = true;
            vfx.SetActive(true);
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

        if (minigameInputDelay)
            return;

        if (up)
        {
            up = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.up);
            MiniGameController.instance.AddActionToSequence(Sequence.up);

            Invoke(nameof(FinishMinigame), 1.8f);
        }

        if(down)
        {
            down = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.down);
            MiniGameController.instance.AddActionToSequence(Sequence.down);
            Invoke(nameof(FinishMinigame), 1.8f);
        }

        if (left)
        {
            left = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.left);
            MiniGameController.instance.AddActionToSequence(Sequence.left);
            Invoke(nameof(FinishMinigame), 1.8f);
        }

        if(right)
        {
            right = false;
            CancelInvoke(nameof(FinishMinigame));
            ShowSequence(Sequence.right);
            MiniGameController.instance.AddActionToSequence(Sequence.right);
            Invoke(nameof(FinishMinigame), 1.8f);
        }
    }

    private void FinishMinigame()
    {
        MiniGameController.instance.CheckMiniGame();
        isOnMinigame = false;
        vfx.SetActive(false);
    }

    private void ResetDelay()
    {
        minigameInputDelay = false;
        playerAnimation.PlayAnimation(idle);
    }

    public void ShowSequence(Sequence s)
    {
        Invoke(nameof(ResetDelay), 1.1f);
        string sign = "";
        PlayerCamera.instance.ShakeCamera(.15f, .2f);
        switch (s)
        {
            case Sequence.up:
                sign = sequence_up;
                break;
            case Sequence.down:
                sign = sequence_down;
                break;
            case Sequence.left: 
                sign = playerMovement.FacingRight() ? sequence_left : sequence_right;
                break;
            case Sequence.right:
                sign = playerMovement.FacingRight() ? sequence_right : sequence_left;
                break;
        }

        playerAnimation.PlayAnimation(sign);

        //GameObject signInstance = Instantiate(sign, transform.position, Quaternion.identity);
        //Destroy(signInstance, 1f);
    }
}
