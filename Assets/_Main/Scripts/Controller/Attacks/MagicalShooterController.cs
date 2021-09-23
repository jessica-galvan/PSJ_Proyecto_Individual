using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalShooterController : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;

    private AttackStats _attackStats;
    private int currentMana;
    protected float timerCD;
    protected bool canShoot;

    public bool IsAttacking { get; private set; }

    public Action<int, int> UpdateMana;

    void Start()
    {
        _attackStats = GetComponent<Actor>().AttackStats;
        currentMana = _attackStats.MaxMana;
    }

    void Update()
    {
        if (IsAttacking)
        {
            timerCD -= Time.deltaTime;
            if(timerCD <= 0)
                IsAttacking = false;
        }
            
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }

    public void RechargeAmmo(int mana)
    {
        if (currentMana < _attackStats.MaxMana)
        {
            if (currentMana < (_attackStats.MaxMana - mana))
                currentMana += mana;
            else
                currentMana = _attackStats.MaxMana;

            UpdateMana?.Invoke(currentMana, _attackStats.MaxMana);
        }
    }
    
    public bool CanRechargeMana()
    {
        return currentMana < _attackStats.MaxMana;
    }

    public void Shoot(Transform target = null)
    {
            if (!IsAttacking && currentMana >= 1)
            {
                IsAttacking = true;
                timerCD = _attackStats.CooldownMana;
                currentMana--;

                InstantiateBullets(shootingPoint, target);
                UpdateMana?.Invoke(currentMana, _attackStats.MaxMana);
            }
    }

    private void InstantiateBullets(Transform shootingPoint, Transform target) 
    {
        var bullet = PoolManager.instance.GetItem(_attackStats.BulletType);
        if(bullet is MagicalBullet)
        {
            var aux = (MagicalBullet)bullet;
            aux.Initialize(shootingPoint, _attackStats, target);
        }
    }
}
