using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHealth : MonoBehaviour
{
	private int maxHealth;

	public int maxhealth
	{
		get { return maxHealth; }
		set { maxHealth = value; }
	}

	private int currentHealth;

	public int currenthealth
	{
		get { return currentHealth; }
		set { currentHealth = value; }
	}


	public virtual void TakeDamage(int damage)
	{ 
		currenthealth -= damage;

	}

}
