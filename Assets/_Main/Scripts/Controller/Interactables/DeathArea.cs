using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathArea : BaseInteractable
{
    public override void Interact()
    {
        if (player != null)
            player.LifeController.TakeDamage(player.LifeController.CurrentLife);
    }
}
