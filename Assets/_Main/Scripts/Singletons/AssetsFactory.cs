using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsFactory : MonoBehaviour
{
    public static AssetsFactory instance;
    private readonly Factory<MagicalAttackBullet> ammoFactory = new Factory<MagicalAttackBullet>();
    private readonly Factory<InteractableController> interactableFactory = new Factory<InteractableController>();
    private readonly Factory<MonoBehaviour> poolFactory = new Factory<MonoBehaviour>();

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

    public MonoBehaviour Create(MonoBehaviour prefab)
    {
        return poolFactory.Create(prefab);
    }
}
