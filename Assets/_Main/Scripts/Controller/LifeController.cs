using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    //PRIVADOS
    [SerializeField] private int currentLife;
    private ActorStats stats;
    private int lifeOnRespawn = 2;
    private bool isDead;
    
    //PROPIEDADES
    public int MaxLife => stats.MaxLife;
    public int CurrentLife => currentLife;

    //EVENTOS
    public Action OnDie;
    public Action<int, int> UpdateLifeBar;
    public Action OnTakeDamage;
    public Action OnHeal;
    public Action OnRespawn;

    public void SetStats(ActorStats stats)
    {
        this.stats = stats;
        currentLife = stats.MaxLife;
    }

    public bool CanHeal()
    {
        return currentLife < MaxLife;
    }

    public void Heal(int heal)
    {
        if (currentLife < MaxLife && currentLife > 0)
        {
            if (currentLife < (MaxLife - heal))
                currentLife += heal;
            else
                currentLife = MaxLife;

            OnHeal?.Invoke();
            UpdateLifeBar?.Invoke(CurrentLife, MaxLife);
        }
    }
    public void TakeDamage(int damage)
    {
        if (currentLife > 0)
        {
            currentLife -= damage;
            OnTakeDamage?.Invoke();
            UpdateLifeBar?.Invoke(CurrentLife, MaxLife);
            CheckLife();
        }
    }

    private void CheckLife()
    {
        if (currentLife <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Respawn()
    {
        currentLife = lifeOnRespawn;
        isDead = false;
        UpdateLifeBar?.Invoke(CurrentLife, MaxLife);
        OnRespawn?.Invoke();
    }

    private void Die()
    {
        isDead = true;
        OnDie?.Invoke();
    }
}
