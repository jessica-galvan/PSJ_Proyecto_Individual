using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCommand : ICommand
{
    private InteractableController _interactable;
    public InteractionCommand(InteractableController interactable)
    {
        _interactable = interactable;
    }

    public void Do()
    {
        //_interactable.Interact();
    }
}
