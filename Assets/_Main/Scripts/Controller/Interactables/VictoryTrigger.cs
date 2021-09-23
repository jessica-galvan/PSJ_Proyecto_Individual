using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : BaseInteractable
{
    public override void Interact()
    {
        LevelManager.instance.Victory();
    }
}
