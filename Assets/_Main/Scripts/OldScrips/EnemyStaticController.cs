using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
public class EnemyStaticController : EnemyController
{
    private float cooldownTimer;
    private bool canShoot = true;
    private bool canTime;

    public MagicalShooterController ShooterController { get; private set; }

    protected override void Start()
    {
        base.Start();
        ShooterController = GetComponent<MagicalShooterController>();
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
            if (canAttack)
            {
                CheckPlayerLocation();
                Shoot();
            }
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
            if (player.transform.position.x > transform.position.x && !facingRight) //estoy a la derecha
                BackFlip();
            else if (player.transform.position.x < transform.position.x && facingRight) //estoy a la izquierda
                BackFlip();
        }
    }

    private void Shoot()
    {
        if (canShoot && Time.time > cooldownTimer)
        {
            canShoot = false;
            //shootingSound.Play();
            _animatorController.SetTrigger("IsShooting");
            //Instantiate(bullet, transform.position + offset, transform.rotation);
            cooldownTimer = _attackStats.CooldownMana + Time.time;
            canShoot = true;
        }
    }
}
