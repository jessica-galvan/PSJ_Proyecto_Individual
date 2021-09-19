using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(PhysicalAttackController))]
public class EnemyPatrolController : EnemyController
{
    public PhysicalAttackController PhysicalAttackController { get; private set; }
    public PatrolMovementController PatrolMovementController { get; private set; }

    private bool isInCooldown;
    

    protected override void Start()
    {
        base.Start();
        PhysicalAttackController = GetComponent<PhysicalAttackController>();
        PatrolMovementController = GetComponent<PatrolMovementController>();
        PatrolMovementController.SetStats(_actorStats);

        CanAttack = true;

    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze && !LifeController.IsDead)
        {
            if (!IsAttacking)
            {
                _animatorController.SetBool("Walk", PatrolMovementController.CanMove);
                _animatorController.SetFloat("Speed", PatrolMovementController.CurrentSpeed);

                if (isInCooldown)
                {
                    cooldownTimer -= Time.deltaTime;
                    if (cooldownTimer <= 0)
                        isInCooldown = false;
                }
            }

            if(PatrolMovementController.IsPlayerInRange)
                DoAttack();
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
