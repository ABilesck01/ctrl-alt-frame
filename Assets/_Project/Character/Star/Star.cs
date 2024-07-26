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

    private Transform player;

    private Vector3 startPoint;
    private Vector3 randomPoint;
    private Vector3 direction;

    private StarMovement starMovement;

    protected override void Awake()
    {
        base.Awake();
        starMovement = GetComponent<StarMovement>();
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
        //enemyAnimation.HandleMovementAnimation(1);
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("Player") && currentStarSate == StarState.running)
        {
            currentStarSate = StarState.FollowingPlayer;
        }
    }

    private void SetNewRandomPoint()
    {
        float x = Random.Range(-runningDistance, runningDistance) + startPoint.x;
        float z = Random.Range(-runningDistance, runningDistance) + startPoint.z;
        
        x *= 0.95f;
        z *= 0.95f;

        randomPoint = new Vector3(x, 0, z);
    }
}
public enum StarState
{
    running,
    FollowingPlayer
}