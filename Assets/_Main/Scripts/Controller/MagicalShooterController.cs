using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaManager))]
public class MagicalShooterController : MonoBehaviour
{
    [SerializeField] private Transform shootingPoint;

    private ManaManager manaManager;
    private AttackStats _attackStats;

    private int currentMana;
    protected float timerCD;
    protected bool canShoot;

    public bool IsAttacking { get; private set; }

    void Start()
    {
        manaManager = GetComponent<ManaManager>();
        _attackStats = GetComponent<Actor>().AttackStats;

        manaManager.Initializer(_attackStats.MagicalAttackPrefab, _attackStats.MaxMana);
        currentMana = _attackStats.MaxMana;
        print(currentMana);
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

            HUDManager.instance.UpdateMana(currentMana, _attackStats.MaxMana);
        }
    }
    
    public bool CanRechargeMana()
    {
        return currentMana < _attackStats.MaxMana;
    }

    public void Shoot()
    {
            if (!IsAttacking && currentMana >= 1)
            {
                IsAttacking = true;
                timerCD = _attackStats.CooldownMana;
                print("set timer");
                currentMana--;

                InstantiateBullets(shootingPoint);
                HUDManager.instance.UpdateMana(currentMana, _attackStats.MaxMana);
            }
    }

    private void InstantiateBullets(Transform shootingPoint) //Aca es donde variaria si el arma es un Pistol, Shotgun, etc. 
    {
        var bullet = manaManager.GetBullet();
        bullet.Initialize(shootingPoint, _attackStats, true);
    }
}
