using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private Transform pivotRotation;
    [SerializeField] private SpriteRenderer sprite;

    private bool facingRight = true;

    public override void HandleMovement(Vector2 direction, float speed)
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        movement.Normalize();

        if(movement.magnitude <= 0 )
        {
            return;
        }

        characterController.Move(Time.deltaTime * speed * movement);
    }

    public override void handleRotation(Vector2 direction)
    {
        if(direction.x > 0 && !facingRight)
        {
            facingRight = true;
            sprite.flipX = false;
            pivotRotation.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if(direction.x < 0 && facingRight)
        {
            facingRight = false;
            sprite.flipX = true;
            pivotRotation.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
