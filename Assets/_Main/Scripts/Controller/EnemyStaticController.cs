using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
public class EnemyStaticController : EnemyController
{
    public MagicalShooterController MagicController { get; private set; }

    protected override void Start()
    {
        base.Start();
        MagicController = GetComponent<MagicalShooterController>();
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
            if (CanAttack)
            {
                CheckPlayerLocation();
                Attack();
            }

        if (CanAttack && !canShoot && cooldownTimer <= Time.deltaTime)
            canShoot = true;
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayEnemySound(EnemySoundClips.StaticDamage);
    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
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

    private void Attack()
    {
        if (canShoot && !MagicController.IsAttacking && MagicController.GetCurrentMana() > 0)
        {
            canShoot = false;
            cooldownTimer = _attackStats.CooldownMana + Time.time;
            _animatorController.SetTrigger("IsShooting");
            AudioManager.instance.PlayEnemySound(EnemySoundClips.StaticAttack);
        }
    }
}
