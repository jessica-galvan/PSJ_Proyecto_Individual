using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    [SerializeField] private Transform[] patrolPoints;

    [Header("Ground Patrol Settings")]
    [SerializeField] private bool IsGroundEnemy;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDistance = 1f;

    //private ActorStats _actorStats;
    private EnemyController enemyController;
    private int currentPosition;
    private Vector2[] route;
    private Vector2 currentTarget;
    private bool canFlip;

    void Start()
    {
        enemyController = GetComponent<EnemyController>();
        CreatePatrolRoute();
    }

    private void CreatePatrolRoute()
    {
        canFlip = true;

        route = new Vector2[patrolPoints.Length];
        for (int i = 0; i < patrolPoints.Length; i++)
        {
            route[i] = patrolPoints[i].position;
        }
    }

    public void Move(float speed)
    {
        if(!GameManager.instance.IsGameFreeze && !enemyController.IsAttacking)
            transform.position += transform.right * speed * Time.deltaTime;
    }

    public void Patrol()
    {
        if (currentPosition < route.Length)
        {
            currentTarget = route[currentPosition];

            var distance = Vector2.Distance(transform.position, currentTarget);
            if (distance < 0.1f && canFlip)
            {
                canFlip = false;
                enemyController.BackFlip();

                if (currentPosition == route.Length - 1)
                    ResetPosition();
                else
                    currentPosition++;
            }
            else if (distance > 0.5)
                canFlip = true;
        }
    }

    public void CheckGroundDetection()
    {
        RaycastHit2D hitPatrol = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundDetectionList);
        if (!hitPatrol)
            enemyController.BackFlip();
    }

    public void ResetPosition()
    {
        currentPosition = 0;
    }
}