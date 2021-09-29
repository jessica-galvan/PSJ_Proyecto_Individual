using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerController : MonoBehaviour
{
    [Header("Patrol Settings")]
    [SerializeField] private float checkPlayerTimeDuration = 5f;

    [Header("Prefab Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform playerDetectionPoint;

    private EnemyController enemyController;
    private ActorStats _actorStats;
    private Vector2 spawnPoint;
    private float playerDetectionDistance;
    private float checkPlayerTimer;
    private float moveTimer = 0f;
    private bool canReturnToSpawnPoint;
    private bool checkDirection;
    private Transform target;

    public float CurrentSpeed { get; private set; }
    public bool IsFollowingPlayer { get; private set; }
    public bool IsPlayerInRange { get; private set; }
    public bool CanMove { get; private set; }

    #region Private
    private void Start()
    {
        spawnPoint = transform.position;
        enemyController = GetComponent<EnemyController>();
        playerDetectionDistance = Vector2.Distance(transform.position, playerDetectionPoint.position);  //Con esto sacamos a cuanta distancia puede ver. 
        CanMove = true;
    }
    private void SetToFollow()
    {
        if (!IsFollowingPlayer)
        {
            IsFollowingPlayer = true;
            canReturnToSpawnPoint = false;
            CurrentSpeed = _actorStats.BuffedSpeed;
        }
        CheckIfPlayerInRange();
    }

    private void ReturnToSpawnPoint()
    {
        if (CanMove && target != null) //Si estabas siguiendo al player
        {
            target = null;
            IsPlayerInRange = false;
            CanMove = false;
            CurrentSpeed = _actorStats.OriginalSpeed;
            checkPlayerTimer = checkPlayerTimeDuration;
        }

        checkPlayerTimer -= Time.deltaTime;
        if (!canReturnToSpawnPoint && checkPlayerTimer <= 0)
        {
            CanMove = true;
            canReturnToSpawnPoint = true;
            checkDirection = true;
        }

        if (canReturnToSpawnPoint) //Ahora podes volver al punto de spawn
        {
            if (checkDirection) //hace un check de la direcion del spawnpoint
                CheckSpawnPointDirection();


            float difMax = Vector2.Distance(transform.position, spawnPoint); //Si estas cerca del spawnPoint.. 
            if (difMax < 1f)
            {
                canReturnToSpawnPoint = false;
                IsFollowingPlayer = false;
            }
        }
        print("Can Move: " + CanMove);
    }
    private void CheckSpawnPointDirection()
    {
        checkDirection = false;
        if (spawnPoint.x > transform.position.x && !enemyController.FacingRight) //Si el spawnpint es mayor a la posicion del enemigo, y no esta mirando a la derecha...
            enemyController.BackFlip();
        else if (spawnPoint.x < transform.position.x && enemyController.FacingRight) //si o si esta este else if porque solo tiene que flipear si esta mirando en la direccion contraria, sino ni flipea. 
            enemyController.BackFlip();
    }
    #endregion

    #region Public
    public void CheckIfPlayerIsInView()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistance, enemyController.AttackStats.TargetList);
        if (hit) //Hace Raycast, si ves al player...
        {
            var player = hit.collider.GetComponent<PlayerController>();
            if (player != null)
            {
                target = hit.collider.transform;
                SetToFollow(); //Anda a atacarlo
            }
                
        }
        else
        {
            if (IsFollowingPlayer && !IsPlayerInRange)
                ReturnToSpawnPoint(); //Si no lo ves, volve al spawnPoint
        }
    }

    public void CheckIfPlayerInRange()
    {
        if (target != null)
        {
            float distance = Vector2.Distance(target.position, attackPoint.position);
            if (distance <= enemyController.AttackStats.PhysicalAttackRadious) //Y si esta a una distancia menor o igual al radio de ataque, dejate de mover. 
            {
                CanMove = false;
                IsPlayerInRange = true;
                enemyController.TargetDetected(true);
            }
            else
            {
                IsPlayerInRange = false;
                enemyController.TargetDetected(false);
                if (!CanMove && Time.time > moveTimer) //Termino animación ataque? Se puede mover
                    CanMove = true;
            }
        }
        else
        {
            IsPlayerInRange = false;
        }
    }
    public void SetStats(ActorStats actor)
    {
        _actorStats = actor;
        CurrentSpeed = _actorStats.OriginalSpeed;
    }
    #endregion
}
