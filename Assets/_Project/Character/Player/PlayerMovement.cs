using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    public bool canMove = true;

    public override void HandleMovement(Vector2 direction, float speed)
    {
        if(!canMove)
        {
            return;
        }

        base.HandleMovement(direction, speed);
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
