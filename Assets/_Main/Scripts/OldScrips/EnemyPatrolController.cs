using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PatrolMovementController))]
[RequireComponent(typeof(PhysicalAttackController))]
public class EnemyPatrolController : EnemyController
{
    private Rigidbody2D _rigidBody;
    private PhysicalAttackController physicalAttackController;
    private PatrolMovementController patrolMovementController;

    protected override void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        physicalAttackController = GetComponent<PhysicalAttackController>();
        patrolMovementController = GetComponent<PatrolMovementController>();
        physicalAttackController.SetStats(_attackStats);
        patrolMovementController.SetStats(_actorStats, _attackStats);

        CanAttack = true;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if(_animatorController != null)
                _animatorController.SetBool("Walk", patrolMovementController.CanMove); //Mientras canMove sea true, vas a caminar

            //if (patrolMovementController.CanMove)
            //    _animatorController.SetFloat("Speed", patrolMovementController.CurrentSpeed);


            if (!CanAttack && !physicalAttackController.IsAttacking && Time.time > cooldownTimer) //Cooldown Attack Timer
            {
                CanAttack = true;            
                DoAttack();
            }
        }
    }

    private void FixedUpdate()
    {
        if (patrolMovementController.CanMove && !GameManager.instance.IsGameFreeze)
        {
            _rigidBody.velocity = transform.right * patrolMovementController.CurrentSpeed;
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
        if (!physicalAttackController.IsAttacking && CanAttack)
        {
            CanAttack = false;
            patrolMovementController.CanMove = false;
            _animatorController.SetTrigger("IsAttacking");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.PatrolAttack);
            cooldownTimer = _attackStats.CooldownPhysical + Time.deltaTime;
        }
    }
}
