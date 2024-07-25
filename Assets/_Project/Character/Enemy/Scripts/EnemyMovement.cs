using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : CharacterMovement
{
    [SerializeField] private Transform gfx;

    public override void handleRotation(Vector2 direction)
    {
        if (direction.x > 0 && !facingRight)
        {
            facingRight = true;
            //gfx.rotation = Quaternion.Euler(0, 0, 0);
            pivotRotation.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0 && facingRight)
        {
            facingRight = false;
            //gfx.rotation = Quaternion.Euler(0, 180, 0);
            pivotRotation.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
