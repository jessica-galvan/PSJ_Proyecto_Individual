using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LifeController))]
[RequireComponent(typeof(MovementController))]
public class Actor : MonoBehaviour, IDamagable
{
    [SerializeField] protected ActorStats _actorStats;

    public LifeController LifeController { get; private set; }
    public MovementController MovementController { get; private set; }

    public virtual void Start()
    {
        LifeController = GetComponent<LifeController>();
        MovementController = GetComponent<MovementController>();
        InitStats();
    }

    private void InitStats()
    {
        MovementController.SetStats(_actorStats);
        LifeController.SetStats(_actorStats);
        LifeController.OnDie += Die;
    }

    public virtual void Die()
    {
        //TODO: TBD
        //Destroy(gameObject);
    }

}
