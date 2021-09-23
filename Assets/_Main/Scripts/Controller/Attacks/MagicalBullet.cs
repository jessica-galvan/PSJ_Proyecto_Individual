using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalBullet : MonoBehaviour, IPooleable
{
    [SerializeField] private PooleableType type;
    private AttackStats _attackStats;
    private bool canMove;
    private float timer;
    private Vector2 direction;

    public PooleableType Type => type;

    public void Initialize(Transform shootingPoint, AttackStats attackStats, Transform target = null)
    {
        _attackStats = attackStats;
        canMove = true;
        timer = _attackStats.LifeMagicalAttack;

        transform.position = shootingPoint.position;
        transform.rotation = shootingPoint.rotation;
        direction = transform.right;

        if (target != null) //Si le paso un objetivo, reescribimos la direccion
        {
            direction = target.position - shootingPoint.position;
            var rotation = direction.normalized;
            transform.right = rotation; 
        }
    }

    void Update()
    {
        if (canMove)
            transform.position += (Vector3) direction * _attackStats.SpellSpeed * Time.deltaTime;



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