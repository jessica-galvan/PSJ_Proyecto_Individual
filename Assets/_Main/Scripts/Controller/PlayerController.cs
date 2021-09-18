using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MagicalShooterController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(MovementController))]
public class PlayerController : Actor, IDamagable
{
    //Serializados
    [SerializeField] private int coins = 0;
    [SerializeField] private float turnSmoothTime = 0.1f;

    //Propiedades
    public int Coins => coins;
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

    public override void Start()
    {
        base.Start();
        MovementController.SetStats(_actorStats);
        GameManager.instance.AssingCharacter(this);
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

        LifeController.OnTakeDamage += OnTakeDamage;
        LifeController.OnHeal += OnHeal;
        LifeController.OnDie += OnDie;
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
        PhysicalAttackController.Attack();
        _animatorController.SetTrigger("IsPhisicalAttacking");
    }

    private void OnSprint()
    {
        MovementController.Sprint();
    }

    private void OnJump()
    {
        MovementController.Jump();
    }

    private void OnTakeDamage()
    {
        _animatorController.SetTrigger("TakeDamage");
        //Invoke damage sound
    }

    private void OnHeal()
    {
        //TODO: Heal effect?
    }

    private void OnDie()
    {
        //TODO: Destroy? Respawn? Animation? Whatever.
    }

    #endregion

    #region Publicos
    public void AddCoins(int value)
    {
        coins += value;
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
