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
    private Transform target;
    private bool isAttacking;

    protected override void Start()
    {
        base.Start();
        PatrolMovementController = GetComponent<PatrolMovementController>();
        MagicController = GetComponent<MagicalShooterController>();
        detectionArea = GetComponent<DetectTargetArea>();
    }

    void Update()
    {
        if(!GameManager.instance.IsGameFreeze)
        {
            CheckArea();

            PatrolMovementController.Patrol();
            if (!isAttacking)
                PatrolMovementController.Move(_actorStats.OriginalSpeed);

            if (CanAttack && canShoot && !isAttacking && !MagicController.IsAttacking)
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
        if(player != null)
        {
            target = player.transform;
            canShoot = false;
            isAttacking = true;
            _animatorController.SetTrigger("IsAttacking");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.FlyAttack);
            cooldownTimer = _attackStats.CooldownMana;
        }

    }

    private void DoShoot()
    {
        MagicController.Shoot(target);
    }

    private void CanMoveAgain()
    {
        isAttacking = false;
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
}
