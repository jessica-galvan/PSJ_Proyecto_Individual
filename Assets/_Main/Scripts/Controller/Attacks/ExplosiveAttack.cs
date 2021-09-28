using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveAttack : MonoBehaviour
{
    [Header("Attack Physical Settings")]
    [SerializeField] private Transform attackPoint;

    private AttackStats _attackStats;
    public bool IsAttacking { get; private set; }

    public void Attack()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)attackPoint.position, _attackStats.PhysicalAttackRadious, _attackStats.TargetList);
        if (collider != null)
        {
            LifeController life = collider.GetComponent<LifeController>();
            if (life != null)
                life.TakeDamage(_attackStats.PhysicalDamage);
        }
    }
}
