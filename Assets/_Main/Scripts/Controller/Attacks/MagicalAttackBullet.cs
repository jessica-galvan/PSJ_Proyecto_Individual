using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAttackBullet : MonoBehaviour, IPooleable
{
    [SerializeField] private PooleableType type;
    private AttackStats _attackStats;
    private bool canMove;
    private float timer;
    private Vector3 direction;

    public PooleableType Type => type;

    public void Initialize(Transform firePoint, AttackStats attackStats, Transform objective = null)
    {

        _attackStats = attackStats;
        canMove = true;
        timer = _attackStats.LifeMagicalAttack;

        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;

        if(objective != null)
        {
            direction = objective.transform.position - transform.position;
            direction.Normalize();
        }
        else
            direction = transform.right;
    }

    void Update()
    {
        if (canMove)
            transform.position += direction * _attackStats.SpellSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
            OnCollision();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifeController life = collision.GetComponent<LifeController>(); 
        if (life != null)
        {
            life.TakeDamage(_attackStats.MagicalDamage);
            OnCollision();
        }

        if (collision.gameObject.layer == 10) //Si collisiona con ground layer...
            OnCollision();
    }

    private void OnCollision()
    {
        canMove = false;
        timer = 0;
        PoolManager.instance.Store(this);
    }
}