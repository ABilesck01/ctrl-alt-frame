using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    private Enemy enemy;
    private SkeletonAnimation skeletonAnimation;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if(e.Data.Name.Contains("hit"))
        {
            Debug.Log("handle attacks");
            HandleAttack();
        }
    }

    public override void HandlePunch()
    {
        enemy.enemyAnimation.PlayAnimation("anim_enemy_attack");
    }

    public void HandleAttack()
    {
        Collider[] allHits = Physics.OverlapSphere(attackPoint.position, damageRadius, hitLayer);
        foreach (Collider hit in allHits)
        {
            if (hit.TryGetComponent(out PlayerHealth characterHealth))
            {
                characterHealth.TakeDamage(damage, transform);
            }
        }
    }
}
