using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(PhysicalAttackController))]
[RequireComponent(typeof(FollowPlayerController))]
public class EnemyPatrolController : EnemyController
{
    private bool isInCooldown;
    private float currentSpeed;

    public PhysicalAttackController PhysicalAttackController { get; private set; }
    public PatrolMovementController PatrolMovementController { get; private set; }
    public FollowPlayerController FollowPlayerController { get; private set; }

    protected override void Start()
    {
        base.Start();
        FollowPlayerController = GetComponent<FollowPlayerController>();
        PhysicalAttackController = GetComponent<PhysicalAttackController>();
        PatrolMovementController = GetComponent<PatrolMovementController>();
        FollowPlayerController.SetStats(_actorStats);
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze && !LifeController.IsDead)
        {
            FollowPlayerController.CheckIfPlayerIsInView();
            currentSpeed = FollowPlayerController.IsFollowingPlayer ? _actorStats.BuffedSpeed : _actorStats.OriginalSpeed;

            if (FollowPlayerController.CanMove)
            {
                PatrolMovementController.Move(currentSpeed);
                PatrolMovementController.CheckGroundDetection();
            }

            if (!FollowPlayerController.IsFollowingPlayer)
                PatrolMovementController.Patrol();

            if (FollowPlayerController.IsPlayerInRange)
                DoAttack();

            if (!IsAttacking)
            {
                _animatorController.SetBool("Walk", FollowPlayerController.CanMove);
                _animatorController.SetFloat("Speed", currentSpeed);

                if (isInCooldown)
                {
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer <= 0)
                        isInCooldown = false;
                }
            }
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDamage);
    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDead);
    }

    private void DoAttack()
    {
        if (!IsAttacking && CanAttack && !isInCooldown )
        {
            isInCooldown = true;
            _animatorController.SetTrigger("IsAttacking");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolAttack);
            cooldownTimer = _attackStats.CooldownPhysical;
        }
    }
}
