using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Actor : MonoBehaviour, IDamagable
{
    [SerializeField] protected ActorStats _actorStats;
    [SerializeField] protected AttackStats _attackStats;
    protected Animator _animatorController;
    protected Rigidbody2D _rigidBody;
    public LifeController LifeController { get; private set; }
    public AttackStats AttackStats => _attackStats;

    protected virtual void Start()
    {
        LifeController = GetComponent<LifeController>();
        _animatorController = GetComponent<Animator>();

        InitStats();
    }

    protected void InitStats()
    {
        LifeController.SetStats(_actorStats);
        LifeController.OnTakeDamage += OnTakeDamage;
        LifeController.OnDie += Die;
    }

    protected virtual void OnTakeDamage()
    {
        _animatorController.SetTrigger("TakeDamage");
        //Invoke damage sound
    }

    protected virtual void Die()
    {
        //TODO: TBD
        //Destroy(gameObject);
    }
}
