using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InteractableStats", menuName = "Stats/InteractableStats", order = 2)]
public class InteractableStats : ScriptableObject
{
    public int Heal => _heal;
    [SerializeField] private int _heal = 20;

    public int Coin => _coinValue;
    [SerializeField] private int _coinValue = 1;

    public int RechargeAmmo => _rechargeAmmo;
    [SerializeField] private int _rechargeAmmo = 5;

    public int Damage => _damage;
    [SerializeField] private int _damage = 10;
}

