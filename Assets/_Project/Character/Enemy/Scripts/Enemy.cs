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
    [SerializeField] private float timeBtwAttacks = 0.75f;
    [Header("Sequence")]
    [SerializeField] private GameObject sequenceUp;
    [SerializeField] private GameObject sequenceDown;
    [SerializeField] private GameObject sequenceLeft;
    [SerializeField] private GameObject sequenceRight;

    private float currentDistanceToRetreat;
    private Vector3 direction;
    private Vector3 startPoint;
    private Vector3 randomPoint;
    private Transform player;
    private PlayerHealth playerHealth;
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
        randomPoint = startPoint;

        //MiniGameController.instance.OnCorrectMinigame

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    protected override void Update()
    {
        if(currentAIState == EnemyAIState.beingPushed)
        {
            return;
        }

        if (currentAIState == EnemyAIState.minigame)
        {
            enemyAnimation.HandleMovementAnimation(0);
            return;
        }

        currentDistanceToRetreat = Vector3.Distance(startPoint, transform.position);
        bool retreat = currentDistanceToRetreat > distanceToRetreat && currentAIState != EnemyAIState.retreating;
        if (retreat || playerHealth.IsDead())
        {
            currentAIState = EnemyAIState.retreating;
        }

        CheckView();
        CalculateDirection();

        switch (currentAIState)
        {
            case EnemyAIState.idle: //walk randomly

                if(Vector3.Distance(randomPoint, transform.position) < 0.1f)
                {
                    //New random point
                    float x = Random.Range(-distanceToRetreat / 2, distanceToRetreat / 2) + startPoint.x;
                    float z = Random.Range(-distanceToRetreat / 2, distanceToRetreat / 2) + startPoint.z;

                    randomPoint = new Vector3(x, 0, z);

                }

                direction = randomPoint - transform.position;
                enemyMovement.HandleMovement(new Vector2(direction.x, direction.z), MovementSpeed);
                enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
                enemyAnimation.HandleMovementAnimation(1);

                break;
            case EnemyAIState.chase: //chase player
                ChasePlayer();
                break;
            case EnemyAIState.attack: //attack the player
                AttackPlayer();
                break;
            case EnemyAIState.minigame: //stop ai from chasing and attacking the player
                enemyAnimation.HandleMovementAnimation(0);
                break;
            case EnemyAIState.retreating:
                Retreat();
                break;
        }
    }

    private void Retreat()
    {
        direction = startPoint - transform.position;
        enemyMovement.HandleMovement(new Vector2(direction.x, direction.z), MovementSpeed);
        enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
        enemyAnimation.HandleMovementAnimation(1);
        if (currentDistanceToRetreat < 0.1f)
        {
            currentAIState = EnemyAIState.idle;
            currentDistanceToRetreat = 0;
        }
    }

    private void AttackPlayer()
    {
        enemyMovement.HandleMovement(Vector2.zero, 0); //stop the enemy;
        enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
        enemyAnimation.HandleMovementAnimation(0);

        if (!attack)
        {
            enemyCombat.HandlePunch();
            attack = true;
            Invoke(nameof(ResetAttack), timeBtwAttacks);
        }
    }

    private void ChasePlayer()
    {
        enemyMovement.HandleMovement(new Vector2(direction.x, direction.z), MovementSpeed);
        enemyMovement.handleRotation(new Vector2(direction.x, direction.z));
        enemyAnimation.HandleMovementAnimation(1);
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

        if (currentAIState == EnemyAIState.minigame)
        {
            return;
        }

        if (playerHealth.IsDead()) return;

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

        currentAIState = EnemyAIState.idle;   
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

    public void ShowSequence(Sequence s)
    {
        GameObject sign = sequenceUp;

        switch (s)
        {
            case Sequence.up:
                sign = sequenceUp;
                break;
            case Sequence.down:
                sign = sequenceDown;
                break;
            case Sequence.left:
                sign = sequenceLeft;
                break;
            case Sequence.right:
                sign = sequenceRight;
                break;
        }

        GameObject signInstance = Instantiate(sign, transform.position, Quaternion.identity);
        Destroy(signInstance, 1f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, viewRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, distanceToRetreat);
    }

}

public enum EnemyAIState
{
    idle,
    chase,
    attack,
    minigame,
    retreating,
    beingPushed
}
