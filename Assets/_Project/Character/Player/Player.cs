using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Header("Inputs")]
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private bool action_01;
    [SerializeField] private bool playerdash;
    [SerializeField] private bool lookUp;
    [Header("Flags")]
    public bool isAttacking;
    [Header("Components")]
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerAnimation playerAnimation;
    [SerializeField] public PlayerCombat playerCombat;
    [SerializeField] public PlayerCamera playerCamera;
    

    public static Player player;

    public void OnInputMovement(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void OnInputAction(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            action_01 = true;
        }

    }

    public void PlayerDash(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            playerdash = true;
        }

    }

    public void SkyInput(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            lookUp = true;
        }
        else if(context.canceled)
        {
            lookUp = false;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        player = this;
    }


    protected override void Update()
    {
        if (MiniGameController.instance.hasMinigame)
            return;

        if (isAttacking)
            return;

        playerMovement.handleRotation(inputMovement);
        playerAnimation.HandleMovementAnimation(inputMovement.sqrMagnitude);
        if(action_01)
        {
            action_01 = false;
            playerCombat.HandlePunch();
        } 
        if(playerdash)
        {
            playerdash =  false;
            playerMovement.Dash(inputMovement); 
        }

        if(lookUp && !MiniGameController.instance.hasMinigame)
        {
            playerCamera.isLookingAtSky = true;
        }
        else
        {
            playerCamera.isLookingAtSky = false;
        }
    }

    private void FixedUpdate()
    {
        playerMovement.HandleMovement(inputMovement.normalized, MovementSpeed);
    }
}
