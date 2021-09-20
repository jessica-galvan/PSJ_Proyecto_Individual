using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackStats", menuName = "Stats/Attack", order = 1)]
public class AttackStats : ScriptableObject
{
    public LayerMask TargetList => _targetList;
    [SerializeField] private LayerMask _targetList;

    public int MagicalDamage => _damageMagical;
    [SerializeField] private int _damageMagical = 2;

    public int PhysicalDamage => _damagePhysical;
    [SerializeField] private int _damagePhysical = 2;

    public float PhysicalAttackRadious => _physicalAttackRadious;
    [SerializeField] private float _physicalAttackRadious = 1f;

    public  BulletType BulletType => _bulletType;
    [SerializeField] private BulletType _bulletType;

    public float CooldownPhysical => _cooldownPhysical;
    [SerializeField] private float _cooldownPhysical = 1f;

    public float CooldownMana => _cooldownMana;
    [SerializeField] private float _cooldownMana = 2f;

    public int MaxMana => _maxMana;
    [SerializeField] private int _maxMana = 10;

    public float SpellSpeed => _spellSpeed;
    [SerializeField] private float _spellSpeed = 7f;

    public float LifeMagicalAttack => lifeTimerMagicalAttack;
    [SerializeField] private float lifeTimerMagicalAttack = 5f;
}
