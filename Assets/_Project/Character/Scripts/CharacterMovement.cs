using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform pivotRotation;
    [SerializeField] private SpriteRenderer sprite;

    private bool facingRight = true;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void HandleMovement(Vector2 direction, float speed) 
    {
        Vector3 movement = new Vector3(direction.x, 0, direction.y);
        movement.Normalize();

        if (movement.magnitude <= 0)
        {
            return;
        }

        //rb.Move(Time.deltaTime * speed * movement);
        rb.velocity = speed * movement;
    }

    public virtual void handleRotation(Vector2 direction)
    {
        if (direction.x > 0 && !facingRight)
        {
            facingRight = true;
            sprite.flipX = false;
            pivotRotation.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.x < 0 && facingRight)
        {
            facingRight = false;
            sprite.flipX = true;
            pivotRotation.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
