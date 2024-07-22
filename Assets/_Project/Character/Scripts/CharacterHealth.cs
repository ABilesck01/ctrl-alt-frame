using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	[SerializeField] private int maxHealth;
	[SerializeField] private int currentHealth;
	[SerializeField] private float backwardsPushForce;
	[SerializeField] private float upPushForce;
	private Rigidbody rb;

	protected bool isDead;

	public bool IsDead() => isDead;

	public int maxhealth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}


	public int Currenthealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}

	protected virtual void Awake()
	{
        Currenthealth = maxhealth;
		rb = GetComponent<Rigidbody>();
	}


	public virtual void TakeDamage(int damage, Transform attackPoint)
	{ 
		if(isDead)
		{
			return;
		}

		Vector3 direction = attackPoint.position - transform.position;
		rb.AddForce(-direction * backwardsPushForce, ForceMode.Force);
		rb.AddForce(Vector3.up * upPushForce, ForceMode.Force);

		Currenthealth -= damage;
		if( Currenthealth <= 0 )
		{
			Die();
			Debug.Log($"{gameObject.name} died");
		}
	}

	public virtual void Die()
	{
		isDead = true;
	}

	protected virtual void ResetCaracter()
	{
		currentHealth = maxHealth;
		isDead = false;
	}
}
