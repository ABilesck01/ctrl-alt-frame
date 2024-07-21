using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private EnemyAIState currentAIState;
    [SerializeField] private float viewRadius = 3;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private float attackDistance = 1;
    [SerializeField] private float distanceToRetreat = 1;
    [SerializeField] private float currentDistanceToRetreat;
    [SerializeField] private float timeBtwAttacks = 0.75f;

    private Vector3 direction;
    private Vector3 startPoint;
    private Transform player;
    private bool attack = false;

    private EnemyMovement enemyMovement;
    private EnemyCombat enemyCombat;
    [HideInInspector] public EnemyAnimation enemyAnimation;

    public EnemyAIState CurrentAIState
    {
        get { return currentAIState; }
        set { currentAIState = value; }
    }

    protected override void Awake()
    {
        base.Awake();

        enemyMovement = GetComponent<EnemyMovement>();
        enemyCombat = GetComponent<EnemyCombat>();
        enemyAnimation = GetComponent<EnemyAnimation>();
    }

    private void Start()
    {
        startPoint = transform.position;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        if(currentAIState == EnemyAIState.beingPushed)
        {
            return;
        }

        currentDistanceToRetreat = Vector3.Distance(startPoint, transform.position);
        if (currentDistanceToRetreat > distanceToRetreat && currentAIState != EnemyAIState.retreating)
        {
            currentAIState = EnemyAIState.retreating;
        }

        CheckView();
        CalculateDirection();

        switch (currentAIState)
        {
            case EnemyAIState.patrol: //walk randomly
                enemyAnimation.HandleMovementAnimation(0);
                break;
            case EnemyAIState.chase: //chase player
                enemyMovement.HandleMovement(new Vector2(direction.x, direction.z), MovementSpeed);
                enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
                enemyAnimation.HandleMovementAnimation(1);
                break;
            case EnemyAIState.attack: //attack the player
               
                enemyMovement.HandleMovement(Vector2.zero, 0); //stop the enemy;
                enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
                enemyAnimation.HandleMovementAnimation(0);

                if(!attack)
                {
                    enemyCombat.HandlePunch();
                    attack = true;
                    Invoke(nameof(ResetAttack), timeBtwAttacks);
                }

                break;
            case EnemyAIState.minigame: //stop ai from chasing and attacking the player
                enemyAnimation.HandleMovementAnimation(0);
                break;
            case EnemyAIState.retreating:
                direction = startPoint - transform.position;
                enemyMovement.HandleMovement(new Vector2(direction.x, direction.z), MovementSpeed);
                enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
                enemyAnimation.HandleMovementAnimation(1);
                if(currentDistanceToRetreat < 0.1f)
                {
                    currentAIState = EnemyAIState.patrol;
                    currentDistanceToRetreat = 0;
                }
                break;
        }
    }

    private void ResetAttack()
    {
        attack = false;
    }

    private void CheckView()
    {
        if(currentAIState == EnemyAIState.retreating)
        {
            return;
        }

        bool hasPlayerInView = Physics.CheckSphere(transform.position, viewRadius, playerLayer);
        if (hasPlayerInView)
        {
            if(Vector3.Distance(player.position, transform.position) < attackDistance)
            {
                currentAIState = EnemyAIState.attack;
                return;
            }
            currentAIState = EnemyAIState.chase;
            return;
        }

        currentAIState = EnemyAIState.patrol;   
    }

    private void CalculateDirection()
    {
        if (currentAIState == EnemyAIState.retreating)
        {
            return;
        }

        direction = player.position - transform.position;
        direction.Normalize();
    }

    private void Patrol()
    {

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }

}

public enum EnemyAIState
{
    patrol,
    chase,
    attack,
    minigame,
    retreating,
    beingPushed
}
