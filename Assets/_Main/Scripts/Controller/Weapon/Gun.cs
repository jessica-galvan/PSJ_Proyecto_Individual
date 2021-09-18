using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletManager))]
public abstract class Gun : MonoBehaviour
{
    //Serializados
    [SerializeField] protected AttackStats _gunStats;
    [SerializeField] protected int bulletsPerShoot = 1;
    [SerializeField]protected BulletManager bulletManager;

    //Privados
    protected float timerCD;
    protected bool canShoot;

    //PROPIEDADES
    public int CurrentMana { get; protected set; }
    public bool CanAttack { get; set; }
    public IOwner Owner { get; protected set; }
    public int MaxAmmo => _gunStats.MaxMana;
    public int Damage => _gunStats.MagicalDamage;
    public float Cooldown => _gunStats.CooldownMana;

    //METODOS
    private void Start()
    {
        CurrentMana = _gunStats.MaxMana;
        bulletManager = GetComponent<BulletManager>();
        bulletManager.Initializer(_gunStats.MagicalAttackPrefab, _gunStats.MaxMana);
    }

    void Update()
    {
        if (timerCD < Time.deltaTime)
            CanAttack = true;
    }

    public void Attack()
    {
        if (CanAttack && CurrentMana >= bulletsPerShoot)
        {
            CanAttack = false;
            timerCD = Time.deltaTime + Cooldown;
            CurrentMana -= bulletsPerShoot;

            InstantiateBullets(Owner.ShootingPoint);
            //TODO: ShootingSound
        }
    }
    public void Reload(int number)
    {
        if (CurrentMana < MaxAmmo)
        {
            if (CurrentMana < (MaxAmmo - number))
                CurrentMana += number;
            else
                CurrentMana = MaxAmmo;
        }
    }
    public abstract void InstantiateBullets(Transform shootingPoint);

    public void SetOwner(IOwner owner)
    {
        Owner = owner;
    }
}
