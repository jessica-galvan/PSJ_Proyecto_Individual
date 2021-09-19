using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalAttackController : MonoBehaviour
{
    [Header("Attack Physical Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float attackRadius = 1f;

    private float slashCooldownTimer;
    private AttackStats attackStats;

    public float AttackRadius => attackRadius;

    public bool IsAttacking { get; private set; }

    void Update()
    {
        if(IsAttacking )
        {
            slashCooldownTimer -= Time.deltaTime;
            if (slashCooldownTimer <= 0)
            {
                IsAttacking = false;
            }
        }
    }

    public void Attack()
    {
        if (!IsAttacking)
        {
            Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, attackRadius, attackStats.TargetList);
            if (collider != null)
            {
                LifeController life = collider.GetComponent<LifeController>();
                if (life != null)
                {
                    life.TakeDamage(attackStats.PhysicalDamage);
                    //TODO: RechargeMana, quizas puede ser que el enemigo haga un drop en vez de llamar a la funcion de otro script. O quizas puede ser un invoke?.
                }
            }

            IsAttacking = true;
            slashCooldownTimer = attackStats.CooldownPhysical;
        }
    }

    public void SetStats(AttackStats attack)
    {
        attackStats = attack;
    }
}
