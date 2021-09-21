using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeMana : InteractableController, IPooleable
{
    [SerializeField] private PooleableType type;
    public PooleableType Type => type;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    protected override void Interact()
    {
        if (player.MagicController.CanRechargeMana())
        {
            player.MagicController.RechargeAmmo(_interactableStats.RechargeMana);
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.ReloadMana);
            Destroy();
        }
    }

    protected override void Destroy()
    {
        PoolManager.instance.Store(this);
    }
}