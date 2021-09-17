using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeHeal : MonoBehaviour, IInteractable
{
    [SerializeField] protected InteractableStats _interactableStats;
    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact(PlayerController character)
    {
        if (character.LifeController.CanHeal())
        {
            character.LifeController.Heal(_interactableStats.Heal);
            Destroy(gameObject);
        }
    }
}
