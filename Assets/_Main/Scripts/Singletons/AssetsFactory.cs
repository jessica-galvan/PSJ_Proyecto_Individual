using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsFactory : MonoBehaviour
{
    public static AssetsFactory instance;
    private readonly Factory<MagicalAttackBullet> ammoFactory = new Factory<MagicalAttackBullet>();
    private readonly Factory<RechargeMana> manaFactory = new Factory<RechargeMana>();
    private readonly Factory<LifeHeal> lifeHealFactory = new Factory<LifeHeal>();

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

    public MagicalAttackBullet CreateMagicalAttack(MagicalAttackBullet prefab)
    {
        return ammoFactory.Create(prefab);
    }

    public LifeHeal CreateLifeHeal(LifeHeal prefab)
    {
        return lifeHealFactory.Create(prefab);
    }

    public RechargeMana CreateMana(RechargeMana prefab)
    {
        return manaFactory.Create(prefab);
    }
}
