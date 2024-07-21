using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [Header("Inputs")]
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private bool action_01;
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

    protected override void Update()
    {
        playerMovement.handleRotation(inputMovement);
        playerAnimation.HandleMovementAnimation(inputMovement.sqrMagnitude);
        if(action_01)
        {
            action_01 = false;
            playerCombat.HandlePunch();
        }
    }

    private void FixedUpdate()
    {
        playerMovement.HandleMovement(inputMovement.normalized, MovementSpeed);
    }
}
