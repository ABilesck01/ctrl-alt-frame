using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    protected Animator animator;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void HandleMovementAnimation(float moveAmount)
    {
        animator.SetFloat("moveAmount", moveAmount);
    }

    public virtual void PlayAnimation(string anim)
    {
        animator.CrossFade(anim, 0);
    }
}
