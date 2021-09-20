using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    private int manaQuantity;
    private MagicalAttackBullet magicalAttackPrefab;
    private Pool<MagicalAttackBullet> magicalAttackList; //Si hay más bullets, hacer una clase base de bullets para que hereden, no usar interface.

    public ManaManager(MagicalAttackBullet prefab, int quantity)
    {
        Initializer(prefab, quantity);
    }

    public void Initializer(MagicalAttackBullet prefab, int quantity)
    {
        manaQuantity = quantity;
        magicalAttackPrefab = prefab;

        var list = new List<MagicalAttackBullet>();
        for (int i = 0; i < manaQuantity; i++)
        {
            var bullet = AssetsFactory.instance.CreateMagicalAttack(magicalAttackPrefab);
            list.Add(bullet);
        }

        magicalAttackList =  new Pool<MagicalAttackBullet>(list);
    }

    void Update()
    {
        var list = magicalAttackList.GetInUseItems();
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if(list[i].CanReturn)
                StoreBullet(list[i]);
        }
    }

    public MagicalAttackBullet GetBullet()
    {
        return magicalAttackList.GetInstance();
    }

    public void StoreBullet(MagicalAttackBullet bullet)
    {
        bullet.CanReturn = false;
        magicalAttackList.Store(bullet);
    }
}