using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    protected CharacterController characterController;

    protected virtual void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public virtual void HandleMovement(Vector2 direction, float speed) 
    {

    }

    public virtual void handleRotation(Vector2 direction)
    {

    }
}
