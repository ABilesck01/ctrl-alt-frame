using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Star : Character
{
    [SerializeField] private StarState currentStarSate;
    [Header("Running")]
    [SerializeField] private float runningDistance = 15;
    [SerializeField] private float runningSpeed = 3.1f;
    [Header("Following Player")]
    [SerializeField] private float minDistanceToPlayer;
    [SerializeField] private float followSpeed = 3f;
    [Header("Animation")]
    [SerializeField] private string glowAnimation;
    [Header("Minigame")]
    [SerializeField] private DOTweenAnimation enemyAnimation;
    [SerializeField] private Enemy enemy;
    [Header("Sequence")]
    [SerializeField] private string upAnimation;
    [SerializeField] private string downAnimation;
    [SerializeField] private string leftAnimation;
    [SerializeField] private string rightAnimation;
    private Transform player;

    private Vector3 startPoint;
    private Vector3 randomPoint;
    private Vector3 direction;

    private StarMovement starMovement;
    private StarAnimation starAnimation;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();
        starMovement = GetComponent<StarMovement>();
        starAnimation = GetComponent<StarAnimation>();
        rb = GetComponent<Rigidbody>();
        currentStarSate = StarState.running;
    }

    private void Start()
    {
        startPoint = transform.position;
        randomPoint = startPoint;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    protected override void Update()
    {
        switch (currentStarSate)
        {
            case StarState.running:
                Run();
                break;
            case StarState.FollowingPlayer:
                FollowPlayer(); 
                break;
        }
    }

    private void Run()
    {
        if(Vector3.Distance(randomPoint, transform.position) < 0.1f)
        {
            SetNewRandomPoint();
        }

        direction = randomPoint - transform.position;
        starMovement.HandleMovement(new Vector2(direction.x, direction.z), runningSpeed);
        starMovement.handleRotation(new Vector2(direction.x, direction.z));
        starAnimation.HandleMovementAnimation(2);
    }

    private void FollowPlayer()
    {
        direction = player.position - transform.position;
        if(Vector3.Distance(player.position, transform.position) > minDistanceToPlayer)
        {
            starMovement.HandleMovement(new Vector2(direction.x, direction.z), followSpeed);
            starMovement.handleRotation(new Vector2(direction.x, direction.z));
        }
        else
        {
            starMovement.HandleMovement(Vector2.zero, followSpeed);
            starMovement.handleRotation(new Vector2(direction.x, direction.z));
        }

        if (rb.velocity.magnitude > 0.1)
            starAnimation.HandleMovementAnimation(1);
        else
            starAnimation.HandleMovementAnimation(0);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && currentStarSate == StarState.running)
        {
            currentStarSate = StarState.minigame;
            MiniGameController.instance.StartMinigame(this);
            rb.velocity = Vector3.zero;
        }
    }

    private void SetNewRandomPoint()
    {
        Vector3 possibleRandomPoint = Vector3.zero;

        while (Vector3.Angle(possibleRandomPoint, player.position) < 35)
        {
            float x = Random.Range(-runningDistance, runningDistance) + startPoint.x;
            float z = Random.Range(-runningDistance, runningDistance) + startPoint.z;

            x *= 0.95f;
            z *= 0.95f;

            possibleRandomPoint = new Vector3(x, 0, z);
        }

        randomPoint = possibleRandomPoint;
    }

    public void ShowSequence(Sequence sequence)
    {
        Invoke(nameof(ResetAnimation), 1.1f);

        switch (sequence)
        {
            case Sequence.up:
                starAnimation.PlayAnimation(upAnimation);
                break;
            case Sequence.down:
                starAnimation.PlayAnimation(downAnimation);
                break;
            case Sequence.left:
                starAnimation.PlayAnimation(leftAnimation);
                break;
            case Sequence.right:
                starAnimation.PlayAnimation(rightAnimation);
                break;
        }
    }

    private void ResetAnimation()
    {
        starAnimation.PlayAnimation(glowAnimation);
    }

    public void UnlockStar()
    {
        currentStarSate = StarState.FollowingPlayer;
    }

    public void WrongMinigame()
    {
        StartCoroutine(UnlockStarCoroutine());
    }

    private IEnumerator UnlockStarCoroutine()
    {
        yield return new WaitForSeconds(0.1f);
        starAnimation.DOPlay();
        yield return new WaitForSeconds(0.41f);
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        Instantiate(enemy, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
public enum StarState
{
    running,
    FollowingPlayer,
    minigame
}