using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHeal : InteractableController
{
    protected override void Interact()
    {
        if (player.LifeController.CanHeal())
        {
            player.LifeController.Heal(_interactableStats.Heal);
            AudioManager.instance.PlayPlayerSound(PlayerSoundClips.ReloadMana);
            Destroy();
        }
    }
}
