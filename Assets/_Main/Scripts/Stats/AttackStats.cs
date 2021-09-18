using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MagicStats", menuName = "Stats/MagicStats", order = 1)]
public class AttackStats : ScriptableObject
{
    public bool CanDoPhysicalAttack => _canDoPhysicalAttack;
    [SerializeField] private bool _canDoPhysicalAttack;

    public bool CanDoMagicalAttack => _canDoMagicalAttack;
    [SerializeField] private bool _canDoMagicalAttack;

    public int MagicalDamage => _damageMagical;
    [SerializeField] private int _damageMagical = 2;

    public int PhysicalDamage => _damagePhysical;
    [SerializeField] private int _damagePhysical = 2;

    public MagicalAttack MagicalAttackPrefab => _magicalAttackPrefab;
    [SerializeField] private MagicalAttack _magicalAttackPrefab;

    public float CooldownPhysical => _cooldownPhysical;
    [SerializeField] private float _cooldownPhysical = 1f;

    public float CooldownMana => _cooldownMana;
    [SerializeField] private float _cooldownMana = 2f;

    public int MaxMana => _maxMana;
    [SerializeField] private int _maxMana = 10;

    public float Speed => _speed;
    [SerializeField] private float _speed = 7f;

    public float LifeMagicalAttack => lifeTimerMagicalAttack;
    [SerializeField] private float lifeTimerMagicalAttack = 5f;
}
