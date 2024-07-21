using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public PlayerHealthBar playerhealthbar;

    private void Start()
    {
        playerhealthbar.SetMaxHealth(maxhealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        playerhealthbar.SetHealth(currenthealth);
    }

}
