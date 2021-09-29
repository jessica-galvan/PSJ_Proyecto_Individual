using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInteractable : MonoBehaviour
{
    [SerializeField] protected InteractableStats _interactableStats;
    protected PlayerController player;
    protected InteractionCommand interact;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null)
        {
            //Interact();
            interact = new InteractionCommand(this);
            GameManager.instance.AddEvent(interact);
        }
    }

    public abstract void Interact();

    protected virtual void Destroy()
    {
        Destroy(gameObject);
    }
}
