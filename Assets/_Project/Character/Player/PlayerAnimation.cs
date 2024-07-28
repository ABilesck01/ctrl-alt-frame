using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : CharacterAnimation
{
    [SerializeField] private string idle;
    [SerializeField] private string walk;
    [Header("Audio")]
    [SerializeField] private StudioEventEmitter footstep;

    private SkeletonAnimation skeletonAnimation;

    protected override void Awake()
    {
        skeletonAnimation = GetComponentInChildren<SkeletonAnimation>();
    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
    }

    private void OnDisable()
    {
        skeletonAnimation.state.Event += State_Event;
    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name.ToLower().Contains("footstep"))
        {
            footstep.Play();
        }
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
        Debug.Log($"trying to play {anim} animation", this);
        if (skeletonAnimation.AnimationState.GetCurrent(0).Animation.Name.Equals(anim))
            return;

        skeletonAnimation.AnimationState.SetAnimation(0, anim, loop);
    }
}
