using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackController : MonoBehaviour
{
    [Header("Attack Physical Settings")]
    [SerializeField] private Transform attackPoint;

    private Actor actor;
    private float slashCooldownTimer;
    public bool IsAttacking { get; private set; }

    private void Start()
    {
        actor = GetComponent<Actor>();    
    }

    void Update()
    {
        if(actor.IsAttacking)
        {
            slashCooldownTimer -= Time.deltaTime;
            if (slashCooldownTimer <= 0)
            {
                actor.IsAttacking = false;
            }
        }
    }

    public void Attack()
    {
        if (!actor.IsAttacking)
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, actor.AttackStats.PhysicalAttackRadious, actor.AttackStats.TargetList);
            if (collider != null)
            {
                LifeController life = collider.GetComponent<LifeController>();
                if (life != null)
                {
                    life.TakeDamage(actor.AttackStats.PhysicalDamage);
                    //TODO: RechargeMana, quizas puede ser que el enemigo haga un drop en vez de llamar a la funcion de otro script. O quizas puede ser un invoke?.
                }
            }

            actor.IsAttacking = true;
            slashCooldownTimer = actor.AttackStats.CooldownPhysical;
        }
    }
}
