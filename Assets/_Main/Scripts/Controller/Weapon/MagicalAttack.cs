using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalAttack : MonoBehaviour
{
    [SerializeField] private AttackStats _attackStats;
    private bool canMove;
    private float timer;
    public bool CanReturn { get; set; }
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
            transform.position += transform.right * _attackStats.Speed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0)
            OnDestroy();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LifeController life = collision.GetComponent<LifeController>(); 
        if (life != null)
        {
            life.TakeDamage(_attackStats.MagicalDamage);
            OnDestroy();
        }
    }

    private void OnDestroy()
    {
        CanReturn = true;
    }
}



   
