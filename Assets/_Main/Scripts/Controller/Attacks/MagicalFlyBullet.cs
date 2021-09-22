using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalFlyBullet : MonoBehaviour
{
    [SerializeField] private PooleableType type;
    private AttackStats _attackStats;
    private bool canMove;
    private float timer;

    private Vector2 movement;
    private Vector2 direction;
    private Vector2 spawnPosition;

    public PooleableType Type => type;

    public void Initialize(Transform firePoint, AttackStats attackStats, Transform objective = null)
    {
        _attackStats = attackStats;
        canMove = true;
        timer = _attackStats.LifeMagicalAttack;

        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;

        spawnPosition = transform.position;
        direction = objective.transform.position - (Vector3)spawnPosition;
        direction.Normalize();

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
