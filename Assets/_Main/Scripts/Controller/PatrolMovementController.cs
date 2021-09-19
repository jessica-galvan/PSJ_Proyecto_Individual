using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovementController : MonoBehaviour
{
    private EnemyController enemyController;
    protected ActorStats _actorStats;
    private AttackStats _attackStats;
    protected Rigidbody2D _rigidBody;

    [Header("Patrol Settings")]
    [SerializeField] private bool IsGroundEnemy;
    [SerializeField] private GameObject leftX;
    [SerializeField] private GameObject rightX;
    [SerializeField] private LayerMask groundDetectionList;
    [SerializeField] private float groundDetectionDistance = 1f;
    [SerializeField] private float checkPlayerTimeDuration = 5f;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject invisibleBarrierPrefab;
    [SerializeField] private Transform groundDetectionPoint;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private Transform playerDetectionPoint;

    private float currentSpeed;
    private Vector2 spawnPoint;
    private GameObject barrierLeft;
    private GameObject barrierRight;
    private PlayerController player;

    //Timers
    private float checkPlayerTimer;
    private float moveTimer;

    //Bools
    private bool followingPlayer;
    private bool isBarrierActive;
    private bool checkDirection;
    private bool canReturnToSpawnPoint;
    private bool canMove;

    [Header("Attack Settings")]
    [SerializeField] private float attackRadius = 1f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float moveCooldown = 0.8f;
    private float playerDetectionDistance;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        enemyController = GetComponent<EnemyController>();

        isBarrierActive = true;
        currentSpeed = _actorStats.OriginalSpeed;

        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierLeft.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        barrierRight.GetComponent<PatrolEnemyFlip>().SetIsPatrol(true);
        spawnPoint = transform.position;
        playerDetectionDistance = Vector2.Distance(transform.position, playerDetectionPoint.position);  //Con esto sacamos a cuanta distancia puede ver. 
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
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
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !GameManager.instance.IsGameFreeze)
        {
            _rigidBody.velocity = transform.right * currentSpeed;
        }
    }

    private void GoAttackPlayer(RaycastHit2D hit)
    {
        if (!followingPlayer && player)  //Desactiva las barreras de patruyar para perseguirlo
        {
            statusBarriers(false);
            followingPlayer = true;
            checkDirection = true;
            canReturnToSpawnPoint = false;
            currentSpeed = _actorStats.BuffedSpeed;
        }

        float distance = Vector2.Distance(hit.collider.transform.position, attackPoint.position);
        if (distance <= attackRadius) //Y si esta a una distancia menor o igual al radio de ataque, dejate de mover. 
        {
            canMove = false;
            enemyController.TargetDetected(true);
        }
        else
        {
            if (!canMove && Time.time > moveTimer) //Termino animación ataque? Se puede mover
                canMove = true;
        }
    }

    private void ReturnToPatrol()
    {
        if (followingPlayer) //Si estabas siguiendo al player
        {
            checkPlayerTimer = checkPlayerTimeDuration + Time.time;
            canMove = false;
            currentSpeed = _actorStats.OriginalSpeed;
            followingPlayer = false;
        }

        if (!canReturnToSpawnPoint && Time.time > checkPlayerTimer) //PERO espera unos segundos para al spawnPoint
        {
            canReturnToSpawnPoint = true;
            canMove = true;
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
        RaycastHit2D hitPatrol = Physics2D.Raycast(groundDetectionPoint.position, Vector2.down, groundDetectionDistance, groundDetectionList);
        if (!hitPatrol)
            enemyController.BackFlip();
    }

    private void statusBarriers(bool status)
    {
        isBarrierActive = status;
        barrierLeft.SetActive(isBarrierActive);
        barrierRight.SetActive(isBarrierActive);
    }

    private void checkSpawnPointDirection()
    {
        checkDirection = false;
        if (spawnPoint.x > transform.position.x && !enemyController.FacingRight) //Si el spawnpint es mayor a la posicion del enemigo, y no esta mirando a la derecha...
            enemyController.BackFlip();
        else if (spawnPoint.x < transform.position.x && enemyController.FacingRight) //si o si esta este else if porque solo tiene que flipear si esta mirando en la direccion contraria, sino ni flipea. 
            enemyController.BackFlip();
    }

    public void OnDestroy() //Para que destruya las barreras cuando se destruye el objeto. 
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }
}