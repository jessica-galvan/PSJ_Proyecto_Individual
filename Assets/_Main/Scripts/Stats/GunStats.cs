using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GunStats", menuName = "Stats/GunStats", order = 1)]
public class GunStats : ScriptableObject
{
    public int Damage => _damage;
    [SerializeField] private int _damage = 2;

    public Bullet BulletPrefab => _bulletPrefab;
    [SerializeField] private Bullet _bulletPrefab;

    public float Cooldown => _cooldown;
    [SerializeField] private float _cooldown = 2f;

    public int MaxAmmo => _maxAmmo;
    [SerializeField] private int _maxAmmo = 10;

    public float Speed => _speed;
    [SerializeField] private float _speed = 7f;

    public float LifeBullet => lifeTimerBullet;
    [SerializeField] private float lifeTimerBullet = 5f;
}
