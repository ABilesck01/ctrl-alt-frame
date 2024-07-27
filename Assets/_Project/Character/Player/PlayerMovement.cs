using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField]FMODUnity.StudioEventEmitter dashSound;
    
    public bool canMove = true;

    public bool canDash = true;
     

    public float dashForce = 0f;
    public float dashTime = 0f;

    private void Start()
    {
        MiniGameController.instance.OnStartMinimage.AddListener(OnStartMinigame);
        MiniGameController.instance.OnCorrectMinigame.AddListener(OnFinishMinigame);
        MiniGameController.instance.OnWrongMinigame.AddListener(OnFinishMinigame);
    }

    private void OnStartMinigame(Character arg0)
    {
        canDash = false;
        canMove = false;
    }

    private void OnFinishMinigame()
    {
        canDash = true;
        canMove = true;
    }

    public override void HandleMovement(Vector2 direction, float speed)
    {
        if(!canMove)
        {
            return;
        }

        base.HandleMovement(direction, speed);
    }

    public void Dash(Vector2 direction)
    {
        if (!canDash)
        {
            return;
        }

        PlayerCamera.instance.ShakeCamera(.1f, .08f);

        dashSound.Play();

        if (direction == Vector2.zero)
            direction = new Vector2(facingRight ? 1 : 0, 0);

        canDash = false; 
        canMove = false;

        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        movement.Normalize();
        rb.AddForce(movement * dashForce, ForceMode.VelocityChange);
        StartCoroutine(ResetDash());
    }
    
    IEnumerator ResetDash()
    {
        yield return new WaitForSeconds(dashTime);
        canDash = true;
        canMove = true;
    }
    
    public override void handleRotation(Vector2 direction)
    {
        if(!canMove)
        {
            return;
        }

        base.handleRotation(direction);
    }
}
