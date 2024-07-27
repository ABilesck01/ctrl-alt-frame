using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharacterHealth
{
    [SerializeField] private float hitTime;
    [Header("Animation")]
    [SerializeField] protected string hit_anim;
    [SerializeField] protected string death_anim;
    
    private Enemy enemy;
    private EnemyAnimation characterAnimation;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponent<Enemy>();
        characterAnimation = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        MiniGameController.instance.OnWrongMinigame.AddListener(ResetCaracter);
    }

    public override void TakeDamage(int damage, Transform attackPoint)
    {
        if (enemy.attack)
            pushForceTotal = 0;
        else
            pushForceTotal = 1;

        base.TakeDamage(damage, attackPoint);
        if (!isDead)
            enemy.enemyAudio.PlayHitSound();
        if(!enemy.attack)
            characterAnimation.PlayAnimation(hit_anim);

        CancelInvoke(nameof(ResetEnemyAi));
        enemy.CurrentAIState = EnemyAIState.beingPushed;
        Invoke(nameof(ResetEnemyAi), hitTime);
    }

    private void ResetEnemyAi()
    {
        if (!isDead)
        {
            pushForceTotal = 1;
            enemy.CurrentAIState = EnemyAIState.idle;
        }
        else
            enemy.CurrentAIState = EnemyAIState.minigame;
    }

    public override void Die()
    {
        enemy.enemyAudio.PlayDeathSound();
        base.Die();
        characterAnimation.PlayAnimation(death_anim);
    }

    protected override void ResetCaracter()
    {
        base.ResetCaracter();
        enemy.CurrentAIState = EnemyAIState.idle;
    }
}
