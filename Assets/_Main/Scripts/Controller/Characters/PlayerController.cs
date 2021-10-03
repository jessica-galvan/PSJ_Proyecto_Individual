using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(PlayerMovementController))]
public class PlayerController : Actor
{
    private int collectableCount;

    public int Collectables => collectableCount;
    public PlayerMovementController MovementController { get; private set; }
    public MagicalShooterController MagicController { get; private set; }
    public PhysicalAttackController PhysicalAttackController { get; private set; }

    #region Unity
    void Awake()
    {
        MagicController = GetComponent<MagicalShooterController>();
        PhysicalAttackController = GetComponent<PhysicalAttackController>();
        MovementController = GetComponent<PlayerMovementController>();
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

        PhysicalAttackController.PhysicalAttack += OnDoingPhysicalDamage;
    }

    private void OnMove(float horizontal)
    {      
        MovementController.OnMove2D(horizontal);
        _animatorController.SetBool("IsRunning", horizontal != 0);
    }

    private void OnShoot()
    {
        if(!MagicController.IsAttacking && MagicController.GetCurrentMana() > 0)
        {
            MagicController.Shoot();
            _animatorController.SetTrigger("IsShooting");
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.MagicalAttack);
        } else
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Negative);
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
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Negative);
    }

    private void OnSprint()
    {
        MovementController.Sprint();
    }

    private void OnDoingPhysicalDamage()
    {
        if (MagicController.CanRechargeMana())
        {
            MagicController.RechargeAmmo(1);
        }
    }

    protected override void OnTakeDamage()
    {
        base.OnTakeDamage();
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Damage);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        AudioManager.instance.PlayPlayerSound(PlayerSoundClips.Dead);
        OnDie?.Invoke(); //TODO: Fix Bug DeathAnimationOver invoke on player.
    }

    private void OnDestroy()
    {
        InputController.instance.OnMove -= OnMove;
        InputController.instance.OnShoot -= OnShoot;
        InputController.instance.OnJump -= OnJump;
        InputController.instance.OnSprint -= OnSprint;
        InputController.instance.OnPhysicalAttack -= OnPhysicalAttack;

        PhysicalAttackController.PhysicalAttack -= OnDoingPhysicalDamage;
    }
    #endregion

    #region Publicos
    public void PickUpCollectable(int value)
    {
        collectableCount += value;
        HUDManager.instance.UpdateScore(collectableCount);
    }

    public bool CanHeadKill()
    {
        return !MovementController.CheckIfGrounded();
    }
    public void OnJump()
    {
        MovementController.Jump();
    }
    #endregion
}