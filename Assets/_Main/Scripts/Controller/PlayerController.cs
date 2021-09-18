using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(MovementController))]
public class PlayerController : Actor, IDamagable
{
    [SerializeField] private int collectableCount = 0;

    public int Coins => collectableCount;
    public MovementController MovementController { get; private set; }
    public MagicalShooterController ShooterController { get; private set; }
    public PhysicalAttackController PhysicalAttackController { get; private set; }

    #region Unity
    void Awake()
    {
        ShooterController = GetComponent<MagicalShooterController>();
        PhysicalAttackController = GetComponent<PhysicalAttackController>();
        MovementController = GetComponent<MovementController>();
    }

    protected override void Start()
    {
        base.Start();
        LevelManager.instance.AssingCharacter(this);
        MovementController.SetStats(_actorStats);
        PhysicalAttackController.SetStats(_attackStats);
        SubscribeEvents();
    }
    #endregion

    #region Privados
    private void SubscribeEvents()
    {
        InputController.instance.OnMove += OnMove;
        InputController.instance.OnShoot += OnShoot;
        InputController.instance.OnJump += OnJump;
        InputController.instance.OnSprint += OnSprint;
        InputController.instance.OnPhysicalAttack += OnPhysicalAttack;

        LifeController.OnHeal += OnHeal;
    }

    private void OnMove(float horizontal)
    {      
        MovementController.OnMove2D(horizontal);
        _animatorController.SetBool("IsRunning", horizontal != 0);
    }

    private void OnShoot()
    {
        if(ShooterController.GetCurrentMana() > 0)
        {
            ShooterController.Shoot();
            _animatorController.SetTrigger("IsShooting");
        } else
        {
            //Invoke Negative sound;
        }
    }

    private void OnPhysicalAttack()
    {
        if (!PhysicalAttackController.IsAttacking)
        {
            PhysicalAttackController.Attack();
            //attackSound.Play();
            _animatorController.SetTrigger("IsPhisicalAttacking");
        }
        else
        {
            //Invoke Negative sound;
        }
    }

    private void OnSprint()
    {
        MovementController.Sprint();
    }

    private void OnJump()
    {
        MovementController.Jump();
    }

    private void OnHeal()
    {
        //TODO: Heal effect?
    }

    protected override void Die()
    {
        //TODO: Destroy? Respawn? Animation? Whatever.
    }

    #endregion

    #region Publicos
    public void PickUpCollectable(int value)
    {
        collectableCount += value;
        HUDManager.instance.UpdateScore(value);
    }

    public void SetCurrentPosition(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }

    public bool CanHeadKill()
    {
        return !MovementController.CheckIfGrounded();
    }
    #endregion
}
