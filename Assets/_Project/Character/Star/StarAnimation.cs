using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarAnimation : CharacterAnimation
{
    [SerializeField] private string idle;
    [SerializeField] private string walk;
    [SerializeField] private string run;

    public bool isRunning;
    private SkeletonAnimation skeletonAnimation;

    protected override void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    public override void HandleMovementAnimation(float moveAmount)
    {

        if(moveAmount > 1.5)
        {
            if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals(run))
                return;

            skeletonAnimation.AnimationState.SetAnimation(0, run, true);
        }
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
