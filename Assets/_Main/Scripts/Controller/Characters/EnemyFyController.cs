using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(DetectTargetArea))]
public class EnemyFyController : EnemyController
{
    public MagicalShooterController MagicController { get; private set; }
    public PatrolMovementController PatrolMovementController { get; private set; }

    //Extras
    private DetectTargetArea detectionArea;
    private Vector2 spawnPoint;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        PatrolMovementController = GetComponent<PatrolMovementController>();
        MagicController = GetComponent<MagicalShooterController>();
        detectionArea = GetComponent<DetectTargetArea>();
        spawnPoint = transform.position;
    }

    void Update()
    {
        if(!GameManager.instance.IsGameFreeze)
        {
            CheckArea();

            PatrolMovementController.Patrol();
            if(!isAttacking)
                PatrolMovementController.Move(_actorStats.OriginalSpeed);

            if (CanAttack && canShoot && !isAttacking)
            {
                Attack();
            }

            cooldownTimer -= Time.deltaTime;
            if (!canShoot && cooldownTimer <= Time.deltaTime)
                canShoot = true;
        }
    }
    private void CheckArea()
    {
        detectionArea.CheckArea();
        CanAttack = detectionArea.DetectTarget();
        player = detectionArea.Player;
    }

    private void Attack()
    {
        canShoot = false;
        isAttacking = true;
        _animatorController.SetTrigger("IsAttacking");
        AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyAttack);
        //Instantiate(bullet, transform.position + offset, Quaternion.identity);
        cooldownTimer = _attackStats.CooldownMana;
    }

    private void CanMoveAgain()
    {
        isAttacking = false;
        //TODO: when stops attack animation, it can move again. 
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyDamage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyDead);
    }

    protected override void OnPlayerRespawnListener()
    {
        transform.position = spawnPoint;
    }
}
