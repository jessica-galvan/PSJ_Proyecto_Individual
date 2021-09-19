using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolMovementController))]
public class EnemyFly : EnemyController
{
    [Header("Patrol Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject leftX;
    [SerializeField] private GameObject rightX;
    private GameObject barrierLeft;
    private GameObject barrierRight;
    private Vector2 spawnPoint;

    [Header("Prefab Settings")]
    [SerializeField] private GameObject invisibleBarrierPrefab;
    [SerializeField] private GameObject bullet;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource attackSound = null;

    [Header("Attack Settings")]
    [SerializeField] private float attackTimeDuration = 1f;
    [SerializeField] private float cooldown = 5f;
    [SerializeField] private float moveCooldown = 0.8f;
    [SerializeField] private Vector3 offset = Vector3.zero;


    //Extras
    private bool canMove;
    private float moveTimer = 0f;
    private Rigidbody2D _rigidBody;

    protected override void Start()
    {
        base.Start();
        

        canMove = true;
        canShoot = true;
        spawnPoint = transform.position;
        barrierLeft = Instantiate(invisibleBarrierPrefab, leftX.transform.position, transform.rotation);
        barrierLeft.GetComponent<PatrolEnemyFlip>().SetIsPatrol(false);
        barrierRight = Instantiate(invisibleBarrierPrefab, rightX.transform.position, transform.rotation);
        barrierRight.GetComponent<PatrolEnemyFlip>().SetIsPatrol(false);
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (CanAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
            {
                if (canShoot && Time.time > cooldownTimer) //cooldown y que ataque
                {
                    canShoot = false;
                    //canMove = false;
                    Attack();
                }
            }

            /*if (!canMove && Time.time > moveTimer) //Termino animación ataque? Se puede mover
            {
                canMove = true;
            }*/
        }
    }

    private void FixedUpdate()
    {
        if (canMove && !GameManager.instance.IsGameFreeze)
        {
            _rigidBody.velocity = transform.right * speed;
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyDamage);
    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyDead);
    }

    private void Attack()
    {
        canShoot = false;
        //canMove = false;
        //moveTimer = moveCooldown + Time.time;
        _animatorController.SetTrigger("IsAttacking");
        attackSound.Play();
        Instantiate(bullet, transform.position + offset, Quaternion.identity);
        cooldownTimer = cooldown + Time.time; 
        canShoot = true;
    }


    public void OnDestroy()
    {
        Destroy(barrierLeft);
        Destroy(barrierRight);
    }

    protected override void OnPlayerRespawnListener()
    {
        transform.position = spawnPoint;
    }

}
