using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolMovementController : MonoBehaviour
{
    private EnemyController enemyController;
    protected ActorStats _actorStats;
    private AttackStats _attackStats;

    [Header("Patrol Settings")]
    [SerializeField] private bool IsGroundEnemy;
    [SerializeField] private GameObject leftX;
    [SerializeField] private GameObject rightX;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    [SerializeField] private float checkPlayerTimeDuration = 5f;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject invisibleBarrierPrefab;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform playerDetectionPoint;


    private Vector2 spawnPoint;
    private GameObject barrierLeft;
    private GameObject barrierRight;
    private PlayerController player;
    private float playerDetectionDistance;

    //Timers
    private float checkPlayerTimer;
    private float moveTimer;

    //Bools
    private bool followingPlayer;
    private bool isBarrierActive;
    private bool checkDirection;
    private bool canReturnToSpawnPoint;

    [Header("Attack Settings")]
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float moveCooldown = 0.8f;

    public float CurrentSpeed { get; private set; }
    public bool CanMove { get; set; }
    public bool IsPlayerInRange { get; private set; }

    void Start()
    {
        //_rigidBody = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();
        spawnPoint = transform.position;
        playerDetectionDistance = Vector2.Distance(transform.position, playerDetectionPoint.position);  //Con esto sacamos a cuanta distancia puede ver. 
        CreateBarriers();
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze && !enemyController.LifeController.IsDead)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.right, playerDetectionDistance, _attackStats.TargetList);
            if (hit) //Hace Raycast, si ves al player...
            {
                player = hit.collider.GetComponent<PlayerController>();
                if(player != null)
                    GoAttackPlayer(hit);
            }
            else   //Si dejaste de ver al player, espera un rato y patrulla
            {
                ReturnToPatrol();
            }

            if (IsGroundEnemy)
                CheckGroundDetection();

            Move();
        }
    }

    public void Move()
    {
        if(CanMove && !GameManager.instance.IsGameFreeze && !enemyController.IsAttacking)
            transform.position += transform.right * CurrentSpeed * Time.deltaTime;
    }

    private void CreateBarriers()
    {
        isBarrierActive = true;
        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierLeft.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        barrierRight.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
    }

    private void GoAttackPlayer(RaycastHit2D hit)
    {
        if (!followingPlayer && player)  //Desactiva las barreras de patruyar para perseguirlo
        {
            statusBarriers(false);
            followingPlayer = true;
            checkDirection = true;
            canReturnToSpawnPoint = false;
            CurrentSpeed = _actorStats.BuffedSpeed;
        }

        float distance = Vector2.Distance(hit.collider.transform.position, attackPoint.position);
        if (distance <= attackRadius) //Y si esta a una distancia menor o igual al radio de ataque, dejate de mover. 
        {
            CanMove = false;
            IsPlayerInRange = true;
            enemyController.TargetDetected(true);
        }
        else
        {
            IsPlayerInRange = false;
            if (!CanMove && Time.time > moveTimer) //Termino animación ataque? Se puede mover
                CanMove = true;
        }
    }

    private void ReturnToPatrol()
    {
        if (followingPlayer) //Si estabas siguiendo al player
        {
            checkPlayerTimer = checkPlayerTimeDuration + Time.time;
            CanMove = false;
            CurrentSpeed = _actorStats.OriginalSpeed;
            followingPlayer = false;
        }

        if (!canReturnToSpawnPoint && Time.time > checkPlayerTimer) //PERO espera unos segundos para al spawnPoint
        {
            canReturnToSpawnPoint = true;
            CanMove = true;
        }

        if (canReturnToSpawnPoint && !isBarrierActive) //Ahora podes volver al punto de spawn
        {
            if (checkDirection) //hace un check de la direcion del spawnpoint
                checkSpawnPointDirection();

            float difMax = Vector2.Distance(transform.position, spawnPoint);  //cuando estes cerca, activa las barreras asi patruyas
            if (difMax < 1f)
            {
                canReturnToSpawnPoint = false;
                statusBarriers(true);
            }
        }
    }

    private void CheckGroundDetection()
    {
        RaycastHit2D hitPatrol = Physics2D.Raycast(transform.position, Vector2.down, groundDetectionDistance, groundDetectionList);
        if (!hitPatrol)
            enemyController.BackFlip();
    }

    private void statusBarriers(bool status)
    {
        isBarrierActive = status;
        barrierLeft.SetActive(isBarrierActive);
        barrierRight.SetActive(isBarrierActive);
    }

    public void SetStats(ActorStats actor, AttackStats attacks)
    {
        _actorStats = actor;
        _attackStats = attacks;
        CurrentSpeed = _actorStats.OriginalSpeed;
    }

    private void checkSpawnPointDirection()
    {
        checkDirection = false;
        if (spawnPoint.x > transform.position.x && !enemyController.FacingRight) //Si el spawnpint es mayor a la posicion del enemigo, y no esta mirando a la derecha...
            enemyController.BackFlip();
        else if (spawnPoint.x < transform.position.x && enemyController.FacingRight) //si o si esta este else if porque solo tiene que flipear si esta mirando en la direccion contraria, sino ni flipea. 
            enemyController.BackFlip();
    }

    public void OnDestroy() 
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }
}