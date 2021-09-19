using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableStats", menuName = "Stats/InteractableStats", order = 2)]
public class InteractableStats : ScriptableObject
{
    public int Heal => _heal;
    [SerializeField] private int _heal = 2;

    public int Coin => _coinValue;
    [SerializeField] private int _coinValue = 1;

    public int RechargeMana => _rechargeMana;
    [SerializeField] private int _rechargeMana = 2;

    public int Damage => _damage;
    [SerializeField] private int _damage = 1;

    public float DamageTimer => _damageCooldown;
    [SerializeField] private float _damageCooldown = 2f;
}

