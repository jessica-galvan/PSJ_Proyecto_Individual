using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItemScript : InteractableController
{
    protected override void Interact()
    {
        player.PickUpCollectable(_interactableStats.Coin);
        Destroy();
    }
}
