using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowPlayerController))]
[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(ExplosiveAttack))]
public class EnemyExplosiveController : EnemyController
{
    private bool isInCooldown;
    private bool hasExplode;
    private float currentSpeed;

    public ExplosiveAttack ExplosiveAttackController { get; private set; }
    public PatrolMovementController PatrolMovementController { get; private set; }
    public FollowPlayerController FollowPlayerController { get; private set; }

    protected override void Start()
    {
        base.Start();
        FollowPlayerController = GetComponent<FollowPlayerController>();
        ExplosiveAttackController = GetComponent<ExplosiveAttack>();
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

            DoAttack();

            if (!IsAttacking && !hasExplode)
            {
                _animatorController.SetBool("Walk", FollowPlayerController.CanMove);
                _animatorController.SetFloat("Speed", currentSpeed);
            }
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDamage);
    }

    protected override void OnDeath()
    {
        if (!hasExplode)
        {
            base.OnDeath();
            AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDead);
        }
    }

    protected override void DeathAnimationOver()
    {
        if (!hasExplode)
            base.DeathAnimationOver();
    }

    private void DoAttack()
    {
        if (!hasExplode && FollowPlayerController.IsPlayerInRange)
        {
            hasExplode = true;
            _animatorController.SetTrigger("IsAttacking");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolAttack);
        }
    }
}
