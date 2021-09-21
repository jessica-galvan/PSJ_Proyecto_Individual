using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneManaAttack : BaseMagicalAttack
{
    public override void InstantiateBullets(Transform shootingPoint) //Aca es donde variaria si el arma es un Pistol, Shotgun, etc. 
    {
        for (int i = 0; i < spellsPerAttack; i++)
        {
            var bullet = PoolManager.instance.GetItem(_attackStats.BulletType);
            if (bullet is MagicalAttackBullet)
            {
                var aux = (MagicalAttackBullet)bullet;
                aux.Initialize(shootingPoint, _attackStats, true);
            }
        }
    }
}
