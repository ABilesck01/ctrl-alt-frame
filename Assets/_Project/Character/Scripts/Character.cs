using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
	[SerializeField] private Color characterColor;
	[SerializeField] private float movementSpeed;
	[Header("Visual")]
	[SerializeField] protected SpriteRenderer spriteRenderer;
	[SerializeField] protected Light characterLight;

	public float MovementSpeed
	{
		get { return movementSpeed; }
		set { movementSpeed = value; }
	}

    public Color CharacterColor
    {
        get { return characterColor; }
        set { characterColor = value; }
    }

	protected virtual void Awake()
	{
		if(spriteRenderer != null)
		{
			spriteRenderer.color = characterColor;
		}
		if(characterColor != null)
		{
			characterLight.color = characterColor;
		}
	}

	protected virtual void Update()
	{

	}
}
