using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : BaseInteractable
{
    public override void Interact()
    {
        player.PickUpCollectable(_interactableStats.Coin);
        Destroy();
    }
}
