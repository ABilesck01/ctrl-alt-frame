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
        Debug.Log(e.Data.Name);

        if(e.Data.Name.Contains("hit"))
        {
            HandleAttack();
        }
    }

    public override void HandlePunch()
    {
        enemy.enemyAnimation.PlayAnimation("anim_enemy_attack");
    }

    public void HandleAttack()
    {
        enemy.enemyAudio.PlayDamageSound();
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
