using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeMana : InteractableController
{
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
}