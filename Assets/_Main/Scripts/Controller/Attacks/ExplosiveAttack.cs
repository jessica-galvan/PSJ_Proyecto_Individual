using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : MonoBehaviour
{
    [Header("Attack Physical Settings")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private float  ExplosionRadius = 5f;

    private AttackStats _attackStats;
    public bool IsAttacking { get; private set; }

    public void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle(attackPoint.position, ExplosionRadius, _attackStats.TargetList);
        if (collider != null)
        {
            LifeController life = collider.GetComponent<LifeController>();
            if (life != null)
                life.TakeDamage(_attackStats.PhysicalDamage);
        }
    }

    public void SetAttackStats(AttackStats stats)
    {
        _attackStats = stats;
    }
}
