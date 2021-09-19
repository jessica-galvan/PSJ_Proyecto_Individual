using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
public class EnemyStaticController : EnemyController
{
    [Header("Attack Settings")]

    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float cooldown = 2f;
    private float cooldownTimer = 0f;
    private bool canShoot = true;

    public MagicalShooterController ShooterController { get; private set; }

    //Extra
    private bool canTime;

    protected override void Start()
    {
        base.Start();
        canTime = false;
    }

    void Update()
    {
        if (!GameManager.instance.IsGameFreeze)
        {
            if (canAttack) //Si el enemigo puede atacar es porque el player esta dentro de al trigger zone
            {
                CheckPlayerLocation();

                Shoot();
            }
        }
    }

    private void CheckPlayerLocation()
    {
        ////con esto chequea el sentido del player antes de atacar
        //if (player.transform.position.x > transform.position.x && !facingRight) //estoy a la derecha
        //{
        //    BackFlip();
        //}
        //else if (player.transform.position.x < transform.position.x && facingRight) //estoy a la izquierda
        //{
        //    BackFlip();
        //}
    }

    private void Shoot()
    {
        if (canShoot && Time.time > cooldownTimer)
        {
            canShoot = false;
            //shootingSound.Play();
            _animatorController.SetTrigger("IsShooting");
            //Instantiate(bullet, transform.position + offset, transform.rotation);
            cooldownTimer = cooldown + Time.time;
            canShoot = true;
        }
    }
}
