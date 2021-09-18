using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private int bulletsQuantity;
    private MagicalAttack magicalAttackPrefab;
    private Pool<MagicalAttack> bulletList; //Si hay más bullets, hacer una clase base de bullets para que hereden, no usar interface.

    public BulletManager(MagicalAttack prefab, int quantity)
    {
        Initializer(prefab, quantity);
    }

    public void Initializer(MagicalAttack prefab, int quantity)
    {
        bulletsQuantity = quantity;
        magicalAttackPrefab = prefab;

        var list = new List<MagicalAttack>();
        for (int i = 0; i < bulletsQuantity; i++)
        {
            var bullet = ManaFactory.instance.CreateMagicalAttack(magicalAttackPrefab);
            list.Add(bullet);
        }

        bulletList =  new Pool<MagicalAttack>(list);
    }

    void Update()
    {
        var list = bulletList.GetInUseItems();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if(list[i].CanReturn)
                StoreBullet(list[i]);
        }
    }

    public MagicalAttack GetBullet()
    {
        return bulletList.GetInstance();
    }

    public void StoreBullet(MagicalAttack bullet)
    {
        bullet.CanReturn = false;
        bulletList.Store(bullet);
        //Maybe move position;
    }
}