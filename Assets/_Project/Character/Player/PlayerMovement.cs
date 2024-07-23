using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public bool canMove = true;

    public bool canDash = true;
     
    public float dashForce = 0f;
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
        yield return new WaitForSeconds(1f);
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
