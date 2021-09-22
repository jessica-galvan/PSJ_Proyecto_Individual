using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooleableType
{
    Mana,
    Heal,
    PlayerBullet,
    EnemyBullet,
}

public class PoolManager : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private RechargeMana rechargeManaPrefab;
    [SerializeField] private LifeHeal lifeHealPrefab;
    [SerializeField] private MagicalAttackBullet enemyBaseBulletPrefab;
    [SerializeField] private MagicalAttackBullet playerBulletPrefab;

    //Private Pools
    private Pool<MonoBehaviour> lifeHealPool;
    private Pool<MonoBehaviour> manaPool;
    private Pool<MonoBehaviour> enemyBaseBulletPool;
    private Pool<MonoBehaviour> playerBulletPool;

    public static PoolManager instance;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        lifeHealPool = CreatePool(lifeHealPrefab);
        manaPool = CreatePool(rechargeManaPrefab);
        enemyBaseBulletPool = CreatePool(enemyBaseBulletPrefab);
        playerBulletPool = CreatePool(playerBulletPrefab);
    }

    private Pool<MonoBehaviour> CreatePool(MonoBehaviour item)
    {
        return new Pool<MonoBehaviour>(item);
    }
    public MonoBehaviour GetItem(PooleableType type)
    {
        switch (type)
        {
            case PooleableType.Mana:
                return manaPool.GetInstance();
            case PooleableType.Heal:
                return lifeHealPool.GetInstance();
            case PooleableType.PlayerBullet:
                return playerBulletPool.GetInstance();
            case PooleableType.EnemyBullet:
                return enemyBaseBulletPool.GetInstance() ;
            default:
                return null;
        }
    }
    public void Store(MonoBehaviour item)
    {
        if(item is IPooleable)
        {
            var pool = item as IPooleable;
            switch (pool.Type)
            {
                case PooleableType.Mana:
                    manaPool.Store(item);
                    break;
                case PooleableType.Heal:
                    lifeHealPool.Store(item);
                    break;
                case PooleableType.PlayerBullet:
                    playerBulletPool.Store(item);
                    break;
                case PooleableType.EnemyBullet:
                    enemyBaseBulletPool.Store(item);
                    break;
                default:
                    break;
            }
        }    
    }
}
