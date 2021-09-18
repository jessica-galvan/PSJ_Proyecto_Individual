using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaManager))]
public abstract class BaseMagicalAttack : MonoBehaviour
{
    //Serializados
    [SerializeField] protected int spellsPerAttack = 1;

    //Privados
    protected ManaManager manaManager;
    protected float timerCD;
    protected bool canShoot;
    protected AttackStats _attackStats;

    //PROPIEDADES
    public int CurrentMana { get; protected set; }
    public bool CanAttack { get; set; }
    public IOwner Owner { get; protected set; }
    public int MaxMana => _attackStats.MaxMana;
    public int Damage => _attackStats.MagicalDamage;
    public float Cooldown => _attackStats.CooldownMana;

    //METODOS
    private void Start()
    {
        manaManager = GetComponent<ManaManager>();
        _attackStats = GetComponent<Actor>().AttackStats;
        CurrentMana = _attackStats.MaxMana;
        manaManager.Initializer(_attackStats.MagicalAttackPrefab, _attackStats.MaxMana);
    }

    void Update()
    {
        if (timerCD < Time.deltaTime)
            CanAttack = true;
    }

    public void Attack()
    {
        if (CanAttack && CurrentMana >= spellsPerAttack)
        {
            CanAttack = false;
            timerCD = Time.deltaTime + Cooldown;
            CurrentMana -= spellsPerAttack;

            InstantiateBullets(Owner.ShootingPoint);
            HUDManager.instance.UpdateMana(CurrentMana, MaxMana);
            //TODO: ShootingSound
        }
    }
    public void Reload(int number)
    {
        if (CurrentMana < MaxMana)
        {
            if (CurrentMana < (MaxMana - number))
                CurrentMana += number;
            else
                CurrentMana = MaxMana;

            HUDManager.instance.UpdateMana(CurrentMana, MaxMana);
        }
    }
    public abstract void InstantiateBullets(Transform shootingPoint);

    public void SetOwner(IOwner owner)
    {
        Owner = owner;
    }
}
