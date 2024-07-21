using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    private Enemy enemy;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int damage, Transform attackPoint)
    {
        base.TakeDamage(damage, attackPoint);
        CancelInvoke(nameof(ResetEnemyAi));
        enemy.CurrentAIState = EnemyAIState.beingPushed;
        Invoke(nameof(ResetEnemyAi), 0.6f);
    }

    private void ResetEnemyAi()
    {
        enemy.CurrentAIState = EnemyAIState.patrol;
    }

}
