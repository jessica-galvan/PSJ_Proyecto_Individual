using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFactory : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    public static BulletFactory instance;
    private Factory<Bullet> factory = new Factory<Bullet>();

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public Bullet CreateBullet()
    {
        return factory.Create(bulletPrefab.GetComponent<Bullet>());
        //TODO: Bullets necesitan luego en el pool setearse la posicion y cuando son disparadas volver a setear info. 
    } 
}
