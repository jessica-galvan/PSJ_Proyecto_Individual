using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour
{
    //Serializados
    [SerializeField] protected GunStats _gunStats;
    [SerializeField] protected GameObject ammoPrefab;
    [SerializeField] protected int bulletsPerShoot = 1;
    [SerializeField]protected BulletManager bulletManager;

    //Privados
    protected float timerCD;
    protected bool canShoot;

    //PROPIEDADES
    public int CurrentAmmo { get; protected set; }
    public bool CanAttack { get; set; }
    public IOwner Owner { get; protected set; }
    public int MaxAmmo => _gunStats.MaxAmmo;
    public int Damage => _gunStats.Damage;
    public float Cooldown => _gunStats.Cooldown;

    //METODOS
    private void Start()
    {
        CurrentAmmo = _gunStats.MaxAmmo;
        bulletManager = GetComponent<BulletManager>();
        bulletManager.Initializer(_gunStats.MaxAmmo);
    }

    void Update()
    {
        if (timerCD < Time.deltaTime)
            CanAttack = true;
    }

    public void Attack()
    {
        if (CanAttack && CurrentAmmo >= bulletsPerShoot)
        {
            CanAttack = false;
            timerCD = Time.deltaTime + Cooldown;
            CurrentAmmo -= bulletsPerShoot;

            InstantiateBullets(Owner.ShootingPoint);
        }
    }
    public void Reload(int number)
    {
        if (CurrentAmmo < MaxAmmo)
        {
            if (CurrentAmmo < (MaxAmmo - number))
                CurrentAmmo += number;
            else
                CurrentAmmo = MaxAmmo;
        }
    }
    public abstract void InstantiateBullets(Transform shootingPoint);

    public void SetOwner(IOwner owner)
    {
        Owner = owner;
    }
}
