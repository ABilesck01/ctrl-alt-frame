using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : CharacterAnimation
{
    [SerializeField] private string idle;
    [SerializeField] private string walk;
    private SkeletonAnimation skeletonAnimation;
    private SkeletonRootMotion skeletonRootMotion;

    protected override void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
        skeletonRootMotion = GetComponentInChildren<SkeletonRootMotion>();
    }

    private void Start()
    {
        skeletonRootMotion.ProcessRootMotionOverride += SkeletonRootMotion_ProcessRootMotionOverride;
    }

    private void SkeletonRootMotion_ProcessRootMotionOverride(SkeletonRootMotionBase component, Vector2 translation, float rotation)
    {
        transform.TransformVector(new Vector3(translation.x, 0, translation.y));
    }

    public override void HandleMovementAnimation(float moveAmount)
    {
        if (moveAmount > 0.1)
        {
            if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals(walk))
                return;

            skeletonAnimation.AnimationState.SetAnimation(0, walk, true);
        }
        else
        {
            if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals(idle))
                return;
            skeletonAnimation.AnimationState.SetAnimation(0, idle, true);
        }
    }

    public override void PlayAnimation(string anim, bool loop = false)
    {
        if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals(anim))
            return;


        skeletonAnimation.AnimationState.SetAnimation(0, anim, loop);
    }
}
