using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    [SerializeField] protected Transform attackPoint;
    [SerializeField] protected int damage;
    [SerializeField] protected int damageRadius;
    [SerializeField] protected LayerMask hitLayer;

    protected virtual void Awake()
    {

    }

    public virtual void HandlePunch()
    {

    }

    //public void HandleAttack()
    //{
    //    Collider[] allHits = Physics.OverlapSphere(attackPoint.position, damageRadius, hitLayer);
    //    foreach (Collider hit in allHits)
    //    {
    //        if (hit.TryGetComponent(out CharacterHealth characterHealth))
    //        {
    //            characterHealth.TakeDamage(damage, transform);
    //        }
    //    }
    //}
}
