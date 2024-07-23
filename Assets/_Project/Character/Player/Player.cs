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

    [Header("Components")]
    [SerializeField] public PlayerMovement playerMovement;
    [SerializeField] public PlayerAnimation playerAnimation;
    [SerializeField] public PlayerCombat playerCombat;
    

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


    protected override void Update()
    {
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
    }

    private void FixedUpdate()
    {
        playerMovement.HandleMovement(inputMovement.normalized, MovementSpeed);
    }
}
