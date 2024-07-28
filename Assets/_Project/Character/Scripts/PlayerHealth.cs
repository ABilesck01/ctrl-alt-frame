using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : CharacterHealth
{
    public PlayerHealthBar playerhealthbar;
    [Space]
    [SerializeField] private Color forecolor;
    [SerializeField] private Color backcolor;
    private Player player;

    private Vector3 startPos;

    protected override void Awake()
    {
        base.Awake();

        startPos = transform.position;
    }

    private void Start()
    {
        playerhealthbar.SetMaxHealth(MaxHealth);
        player = GetComponent<Player>();
    }

    public override void TakeDamage(int damage, Transform attackPoint)
    {
        base.TakeDamage(damage, attackPoint);
        PlayerCamera.instance.ShakeCamera(.15f, .16f);
        playerhealthbar.SetHealth(Currenthealth);
        player.playerMovement.canMove = false;
        if(!isDead)
            Invoke(nameof(ResetMovement), 0.7f);
    }

    private void ResetMovement()
    {
        player.playerMovement.canMove = true;
    }

    public override void Die()
    {
        base.Die();
        CancelInvoke();
        player.playerMovement.canMove = false;
        PlayerPopUpsController.instance.ShowPopUp("You died", forecolor, backcolor);
        Invoke(nameof(ResetCaracter), 5f);
    }

    protected override void ResetCaracter()
    {
        Debug.Log("Reset character");
        base.ResetCaracter();
        playerhealthbar.SetHealth(Currenthealth);
        player.playerMovement.canMove = true;
        transform.position = startPos;
    }
}
