using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : CharacterCombat
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();
        enemy = GetComponent<Enemy>();
    }

    public override void HandlePunch()
    {
        enemy.enemyAnimation.PlayAnimation("swoosh");
    }

    public void HandleAttack()
    {
        Collider[] allHits = Physics.OverlapSphere(attackPoint.position, damageRadius, hitLayer);
        foreach (Collider hit in allHits)
        {
            if (hit.TryGetComponent(out CharacterHealth characterHealth))
            {
                characterHealth.TakeDamage(damage, transform);
            }
        }
    }
}
