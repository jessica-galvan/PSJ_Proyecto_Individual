using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaFactory : MonoBehaviour
{
    [SerializeField] private GameObject magicalAttackPrefab;
    public static ManaFactory instance;
    private readonly Factory<MagicalAttack> factory = new Factory<MagicalAttack>();

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

    public MagicalAttack CreateMagicalAttack(MagicalAttack prefab)
    {
        //return factory.Create(magicalAttackPrefab.GetComponent<MagicalAttack>());
        return factory.Create(prefab);
    }
}
