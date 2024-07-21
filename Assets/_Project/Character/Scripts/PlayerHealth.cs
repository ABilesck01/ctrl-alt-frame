using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public PlayerHealthBar playerhealthbar;

    private Player player;

    private void Start()
    {
        playerhealthbar.SetMaxHealth(maxhealth);
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int damage, Transform attackPoint)
    {
        base.TakeDamage(damage, attackPoint);
        playerhealthbar.SetHealth(Currenthealth);
        player.playerMovement.canMove = false;
        Invoke(nameof(ResetMovement), 0.7f);
    }

    private void ResetMovement()
    {
        player.playerMovement.canMove = true;
    }

}
