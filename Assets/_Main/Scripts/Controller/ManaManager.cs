using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    private int manaQuantity;
    private MagicalAttack magicalAttackPrefab;
    private Pool<MagicalAttack> magicalAttackList; //Si hay más bullets, hacer una clase base de bullets para que hereden, no usar interface.

    public ManaManager(MagicalAttack prefab, int quantity)
    {
        Initializer(prefab, quantity);
    }

    public void Initializer(MagicalAttack prefab, int quantity)
    {
        manaQuantity = quantity;
        magicalAttackPrefab = prefab;

        var list = new List<MagicalAttack>();
        for (int i = 0; i < manaQuantity; i++)
        {
            var bullet = ManaFactory.instance.CreateMagicalAttack(magicalAttackPrefab);
            list.Add(bullet);
        }

        magicalAttackList =  new Pool<MagicalAttack>(list);
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

    public MagicalAttack GetBullet()
    {
        return magicalAttackList.GetInstance();
    }

    public void StoreBullet(MagicalAttack bullet)
    {
        bullet.CanReturn = false;
        magicalAttackList.Store(bullet);
        //Maybe move position;
    }
}