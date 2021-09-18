using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour, IOwner
{
    [SerializeField] private Gun currentGun;
    [SerializeField] private Transform shootingPoint;
    private float cdTimer;

    public Transform ShootingPoint => shootingPoint;

    void Start()
    {
        ChangeGun(currentGun);
    }

    void Update()
    {
        if (cdTimer < Time.deltaTime)
            currentGun.CanAttack = true;
    }

    public int GetCurrentAmmo()
    {
        return currentGun.CurrentMana;
    }

    public void RechargeAmmo(int ammo)
    {
        currentGun.Reload(ammo);
    }
    
    public bool CanRechargeAmmo()
    {
        return currentGun.CurrentMana < currentGun.MaxAmmo;
    }

    public void Shoot()
    {
        if (currentGun.CanAttack)
        {
            currentGun.Attack();
        }
    }

    public void ChangeGun(Gun newGun)
    {
        currentGun = newGun;
        currentGun.SetOwner(this);
        cdTimer = 0f;
    }
}
