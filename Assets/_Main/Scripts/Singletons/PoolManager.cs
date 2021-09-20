using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PooleableType
{
    Mana,
    Heal
}
public enum BulletType
{
    PlayerBullet,
    EnemyBullet,
    FlyBullet
}

public class PoolManager : MonoBehaviour
{
    [SerializeField] private int interactableQuantity = 5;
    [SerializeField] private int enemyBulletsQuantity = 10;
    [SerializeField] private int playerBulletsQuantity = 6;

    [Header("Prefabs")]
    [SerializeField] private RechargeMana rechargeManaPrefab;
    [SerializeField] private LifeHeal lifeHealPrefab;
    [SerializeField] private MagicalAttackBullet enemyFlyBulletPrefab;
    [SerializeField] private MagicalAttackBullet enemyBaseBulletPrefab;
    [SerializeField] private MagicalAttackBullet playerBulletPrefab;

    //Private Pools
    private Pool<InteractableController> lifeHealPool;
    private Pool<InteractableController> manaPool;
    private Pool<MagicalAttackBullet> enemyFlyBulletPool;
    private Pool<MagicalAttackBullet> enemyBaseBulletPool;
    private Pool<MagicalAttackBullet> playerBulletPool;

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
        interactableQuantity = LevelManager.instance.EnemyCounter > 0 ? LevelManager.instance.EnemyCounter : interactableQuantity;
        Initializer();
    }

    public void Initializer()
    {
        lifeHealPool = CreateInteractableList(lifeHealPrefab);
        manaPool = CreateInteractableList(rechargeManaPrefab);
        enemyFlyBulletPool = CreateBulletList(enemyFlyBulletPrefab, enemyBulletsQuantity);
        enemyBaseBulletPool = CreateBulletList(enemyBaseBulletPrefab, enemyBulletsQuantity);
        playerBulletPool = CreateBulletList(playerBulletPrefab, playerBulletsQuantity);
    }

    void Update()
    {
        CheckItemsList(manaPool.GetInUseItems());
        CheckItemsList(lifeHealPool.GetInUseItems());
        CheckBulletList(enemyBaseBulletPool.GetInUseItems());
        CheckBulletList(enemyFlyBulletPool.GetInUseItems());
        CheckBulletList(playerBulletPool.GetInUseItems());
    }

    #region InteractablesManager
    private void CheckItemsList(List<InteractableController> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CanReturn)
                StoreInteractable(list[i]);
        }
    }

    public InteractableController GetItem(PooleableType type)
    {
        switch (type)
        {
            case PooleableType.Mana:
                return  manaPool.GetInstance() as InteractableController;
            case PooleableType.Heal:
                return lifeHealPool.GetInstance() as InteractableController;
            default:
                return null;
        }
    }

    public void StoreInteractable(IPooleable item)
    {
        item.CanReturn = false;
        if (item is LifeHeal)
            lifeHealPool.Store(item as LifeHeal);
        else if (item is RechargeMana)
            manaPool.Store(item as RechargeMana);
    }

    private Pool<InteractableController> CreateInteractableList(InteractableController item)
    {
        var list = new List<InteractableController>();
        for (int i = 0; i < interactableQuantity; i++)
        {
            var aux = AssetsFactory.instance.CreateInteractable(item);
            list.Add(aux);
        }

        return new Pool<InteractableController>(list);
    }
    #endregion

    #region BulletManager
    private void CheckBulletList(List<MagicalAttackBullet> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CanReturn)
                StoreBullet(list[i]);
        }
    }
    public MagicalAttackBullet GetBullet(BulletType type)
    {
        switch (type)
        {
            case BulletType.PlayerBullet:
                return playerBulletPool.GetInstance();
            case BulletType.EnemyBullet:
                return enemyBaseBulletPool.GetInstance();
            case BulletType.FlyBullet:
                return enemyFlyBulletPool.GetInstance();
            default:
                return null;
        }
    }
    public void StoreBullet(MagicalAttackBullet bullet)
    {
        bullet.CanReturn = false;
        switch (bullet.BulletType)
        {
            case BulletType.PlayerBullet:
                playerBulletPool.Store(bullet);
                break;
            case BulletType.EnemyBullet:
                enemyBaseBulletPool.Store(bullet);
                break;
            case BulletType.FlyBullet:
                enemyFlyBulletPool.Store(bullet);
                break;
        }
    }
    private Pool<MagicalAttackBullet> CreateBulletList(MagicalAttackBullet item, int quantity)
    {
        var list = new List<MagicalAttackBullet>();
        for (int i = 0; i < quantity; i++)
        {
            var aux = AssetsFactory.instance.CreateMagicalAttack(item);
            list.Add(aux);
        }

        return new Pool<MagicalAttackBullet>(list);
    }

    #endregion
}
