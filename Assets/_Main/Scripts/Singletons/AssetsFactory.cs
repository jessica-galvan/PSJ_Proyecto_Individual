using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsFactory : MonoBehaviour
{
    public static AssetsFactory instance;
    private readonly Factory<MagicalAttackBullet> ammoFactory = new Factory<MagicalAttackBullet>();
    private readonly Factory<InteractableController> interactableFactory = new Factory<InteractableController>();

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

    public InteractableController CreateInteractable(InteractableController prefab)
    {
        return interactableFactory.Create(prefab);
    }
}
