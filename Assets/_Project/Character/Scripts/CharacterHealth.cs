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
		Vector3 direction = attackPoint.position - transform.position;
		rb.AddForce(-direction * backwardsPushForce, ForceMode.Force);
		rb.AddForce(Vector3.up * upPushForce, ForceMode.Force);

		Currenthealth -= damage;

	}

}
