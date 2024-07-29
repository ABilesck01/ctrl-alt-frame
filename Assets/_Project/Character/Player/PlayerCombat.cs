using Spine.Unity;
using System;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    private Player player;
    private SkeletonAnimation skeletonAnimation;

    private Enemy[] enemies;
    private bool hasCombat = false;

    protected override void Awake()
    {
        base.Awake(); 
        this.player = this.GetComponent<Player>();
        skeletonAnimation = this.GetComponentInChildren<SkeletonAnimation>();
    }

    public override void HandlePunch()
    {
        if (player.isAttacking)
            return;

        player.isAttacking = true;
        Invoke(nameof(ResetAttack), 0.8f);
        Invoke(nameof(HandleAttack), 0.55f);
        player.playerAnimation.PlayAnimation("axolotl_attack1");
        //HandleAttack();=
    }

    private void ResetAttack()
    {
        player.isAttacking = false;
    }

    private void Start()
    {
        skeletonAnimation.state.Event += State_Event;
        enemies = FindObjectsOfType<Enemy>();
    }

    private void OnDisable()
    {
        skeletonAnimation.state.Event += State_Event;
    }

    private void State_Event(Spine.TrackEntry trackEntry, Spine.Event e)
    {
        if (e.Data.Name.ToLower().Contains("enable_hit"))
        {
            HandleAttack();
        }
    }

    public void HandleAttack()
    {
        Debug.Log("Handle attack");
        Collider[] allHits = Physics.OverlapSphere(attackPoint.position, damageRadius, hitLayer);
        foreach (Collider hit in allHits)
        {
            if (hit.TryGetComponent(out EnemyHealth characterHealth))
            {
                characterHealth.TakeDamage(damage, transform);
            }
        }
    }

    //private void Update()
    //{
    //    var attackers = Array.Find(enemies, e => e.currentAIState == EnemyAIState.chase || e.currentAIState == EnemyAIState.attack);
    //    if(attackers != null)
    //    {
    //        if(!hasCombat)
    //        {
    //            SongController.instance.SetSongParameter(4);
    //            hasCombat = true;
    //        }
    //    }
    //    else
    //    {
    //        if (hasCombat)
    //        {
    //            SongController.instance.SetSongParameter(2);
    //            hasCombat = false;
    //        }
    //    }
    //}
}
