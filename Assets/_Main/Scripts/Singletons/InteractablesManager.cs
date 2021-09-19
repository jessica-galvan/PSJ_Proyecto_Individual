using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractableType
{
    Mana,
    Heal
}

public class InteractablesManager : MonoBehaviour
{
    private int dropQuantity;
    [SerializeField] private RechargeMana rechargeManaPrefab;
    [SerializeField] private LifeHeal lifeHealPrefab;
    private Pool<InteractableController> lifeHealPool;
    private Pool<InteractableController> manaPool;

    public static InteractablesManager instance;

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
        dropQuantity = LevelManager.instance.EnemyCounter > 0 ? LevelManager.instance.EnemyCounter : 5;
        Initializer(dropQuantity);
    }

    public void Initializer(int quantity)
    {
        dropQuantity = quantity;

        var lifeList = new List<InteractableController>();
        var manaList = new List<InteractableController>();
        for (int i = 0; i < dropQuantity; i++)
        {
            var life = AssetsFactory.instance.CreateLifeHeal(lifeHealPrefab);
            var mana = AssetsFactory.instance.CreateMana(rechargeManaPrefab);
            lifeList.Add(life);
            manaList.Add(mana);
        }

        lifeHealPool = new Pool<InteractableController>(lifeList);
        manaPool = new Pool<InteractableController>(manaList);
    }

    void Update()
    {
        CheckList(manaPool.GetInUseItems());
        CheckList(lifeHealPool.GetInUseItems());
    }

    private void CheckList(List<InteractableController> list)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CanReturn)
                StoreInteractable(list[i]);
        }
    }

    public InteractableController GetItem(InteractableType type)
    {
        switch (type)
        {
            case InteractableType.Mana:
                return  manaPool.GetInstance() as InteractableController;
            case InteractableType.Heal:
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
        if (item is RechargeMana)
            manaPool.Store(item as RechargeMana);
    }
}
