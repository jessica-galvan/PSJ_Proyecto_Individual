using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Gun
{
    public override void InstantiateBullets(Transform shootingPoint) //Aca es donde variaria si el arma es un Pistol, Shotgun, etc. 
    {

        for (int i = 0; i < bulletsPerShoot; i++)
        {
            var bullet = bulletManager.GetBullet();
            bullet.Initialize(shootingPoint, true);
        }
    }
}
