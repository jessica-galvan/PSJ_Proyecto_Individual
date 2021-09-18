using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
[RequireComponent(typeof(Animator))]
public class Actor : MonoBehaviour, IDamagable
{
    [SerializeField] protected ActorStats _actorStats;
    [SerializeField] protected AttackStats _attackStats;
    protected Animator _animatorController;
    public LifeController LifeController { get; private set; }
    public AttackStats AttackStats => _attackStats;

    public virtual void Start()
    {
        LifeController = GetComponent<LifeController>();
        _animatorController = GetComponent<Animator>();

        InitStats();
    }

    private void InitStats()
    {

        LifeController.SetStats(_actorStats);
        LifeController.OnDie += Die;
    }

    public virtual void Die()
    {
        //TODO: TBD
        //Destroy(gameObject);
    }
}
