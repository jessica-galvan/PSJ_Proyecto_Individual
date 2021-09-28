using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(FollowPlayerController))]
[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(ExplosiveAttack))]
public class EnemyExplosiveController : EnemyController
{
    private bool hasExplode;

    public ExplosiveAttack ExplosiveAttackController { get; private set; }
    public PatrolMovementController PatrolMovementController { get; private set; }
    public FollowPlayerController FollowPlayerController { get; private set; }

    protected override void Start()
    {
        base.Start();
        FollowPlayerController = GetComponent<FollowPlayerController>();
        PatrolMovementController = GetComponent<PatrolMovementController>();
        ExplosiveAttackController = GetComponent<ExplosiveAttack>();
        FollowPlayerController.SetStats(_actorStats);
        ExplosiveAttackController.SetAttackStats(_attackStats);

    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze && !LifeController.IsDead && !hasExplode)
        {
            FollowPlayerController.CheckIfPlayerIsInView();

            print(FollowPlayerController.IsFollowingPlayer);
            if (FollowPlayerController.IsFollowingPlayer && FollowPlayerController.CanMove)
            {
                _animatorController.SetBool("Walk", FollowPlayerController.CanMove);
                _animatorController.SetFloat("Speed", _actorStats.BuffedSpeed);
                PatrolMovementController.Move(_actorStats.BuffedSpeed);
                PatrolMovementController.CheckGroundDetection();
            }

            DoAttack();
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDamage);
        print(LifeController.CurrentLife);
    }

    protected override void OnDeath()
    {
        if (!hasExplode)
        {
            base.OnDeath();
            AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolDead);
            print("Has explode? " + hasExplode);
        }
    }

    protected override void DeathAnimationOver()
    {
        if (!hasExplode)
            base.DeathAnimationOver();
        else
            Destroy(gameObject);
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
