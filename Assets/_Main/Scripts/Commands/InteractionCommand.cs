using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionCommand : ICommand
{
    private BaseInteractable _interactable;
    public InteractionCommand(BaseInteractable interactable)
    {
        _interactable = interactable;
    }

    public void Do()
    {
        _interactable.Interact();
    }
}
