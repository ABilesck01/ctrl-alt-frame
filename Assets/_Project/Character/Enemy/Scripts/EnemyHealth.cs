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

    private void Start()
    {
        MiniGameController.instance.OnWrongMinigame.AddListener(ResetCaracter);
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
        if (!isDead)
            enemy.CurrentAIState = EnemyAIState.idle;
        else
            enemy.CurrentAIState = EnemyAIState.minigame;
    }

    public override void Die()
    {
        base.Die();

    }

    protected override void ResetCaracter()
    {
        base.ResetCaracter();
        enemy.CurrentAIState = EnemyAIState.idle;
    }
}
