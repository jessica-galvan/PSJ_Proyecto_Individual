using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShooterController))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : Actor, IDamagable
{
    //Serializados
    [SerializeField] private int coins = 0;
    [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private ParticleController damageEffect;

    //Privados
    private float turnSmoothVelocity;

    //Propiedades
    public int Coins => coins;
    public ShooterController ShooterController { get; private set; }

    #region Unity
    void Awake()
    {
        ShooterController = GetComponent<ShooterController>();
    }

    public override void Start()
    {
        base.Start();
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

        LifeController.OnTakeDamage += OnTakeDamage;
        LifeController.OnHeal += OnHeal;
        LifeController.OnDie += OnDie;
    }

    private void OnMove(float horizontal, float vertical)
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, targetAngle, 0f);

        MovementController.Move(direction);
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
        damageEffect.Play();
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

    public void OnShoot()
    {
        ShooterController.Shoot();
    }
    #endregion
}
