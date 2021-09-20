using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
[RequireComponent(typeof(Animator))]

public class Actor : MonoBehaviour, IDamagable
{
    [Header("Stats")]
    [SerializeField] protected ActorStats _actorStats;
    [SerializeField] protected AttackStats _attackStats;

    protected Animator _animatorController;
    public LifeController LifeController { get; private set; }
    public bool IsAttacking { get; set; }
    public AttackStats AttackStats => _attackStats;
    public Action OnDie;

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
        LifeController.OnDie += DieAnimation;
    }

    protected virtual void OnTakeDamage()
    {
        _animatorController.SetTrigger("TakeDamage");
    }

    protected virtual void DieAnimation()
    {
        _animatorController.SetTrigger("IsDead");
    }

    protected virtual void DeathAnimationOver()
    {
        OnDie?.Invoke();
    }
}
