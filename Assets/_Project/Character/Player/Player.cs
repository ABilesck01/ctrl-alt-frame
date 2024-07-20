using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{
    [SerializeField] private Vector2 inputMovement;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerAnimation playerAnimation;

    public void OnInputMovement(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        playerMovement.HandleMovement(inputMovement.normalized, MovementSpeed);
        playerMovement.handleRotation(inputMovement);
        playerAnimation.HandleMovementAnimation(inputMovement.sqrMagnitude);
    }
}
