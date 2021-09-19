using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(MovementController))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : Actor
{
    private int collectableCount;
    protected Rigidbody2D _rigidBody;

    public int Collectables => collectableCount;
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
        if(!ShooterController.IsAttacking && ShooterController.GetCurrentMana() > 0)
        {
            ShooterController.Shoot();
            _animatorController.SetTrigger("IsShooting");
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.MagicalAttack);
        } else
        {
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Negative);
        }
    }

    private void OnPhysicalAttack()
    {
        if (!PhysicalAttackController.IsAttacking)
        {
            PhysicalAttackController.Attack();
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.PhysicalAttack);
            _animatorController.SetTrigger("IsPhisicalAttacking");
        }
        else
        {
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Negative);
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

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Damage);
    }

    protected override void DieAnimation()
    {
        base.DieAnimation();
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Dead);
        OnDie?.Invoke(); //TODO: Fix Bug DeathAnimationOver invoke on player.
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
