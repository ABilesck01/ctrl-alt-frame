using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : CharacterCombat
{
    private Player player;

    protected override void Awake()
    {
        base.Awake(); 
        this.player = this.GetComponent<Player>();
    }

    public override void HandlePunch()
    {
        player.playerAnimation.PlayAnimation("swoosh");
    }

    public void HandleAttack()
    {
        Collider[] allHits = Physics.OverlapSphere(attackPoint.position, damageRadius, hitLayer);
        foreach (Collider hit in allHits)
        {
            if (hit.TryGetComponent(out EnemyHealth characterHealth))
            {
                characterHealth.TakeDamage(damage, transform);
            }
        }
    }
}
