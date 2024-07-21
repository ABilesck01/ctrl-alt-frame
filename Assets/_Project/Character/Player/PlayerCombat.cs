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
}
