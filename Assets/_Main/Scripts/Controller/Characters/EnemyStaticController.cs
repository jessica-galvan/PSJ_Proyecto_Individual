using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(DetectTargetArea))]
public class EnemyStaticController : EnemyController
{
    public MagicalShooterController MagicController { get; private set; }
    private DetectTargetArea detectionArea;

    protected override void Start()
    {
        base.Start();
        MagicController = GetComponent<MagicalShooterController>();
        detectionArea = GetComponent<DetectTargetArea>();
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            CheckArea();

            if (CanAttack)
            {
                CheckPlayerLocation();
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
        if (canShoot && !MagicController.IsAttacking && MagicController.GetCurrentMana() > 0)
        {
            canShoot = false;
            cooldownTimer = _attackStats.CooldownMana;
            _animatorController.SetTrigger("IsShooting");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.StaticAttack);
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.StaticDamage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.StaticDead);
    }

    private void CheckPlayerLocation()
    {
        if(player != null)
        {
            if (player.transform.position.x > transform.position.x && !FacingRight) //estoy a la derecha
                BackFlip();
            else if (player.transform.position.x < transform.position.x && FacingRight) //estoy a la izquierda
                BackFlip();
        }
    }
}
