using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicalShooterController : MonoBehaviour, IOwner
{
    [SerializeField] private Transform shootingPoint;
    private BaseMagicalAttack currentTypeOfAttack;
    private float cdTimer;

    public Transform ShootingPoint => shootingPoint;

    void Start()
    {
        ChangeAttack(GetComponent<BaseMagicalAttack>());
    }

    void Update()
    {
        if (cdTimer < Time.deltaTime)
            currentTypeOfAttack.CanAttack = true;
    }

    public int GetCurrentMana()
    {
        return currentTypeOfAttack.CurrentMana;
    }

    public void RechargeAmmo(int mana)
    {
        currentTypeOfAttack.Reload(mana);
    }
    
    public bool CanRechargeMana()
    {
        return currentTypeOfAttack.CurrentMana < currentTypeOfAttack.MaxMana;
    }

    public void Shoot()
    {
        if (currentTypeOfAttack.CanAttack)
        {
            currentTypeOfAttack.Attack();
        }
    }

    public void ChangeAttack(BaseMagicalAttack newGun)
    {
        currentTypeOfAttack = newGun;
        currentTypeOfAttack.SetOwner(this);
        cdTimer = 0f;
    }
}
