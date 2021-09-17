using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RechargeAmmo : MonoBehaviour, IInteractable
{
    [SerializeField] protected InteractableStats _interactableStats;

    void Start()
    {
        GetComponent<InteractableController>().interactable = this;
    }

    public void Interact(PlayerController character)
    {
        if (character.ShooterController.CanRechargeAmmo())
        {
            character.ShooterController.RechargeAmmo(_interactableStats.RechargeAmmo);
            Destroy(gameObject);
        }
    }
}