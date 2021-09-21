using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAttackBullet : MonoBehaviour
{
    [SerializeField] private BulletType type;

    private AttackStats _attackStats;
    private bool canMove;
    private float timer;
    public bool CanReturn { get; set; }
    public BulletType BulletType => type;

    public void Initialize(Transform firePoint, AttackStats attackStats, bool boolean)
    {
        transform.position = firePoint.position;
        transform.rotation = firePoint.rotation;
        _attackStats = attackStats;
        canMove = boolean;
        timer = _attackStats.LifeMagicalAttack;
    }

    void Update()
    {
        if (canMove)
            transform.position += transform.right * _attackStats.SpellSpeed * Time.deltaTime;

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
        CanReturn = true;
    }
}